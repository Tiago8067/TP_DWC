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
        Task<Email?> GetEmail(Guid pk);
        Task<bool> AddEmail(Email email, Guid idCliente);
        Task<bool> UpdateEmail(Guid pk, Email email);
        Task<bool> DeleteEmail(Guid pk);
    }
}
