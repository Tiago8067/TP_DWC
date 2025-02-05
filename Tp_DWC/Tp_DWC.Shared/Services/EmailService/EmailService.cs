using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Pages.PageMorada;

namespace Tp_DWC.Shared.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Email?> GetEmailByIdCliente(Guid clientePk, Guid emailPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Email/{clientePk}/{emailPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var email = await JsonSerializer.DeserializeAsync<Email>(response, options);

                return email;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddEmailToCliente(Guid clientePk, Email email)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Email/ByCliente/{clientePk}", email);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar Email: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateEmailToCliente(Guid clientePk, Guid emailPk, Email email)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Email/ByCliente/{clientePk}/{emailPk}", email);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar Email: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteEmailToCliente(Guid clientePk, Guid emailPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Email/ByCliente/{clientePk}/{emailPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir Email: {ex.Message}");
                throw;
            }
        }

    }
}
