
using GTI.API.Models;
using GTI.Wcf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ClienteController : ApiController
    {
        // GET cliente-get
        public IEnumerable<Cliente> Get()
        {
            var service = new ServiceCliente();
            IEnumerable<Cliente> clientes = new List<Cliente>();
            clientes = service.Listar();
            return clientes;
        }

        // GET cliente-get{id}
        public Cliente Get(int id)
        {
            var service = new ServiceCliente();
            return service.Obter(id);
        }

        // POST cliente-post
        public HttpResponseMessage Post([System.Web.Http.FromBody]Cliente cliente)
        {
            var service = new ServiceCliente();
            var id = service.Incluir(cliente);
            cliente.Id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            string location = Url.Link("DefaultApi", new { controller = "cliente", id = cliente.Id });
            response.Headers.Location = new Uri(location) ;
            return response;
        }

        // PUT cliente-Put{id}
        public HttpResponseMessage Put(int id, [System.Web.Http.FromBody]Cliente cliente)
        {
            var service = new ServiceCliente();
            if (cliente == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Corpo da requisição ausente.");
            }
            cliente.Id = id;
            service.Alterar(cliente);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string location = Url.Link("DefaultApi", new { controller = "cliente", id = id });
            response.Headers.Location = new Uri(location);
            return response;
        }

        // DELETE cliente-delete{id}
        /// <summary>
        ///DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, [System.Web.Http.FromBody]Cliente cliente)
        {
            var service = new ServiceCliente();
            service.Excluir(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string location = Url.Link("DefaultApi", new { controller = "cliente", id = id });
            response.Headers.Location = new Uri(location);
            return response;
        }
    }
}