using GTI.API.Models;
using GTI.BL;
using System.Collections.Generic;

namespace GTI.Wcf
{
    public class ServiceCliente : IServiceCliente
    {
        public List<Cliente> Listar()
        {
            ClienteBL clienteBL = new ClienteBL();
            return clienteBL.Listar();
        }
        public Cliente Obter(int id)
        {
            ClienteBL clienteBL = new ClienteBL();
            return clienteBL.Obter(id);
        }
        public int Incluir(Cliente cliente)
        {
            ClienteBL clienteBL = new ClienteBL();
            return clienteBL.Inserir(cliente);
        }
        public void Excluir(int id)
        {
            ClienteBL clienteBL = new ClienteBL();
            clienteBL.Excluir(id);
        }

        public void Alterar(Cliente cliente)
        {
            ClienteBL clienteBL = new ClienteBL();
            clienteBL.Atualizar(cliente);
        }
    }
}
