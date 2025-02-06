using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.RegistoMaterialService
{
    public interface IRegMatService
    {
        Task<RegistoMaterial?> GetRegMatByIdCliente(int assistenciaPk, Guid regMatPk);
        Task<List<RegistoMaterial>> AddRegMatToClient(int assistenciaPk, RegistoMaterial regMat);
        Task<List<RegistoMaterial>?> UpdateRegMatToCliente(int assistenciaPk, Guid regMatPk, RegistoMaterial regMatRequest);
        Task<List<RegistoMaterial>?> DeleteRegMatToCliente(int assistenciaPk, Guid regMatPk);
    }
}
