using GTI.API.Models;
using GTI.DAO;
using System.Collections.Generic;

namespace GTI.BL
{
    public class ClienteBL
    {
        public int Inserir(Cliente cliente)
        {
            ClienteDao dao = new ClienteDao();
            return dao.Inserir(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            new ClienteDao().Atualizar(cliente);
        }

        public void Excluir(int intIdCliente)
        {
            ClienteDao dao = new ClienteDao();
            dao.Excluir(intIdCliente);
        }

        public List<Cliente> Listar()
        {
            return new ClienteDao().Listar();
        }

        public Cliente Obter(int Id)
        {
            return new ClienteDao().Obter(Id);
        }
    }
}