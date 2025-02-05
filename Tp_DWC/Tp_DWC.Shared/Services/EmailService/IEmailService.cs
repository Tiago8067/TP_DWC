using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.EmailService
{
    public interface IEmailService
    {
        Task<Email?> GetEmailByIdCliente(Guid clientePk, Guid emailPk);
        Task<bool> DeleteEmailToCliente(Guid clientePk, Guid emailPk);
        Task<bool> AddEmailToCliente(Guid clientePk, Email email);
        Task<bool> UpdateEmailToCliente(Guid clientePk, Guid emailPk, Email email);
    }
}
