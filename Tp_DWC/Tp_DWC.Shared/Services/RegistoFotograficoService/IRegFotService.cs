using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.RegistoFotograficoService
{
    public interface IRegFotService
    {
        Task<RegistoFotografico?> GetRegFotByIdCliente(int assistenciaPk, Guid regFotPk);
        Task<bool> DeleteRegFotToCliente(int assistenciaPk, Guid regFotPk);
        Task<bool> AddRegFotToClient(int assistenciaPk, RegistoFotografico regFot);
        Task<bool> UpdateRegFotToCliente(int assistenciaPk, Guid regFotPk, RegistoFotografico regFot);
    }
}
