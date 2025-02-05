using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.MoradaService
{
    public class MoradaService : IMoradaService
    {
        private readonly HttpClient _httpClient;

        public MoradaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Morada?> GetMorada(Guid clientePk, Guid moradaPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Morada/{clientePk}/{moradaPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var morada = await JsonSerializer.DeserializeAsync<Morada>(response, options);

                return morada;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddMorada(Morada morada, Guid idCliente)
        {
            try
            {
                morada.ClienteId = idCliente;

                var itemJson = new StringContent(JsonSerializer.Serialize(morada), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Morada", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateMorada(Guid pk, Morada morada)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(morada), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Morada/{pk}", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteMoradaToCliente(Guid clientePk, Guid moradaPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Morada/ByCliente/{clientePk}/{moradaPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir morada: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddMoradaToCliente(Guid clientePk, Morada morada)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Morada/ByCliente/{clientePk}", morada);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar morada: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateMoradaToCliente(Guid clientePk, Guid moradaPk, Morada morada)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Morada/ByCliente/{clientePk}/{moradaPk}", morada);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar morada: {ex.Message}");
                throw;
            }
        }

    }
}
