using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.RegistoMaoDeObraService
{
    public interface IRegMaoService
    {
        Task<RegistoMaoDeObra?> GetRegMaoByIdCliente(int assistenciaPk, Guid regMaoPk);
        Task<bool> DeleteRegMaoToCliente(int assistenciaPk, Guid regMaoPk);
        Task<bool> AddRegMaoToClient(int assistenciaPk, RegistoMaoDeObra regMao);
        Task<bool> UpdateRegMaoToCliente(int assistenciaPk, Guid regMaoPk, RegistoMaoDeObra regMao);
    }
}
