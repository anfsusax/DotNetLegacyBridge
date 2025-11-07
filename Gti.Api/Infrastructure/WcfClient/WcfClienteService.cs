using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gti.Contracts.Models;
using Gti.Contracts.Services;

namespace Gti.Api.Infrastructure.WcfClient
{
    public class WcfClienteService : IDisposable
    {
        private ChannelFactory<IServiceCliente> _channelFactory;
        private IServiceCliente _channel;

        public WcfClienteService(string endpointAddress)
        {
            var binding = new BasicHttpBinding();
            _channelFactory = new ChannelFactory<IServiceCliente>(binding, new EndpointAddress(endpointAddress));
            _channel = _channelFactory.CreateChannel();
        }

        public List<Cliente> Listar()
        {
            return _channel.Listar();
        }

        public Cliente Obter(int id)
        {
            return _channel.Obter(id);
        }

        public int Incluir(Cliente cliente)
        {
            return _channel.Incluir(cliente);
        }

        public void Alterar(Cliente cliente)
        {
            _channel.Alterar(cliente);
        }

        public void Excluir(int id)
        {
            _channel.Excluir(id);
        }

        public void Dispose()
        {
            if (_channelFactory != null)
            {
                _channelFactory.Close();
                _channelFactory = null;
            }
        }
    }
}