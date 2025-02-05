using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.DTO;

namespace Tp_DWC.Shared.Services.ClienteService
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>?> AllClientes();
        Task<Cliente?> GetCliente(Guid pk);
        Task<bool> AddCliente(Cliente cliente);
        Task<bool> UpdateCliente(Guid pk, Cliente cliente);
        Task<bool> DeleteCliente(Guid pk);
        Task<bool> AddClienteComDetalhes(ClienteCompletoDTO clienteCompleto);
    }
}
