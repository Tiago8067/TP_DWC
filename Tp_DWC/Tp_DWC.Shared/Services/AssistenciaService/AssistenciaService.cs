using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using System.Net.Http.Json;

namespace Tp_DWC.Shared.Services.AssistenciaService
{
    public class AssistenciaService : IAssistenciaService
    {
        private readonly HttpClient _httpClient;

        public AssistenciaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Assistencia?> GetAssistenciaByIdCliente(Guid clientePk, int assistenciaPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Assistencia/{clientePk}/{assistenciaPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var assistencia = await JsonSerializer.DeserializeAsync<Assistencia>(response, options);

                return assistencia;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddAssistenciaToClienteBool(Guid clientePk, Assistencia assistencia)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Assistencia/ByCliente/{clientePk}", assistencia);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar assistência: {ex.Message}");
                throw;
            }
        }

        public async Task<Assistencia?> AddAssistenciaToClienteObj(Guid clientePk, Assistencia assistencia)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Assistencia/ByCliente/{clientePk}", assistencia);
                if (response.IsSuccessStatusCode)
                {
                    // Desserializa o objeto inserido
                    var result = await response.Content.ReadFromJsonAsync<Assistencia>();
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar assistência: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAssistenciaToCliente(Guid clientePk, int assistenciaPk, Assistencia assistencia)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Assistencia/ByCliente/{clientePk}/{assistenciaPk}", assistencia);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar assistência: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAssistenciaToCliente(Guid clientePk, int assistenciaPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Assistencia/ByCliente/{clientePk}/{assistenciaPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao eliminar assistência: {ex.Message}");
                throw;
            }
        }


    }
}
