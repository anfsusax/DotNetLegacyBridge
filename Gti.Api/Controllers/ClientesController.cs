using System;
using System.Collections.Generic;
using System.Web.Http;
using Gti.Api.Infrastructure.WcfClient;
using Gti.Contracts.Models;

namespace Gti.Api.Controllers
{
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        private readonly WcfClienteService _wcfService;

        public ClientesController()
        {
            // TODO: Move endpoint to configuration
            _wcfService = new WcfClienteService("http://localhost:PORT/ServiceCliente.svc");
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var clientes = _wcfService.Listar();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var cliente = _wcfService.Obter(id);
                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = _wcfService.Incluir(cliente);
                return Created($"api/clientes/{id}", id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != cliente.Id)
                    return BadRequest("Id na URL não corresponde ao Id do cliente");

                _wcfService.Alterar(cliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _wcfService.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _wcfService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}