using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.EmailService
{
    public interface IEmailService
    {
        Task<List<Email>> GetAllEmails();
        Task<Email?> GetEmailById(Guid pk);
        Task<List<Email>> AddEmail(Email email);
        Task<List<Email>?> UpdateEmail(Guid pk, Email emailRequest);
        Task<List<Email>?> DeleteEmail(Guid pk);
        Task<Email?> GetEmailByIdCliente(Guid pk);
    }
}
