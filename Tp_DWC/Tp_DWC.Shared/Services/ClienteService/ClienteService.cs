using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.DTO;

namespace Tp_DWC.Shared.Services.ClienteService
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Cliente>?> AllClientes()
        {
            try
            {
                var response = await _httpClient.GetStreamAsync("api/Cliente");

                var clientes = await JsonSerializer.DeserializeAsync<IEnumerable<Cliente>>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<Cliente?> GetCliente(Guid pk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Cliente/{pk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var cliente = await JsonSerializer.DeserializeAsync<Cliente>(response, options);

                //var cliente = await JsonSerializer.DeserializeAsync<Cliente>(response, new JsonSerializerOptions()
                //{
                //    PropertyNameCaseInsensitive = true
                //});

                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddCliente(Cliente cliente)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Cliente", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCliente(Guid pk, Cliente cliente)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Cliente/{pk}", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCliente(Guid pk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Cliente/{pk}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddClienteComDetalhes(ClienteCompletoDTO clienteCompleto)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(clienteCompleto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Cliente/adicionar-completo", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

    }
}
