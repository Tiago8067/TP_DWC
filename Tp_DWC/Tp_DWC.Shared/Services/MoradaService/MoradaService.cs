using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task<Morada?> GetMorada(Guid pk)
        {
            try
            {
                //var response = await _httpClient.GetStreamAsync($"api/Morada/{pk}");
                var response = await _httpClient.GetStreamAsync($"api/Morada");

                var morada = await JsonSerializer.DeserializeAsync<Morada>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

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

        public async Task<bool> DeleteMorada(Guid pk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Morada/{pk}");

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
