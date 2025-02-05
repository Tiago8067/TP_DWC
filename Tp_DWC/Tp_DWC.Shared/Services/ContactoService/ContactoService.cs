using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        #region base 

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

        #endregion


        #region crud pelo cliente especificado

        public async Task<Contacto?> GetContactoByIdCliente(Guid clientePk, Guid contactoPk)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/Contacto/{clientePk}/{contactoPk}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora ciclos de referência
                };

                var contacto = await JsonSerializer.DeserializeAsync<Contacto>(response, options);

                return contacto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteContactoToCliente(Guid clientePk, Guid contactoPk)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Contacto/ByCliente/{clientePk}/{contactoPk}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir Contacto: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddContactoToCliente(Guid clientePk, Contacto contacto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Contacto/ByCliente/{clientePk}", contacto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar Contacto: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateContactoToCliente(Guid clientePk, Guid moradaPk, Contacto contacto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Contacto/ByCliente/{clientePk}/{moradaPk}", contacto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar morada: {ex.Message}");
                throw;
            }
        }

        #endregion

    }
}
