using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using System.Drawing;
using System.Net.Http.Json;

namespace Tp_DWC.Shared.Services.RegistoMaterialService
{
    public class RegMatService : IRegMatService 
    {
        private readonly HttpClient _httpClient;

        public RegMatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegistoMaterial?> GetRegMatByIdCliente(int assistenciaPk, Guid regMatPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/RegMat/{assistenciaPk}/{regMatPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var result = await JsonSerializer.DeserializeAsync<RegistoMaterial>(response, options);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddRegMatToClient(int assistenciaPk, RegistoMaterial regMat)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/RegMat/ByAssis/{assistenciaPk}", regMat);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar RegistoMaterial: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteRegMatToCliente(int assistenciaPk, Guid regMatPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/RegMat/ByAssis/{assistenciaPk}/{regMatPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao eliminar RegistoMaterial: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateRegMatToCliente(int assistenciaPk, Guid regMatPk, RegistoMaterial regMat)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/RegMat/ByAssis/{assistenciaPk}/{regMatPk}", regMat);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar RegistoMaterial: {ex.Message}");
                throw;
            }
        }
    }
}
