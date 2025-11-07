using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BTI.MvcX.Services;
using Gti.Contracts.Models;

namespace BTI.MvcX.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteApiService _clienteService;

        public ClientesController()
        {
            // TODO: Mover para configuração
            _clienteService = new ClienteApiService("http://localhost:8081/");
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var clientes = await _clienteService.ListarAsync();
                return View(clientes);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao carregar clientes: " + ex.Message;
                return View(new Cliente[] { });
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterAsync(id);
                if (cliente == null)
                    return HttpNotFound();

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao carregar cliente: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _clienteService.IncluirAsync(cliente);
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao criar cliente: " + ex.Message;
                return View(cliente);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clienteService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}