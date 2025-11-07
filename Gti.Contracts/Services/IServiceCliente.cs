using Gti.Contracts.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Gti.Contracts.Services
{
    [ServiceContract]
    public interface IServiceCliente
    {
        [OperationContract]
        List<Cliente> Listar();

        [OperationContract]
        Cliente Obter(int id);

        [OperationContract]
        int Incluir(Cliente cliente);

        [OperationContract]
        void Alterar(Cliente cliente);

        [OperationContract]
        void Excluir(int id);
    }
}