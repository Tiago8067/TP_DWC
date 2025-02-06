using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Pages.PageCliente;
using System.Net.Http.Json;

namespace Tp_DWC.Shared.Services.RegistoFotograficoService
{
    public class RegFotService : IRegFotService
    {
        private readonly HttpClient _httpClient;

        public RegFotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegistoFotografico?> GetRegFotByIdCliente(int assistenciaPk, Guid regFotPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/RegFot/{assistenciaPk}/{regFotPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var result = await JsonSerializer.DeserializeAsync<RegistoFotografico>(response, options);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddRegFotToClient(int assistenciaPk, RegistoFotografico regFot)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/RegFot/ByAssis/{assistenciaPk}", regFot);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar RegistoFotografico: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteRegFotToCliente(int assistenciaPk, Guid regFotPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/RegFot/ByAssis/{assistenciaPk}/{regFotPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao eliminar RegistoFotografico: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateRegFotToCliente(int assistenciaPk, Guid regFotPk, RegistoFotografico regFot)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/RegFot/ByAssis/{assistenciaPk}/{regFotPk}", regFot);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar RegistoFotografico: {ex.Message}");
                throw;
            }
        }
    }
}
