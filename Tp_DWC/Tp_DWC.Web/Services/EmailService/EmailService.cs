using System.Diagnostics.Contracts;
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

        public async Task<Email?> GetEmailByIdCliente(Guid clientePk, Guid emailPk)
        {
            return await _context.Emails
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.PK_Email == emailPk);
        }

        public async Task<List<Email>> AddEmailToClient(Guid clientePk, Email email)
        {
            // Garante que a morada fique associada ao cliente passado na URL
            email.ClienteId = clientePk;

            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return await _context.Emails.ToListAsync();
        }

        public async Task<List<Email>?> UpdateEmailToCliente(Guid clientePk, Guid emailPk, Email emailRequest)
        {
            // Buscar a morada existente
            var existingEmail = await _context.Emails
                .FirstOrDefaultAsync(m => m.PK_Email == emailPk && m.ClienteId == clientePk);

            if (existingEmail == null)
            {
                return await _context.Emails.ToListAsync();
            }

            // Atualiza os campos
            existingEmail.TipoEmail = emailRequest.TipoEmail;
            existingEmail.EnderecoEmail = emailRequest.EnderecoEmail;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.Emails.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateMoradaToCliente: {ex.Message}");
                return await _context.Emails.ToListAsync();
            }
        }

        public async Task<List<Email>?> DeleteEmailToCliente(Guid clientePk, Guid emailPk)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.PK_Email == emailPk);

            if (email == null) return null;

            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
            return await _context.Emails.ToListAsync();
        }
    }
}
