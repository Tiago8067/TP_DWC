using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using System.Net.Http.Json;

namespace Tp_DWC.Shared.Services.MudancaEstadoService
{
    public class MudancaEstadoService : IMudancaEstadoService
    {
        private readonly HttpClient _httpClient;

        public MudancaEstadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MudancaEstado?> GetMudEstByIdCliente(int assistenciaPk, Guid mudEstPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/MudancaEstado/{assistenciaPk}/{mudEstPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var result = await JsonSerializer.DeserializeAsync<MudancaEstado>(response, options);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddMudEstToClient(int assistenciaPk, MudancaEstado mudEst)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/MudancaEstado/ByAssis/{assistenciaPk}", mudEst);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar MudancaEstado: {ex.Message}");
                throw;
            }
        }


    }
}
