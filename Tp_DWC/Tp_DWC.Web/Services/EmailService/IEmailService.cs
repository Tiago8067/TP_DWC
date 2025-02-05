using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.EmailService
{
    public interface IEmailService
    {
        Task<Email?> GetEmailByIdCliente(Guid clientePk, Guid emailPk);
        Task<List<Email>> AddEmailToClient(Guid clientePk, Email email);
        Task<List<Email>?> UpdateEmailToCliente(Guid clientePk, Guid emailPk, Email emailRequest);
        Task<List<Email>?> DeleteEmailToCliente(Guid clientePk, Guid emailPk);
    }
}
