using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GTIMVC.Model;
using Newtonsoft.Json;
  
namespace GTIMVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly string apiUrl;

        public ClienteController()
        {
            // Obtém a URL da API do Web.config ou usa valor padrão
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"] ?? "http://localhost:55812/api/cliente";
        }

        // GET: Cliente/Home
        public ActionResult Home()
        {
            return View();
        }

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            try
            {
                List<Model.Cliente> listaClientes = new List<Cliente>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync(apiUrl + "/cliente-get");

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse) ?? new List<Cliente>();
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao carregar clientes. Status: " + response.StatusCode;
                    }
                }

                return View(listaClientes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new List<Cliente>());
            }
        }

        // GET: Cliente/GetCliente/5
        [HttpGet]
        public async Task<ActionResult> GetCliente(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiUrl}/cliente-get{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao buscar cliente. Status: " + response.StatusCode;
                    }
                }

                if (cliente == null)
                {
                    ViewBag.Error = "Cliente não encontrado.";
                    return HttpNotFound();
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }

        // GET: Cliente/Adicionar
        [HttpGet]
        public ActionResult Adicionar()
        {
            return View(new Cliente());
        }

        // POST: Cliente/AddCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCliente(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Adicionar", cliente);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PostAsync(apiUrl + "/cliente-post", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Success = "Cliente adicionado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao adicionar cliente. Status: " + response.StatusCode;
                    }
                }

                return View("Adicionar", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View("Adicionar", cliente);
            }
        }

        // GET: Cliente/UpdateCliente/5
        [HttpGet]
        public async Task<ActionResult> UpdateCliente(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiUrl}/cliente-get{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao buscar cliente. Status: " + response.StatusCode;
                    }
                }

                if (cliente == null)
                {
                    ViewBag.Error = "Cliente não encontrado.";
                    return HttpNotFound();
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }

        // POST: Cliente/UpdateCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCliente(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cliente);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PutAsync($"{apiUrl}/cliente-Put{cliente.Id}", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Success = "Cliente atualizado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao atualizar cliente. Status: " + response.StatusCode;
                    }
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(cliente);
            }
        }

        // GET: Cliente/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiUrl}/cliente-get{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                    }
                }

                if (cliente == null)
                {
                    ViewBag.Error = "Cliente não encontrado.";
                    return HttpNotFound();
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }

        // POST: Cliente/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id, Cliente cliente)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    
                    // A API espera um body mesmo no DELETE, então usamos SendAsync
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente ?? new Cliente { Id = id }),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var request = new HttpRequestMessage(HttpMethod.Delete, $"{apiUrl}/cliente-delete{id}")
                    {
                        Content = jsonContent
                    };

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Success = "Cliente excluído com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao excluir cliente. Status: " + response.StatusCode;
                    }
                }

                return RedirectToAction("Delete", new { id = id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return RedirectToAction("Delete", new { id = id });
            }
        }
    }
}