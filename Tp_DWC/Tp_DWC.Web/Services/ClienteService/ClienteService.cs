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
            var cliente = await _context.Clientes
                                    .Include(c => c.Moradas) // Inclui os relacionamentos que forem necessários
                                    .Include(c => c.Contactos) 
                                    .Include(c => c.Emails) 
                                    .Include(c => c.Assistencias)
                                    .FirstOrDefaultAsync(c => c.PK_Cliente == pk);

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

        public async Task<bool> AddClienteComDetalhes(Cliente cliente, List<Morada> moradas, List<Contacto> contactos, List<Email> emails)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Adicionar o cliente
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                // Associar o ClienteId às moradas, contactos e emails
                foreach (var morada in moradas)
                {
                    morada.ClienteId = cliente.PK_Cliente;
                    _context.Moradas.Add(morada);
                }

                foreach (var contacto in contactos)
                {
                    contacto.ClienteId = cliente.PK_Cliente;
                    _context.Contactos.Add(contacto);
                }

                foreach (var email in emails)
                {
                    email.ClienteId = cliente.PK_Cliente;
                    _context.Emails.Add(email);
                }

                // Salvar alterações
                await _context.SaveChangesAsync();

                // Confirmar a transação
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Reverter transação em caso de erro
                await transaction.RollbackAsync();
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }

    }
}
