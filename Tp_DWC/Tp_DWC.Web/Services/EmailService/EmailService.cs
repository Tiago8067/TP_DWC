using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;

        public EmailService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Email>> GetAllEmails()
        {
            return await _context.Emails.Include(m => m.Cliente).ToListAsync();
        }

        public async Task<Email?> GetEmailById(Guid pk)
        {
            return await _context.Emails.Include(m => m.Cliente).FirstOrDefaultAsync(m => m.PK_Email == pk);
        }

        public async Task<Email?> GetEmailByIdCliente(Guid pk)
        {
            return await _context.Emails.FirstOrDefaultAsync(m => m.ClienteId == pk);
        }

        public async Task<List<Email>> AddEmail(Email email)
        {
            // Verifique se o cliente existe no banco de dados
            var cliente = await _context.Emails.FindAsync(email.ClienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return await _context.Emails
                    .Where(m => m.ClienteId == email.ClienteId)
                    .ToListAsync(); 
        }

        public async Task<List<Email>?> UpdateEmail(Guid pk, Email emailRequest)
        {
            var existing = await _context.Emails.FindAsync(pk);
            if (existing == null) return null;

            existing.TipoEmail = emailRequest.TipoEmail;
            existing.EnderecoEmail = emailRequest.EnderecoEmail;

            await _context.SaveChangesAsync();
            return await _context.Emails.ToListAsync();
        }

        public async Task<List<Email>?> DeleteEmail(Guid pk)
        {
            var existing = await _context.Emails.FindAsync(pk);
            if (existing == null) return null;

            _context.Emails.Remove(existing);
            await _context.SaveChangesAsync();
            return await _context.Emails.ToListAsync();
        }
    }
}
