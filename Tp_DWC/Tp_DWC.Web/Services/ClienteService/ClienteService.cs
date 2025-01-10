using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.ClienteService
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetClientesById(Guid pk)
        {
            var cliente = await _context.Clientes.FindAsync(pk);

            if (cliente == null)
            {
                return null;
            }

            return cliente;
        }

        public async Task<List<Cliente>> AddCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return await _context.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<Cliente>?> UpdateCliente(Guid pk, Cliente clienteRequest)
        {
            var cliente = await _context.Clientes.FindAsync(pk);

            if (cliente == null)
            {
                return null;
            }

            cliente.Nome = clienteRequest.Nome;
            cliente.Website = clienteRequest.Website;
            cliente.Ativo = clienteRequest.Ativo;

            await _context.SaveChangesAsync();
            return await _context.Clientes.ToListAsync();
        }

        public async Task<List<Cliente>?> DeleteCliente(Guid pk)
        {
            var cliente = await _context.Clientes.FindAsync(pk);

            if (cliente == null)
            {
                return null;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return await _context.Clientes.ToListAsync();
        }
    }
}
