using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.ContactoService
{
    public class ContactoService : IContactoService
    {
        private readonly HttpClient _httpClient;

        public ContactoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Contacto>?> AllContactos()
        {
            try
            {
                var response = await _httpClient.GetStreamAsync("api/Contacto");

                var contactos = await JsonSerializer.DeserializeAsync<IEnumerable<Contacto>>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return contactos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<Contacto?> GetContacto(Guid pk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Contacto/{pk}");

                var contacto = await JsonSerializer.DeserializeAsync<Contacto>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return contacto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddContacto(Contacto contacto, Guid idCliente)
        {
            try
            {
                contacto.ClienteId = idCliente;

                var itemJson = new StringContent(JsonSerializer.Serialize(contacto), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Contacto", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateContacto(Guid pk, Contacto contacto)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(contacto), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Contacto/{pk}", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteContacto(Guid pk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Contacto/{pk}");

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
