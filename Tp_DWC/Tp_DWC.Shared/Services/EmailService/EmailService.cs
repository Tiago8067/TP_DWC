using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Email?> GetEmail(Guid pk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Email/{pk}");

                var email = await JsonSerializer.DeserializeAsync<Email>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return email;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddEmail(Email email, Guid idCliente)
        {
            try
            {
                email.ClienteId = idCliente;

                var itemJson = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Email", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateEmail(Guid pk, Email email)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Email/{pk}", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteEmail(Guid pk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Email/{pk}");

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
