using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Shared.Services.EstadoService
{
    public class EstadoService : IEstadoService
    {
        private readonly HttpClient _httpClient;

        public EstadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Estado>?> GetAllEstados()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Estado>>("api/Estado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter estados: {ex.Message}");
                throw;
            }
        }

        public async Task<string?> GetEstadoDescricaoById(Guid id)
        {
            try
            {
                //return await _httpClient.GetFromJsonAsync<string>($"api/Estado/{id}");
                // Obtém a resposta como string
                var result = await _httpClient.GetStringAsync($"api/Estado/{id}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter descrição do estado: {ex.Message}");
                throw;
            }
        }


    }
}
