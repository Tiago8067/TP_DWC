using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Pages.PageCliente;

namespace Tp_DWC.Web.Services.ContactoService
{
    public class ContactoService : IContactoService
    {
        private readonly ApplicationDbContext _context;

        public ContactoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contacto>> GetAllContactos()
        {
            return await _context.Contactos.Include(m => m.Cliente).ToListAsync();
        }

        public async Task<Contacto?> GetContactoById(Guid pk)
        {
            return await _context.Contactos.Include(m => m.Cliente).FirstOrDefaultAsync(m => m.PK_Contacto == pk);
        }

        public async Task<Contacto?> GetContactoByIdCliente(Guid pk)
        {
            return await _context.Contactos.FirstOrDefaultAsync(m => m.ClienteId == pk);
        }

        public async Task<List<Contacto>> AddContacto(Contacto contacto)
        {
            // Verifique se o cliente existe no banco de dados
            var cliente = await _context.Contactos.FindAsync(contacto.ClienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return await _context.Contactos
                    .Where(m => m.ClienteId == contacto.ClienteId)
                    .ToListAsync(); 
        }

        public async Task<List<Contacto>?> UpdateContacto(Guid pk, Contacto contactoRequest)
        {
            var existing = await _context.Contactos.FindAsync(pk);
            if (existing == null) return null;

            existing.TipoContacto = contactoRequest.TipoContacto;
            existing.Numero = contactoRequest.Numero;

            await _context.SaveChangesAsync();
            return await _context.Contactos.ToListAsync();
        }

        public async Task<List<Contacto>?> DeleteContacto(Guid pk)
        {
            //var existing = await _context.Contactos.FindAsync(pk);
            var existing = await _context.Contactos.FirstOrDefaultAsync(m => m.ClienteId == pk);
            if (existing == null) return null;

            _context.Contactos.Remove(existing);
            await _context.SaveChangesAsync();
            return await _context.Contactos.ToListAsync();
        }
    }
}
