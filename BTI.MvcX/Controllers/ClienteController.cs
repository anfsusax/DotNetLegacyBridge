using BTI.MvcX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BTI.MvcX.Controllers
{
    public class ClienteController : Controller
    {
        private readonly string apiBaseUrl;

        public ClienteController()
        { 
            apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "http://localhost:55812";
        }
         
        public ActionResult Home()
        {
            return View();
        }
         
        public async Task<ActionResult> Index()
        {
            try
            {
                List<Cliente> listaClientes = new List<Cliente>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiBaseUrl}/cliente-get");

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
         
        [HttpGet]
        public async Task<ActionResult> Detalhes(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiBaseUrl}/cliente-get{id}");

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

                return View("Detalhes", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }
         
        [HttpGet]
        public ActionResult Adicionar()
        {
            return View("Form", new Cliente());
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCliente(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Form", cliente);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PostAsync($"{apiBaseUrl}/cliente-post", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ToastMessage"] = "Cliente adicionado com sucesso!";
                        TempData["ToastVariant"] = "success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao adicionar cliente. Status: " + response.StatusCode;
                    }
                }

                return View("Form", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View("Form", cliente);
            }
        }
         
        [HttpGet]
        public async Task<ActionResult> UpdateCliente(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiBaseUrl}/cliente-get{id}");

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

                return View("FormAlterar", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCliente(Cliente cliente)
        {
            try
            {
                

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PutAsync($"{apiBaseUrl}/cliente-Put{cliente.Id}", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ToastMessage"] = "Cliente atualizado com sucesso!";
                        TempData["ToastVariant"] = "success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Erro ao atualizar cliente. Status: " + response.StatusCode;
                    }
                }

                return View("FormAlterar", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View("FormAlterar", cliente);
            }
        }
         
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetAsync($"{apiBaseUrl}/cliente-get{id}");

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

                return View("FormExcluir", cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao comunicar com a API: " + ex.Message;
                return View(new Cliente());
            }
        }
         
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
                     
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(cliente ?? new Cliente { Id = id }),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var request = new HttpRequestMessage(HttpMethod.Delete, $"{apiBaseUrl}/cliente-delete{id}")
                    {
                        Content = jsonContent
                    };

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["ToastMessage"] = "Cliente excluído com sucesso!";
                        TempData["ToastVariant"] = "success";
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