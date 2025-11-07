using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Gti.Contracts.Models;
using Newtonsoft.Json;

namespace BTI.MvcX.Services
{
    public class ClienteApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ClienteApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<Cliente>> ListarAsync()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Cliente>>(content);
        }

        public async Task<Cliente> ObterAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
                
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cliente>(content);
        }

        public async Task<int> IncluirAsync(Cliente cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/clientes", cliente);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(content);
        }

        public async Task AlterarAsync(Cliente cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/clientes/{cliente.Id}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}