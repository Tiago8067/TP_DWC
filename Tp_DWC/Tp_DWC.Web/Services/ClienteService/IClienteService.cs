using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.ClienteService
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllClientes();
        Task<Cliente?> GetClientesById(Guid pk);
        Task<List<Cliente>> AddCliente(Cliente cliente);
        Task<List<Cliente>?> UpdateCliente(Guid pk, Cliente clienteRequest);
        Task<List<Cliente>?> DeleteCliente(Guid pk);
    }
}
