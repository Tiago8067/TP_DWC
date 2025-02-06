using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.RegistoMaterialService
{
    public interface IRegMatService
    {
        Task<RegistoMaterial?> GetRegMatByIdCliente(int assistenciaPk, Guid regMatPk);
        Task<bool> DeleteRegMatToCliente(int assistenciaPk, Guid regMatPk);
        Task<bool> AddRegMatToClient(int assistenciaPk, RegistoMaterial regMat);
        Task<bool> UpdateRegMatToCliente(int assistenciaPk, Guid regMatPk, RegistoMaterial regMat);
    }
}
