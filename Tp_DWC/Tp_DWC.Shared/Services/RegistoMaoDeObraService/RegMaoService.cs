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

namespace Tp_DWC.Shared.Services.RegistoMaoDeObraService
{
    public class RegMaoService :  IRegMaoService
    {
        private readonly HttpClient _httpClient;

        public RegMaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegistoMaoDeObra?> GetRegMaoByIdCliente(int assistenciaPk, Guid regMaoPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/RegMao/{assistenciaPk}/{regMaoPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var result = await JsonSerializer.DeserializeAsync<RegistoMaoDeObra>(response, options);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddRegMaoToClient(int assistenciaPk, RegistoMaoDeObra regMao)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/RegMao/ByAssis/{assistenciaPk}", regMao);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar RegistoMaoDeObra: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteRegMaoToCliente(int assistenciaPk, Guid regMaoPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/RegMao/ByAssis/{assistenciaPk}/{regMaoPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao eliminar RegistoMaoDeObra: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateRegMaoToCliente(int assistenciaPk, Guid regMaoPk, RegistoMaoDeObra regMao)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/RegMao/ByAssis/{assistenciaPk}/{regMaoPk}", regMao);
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
