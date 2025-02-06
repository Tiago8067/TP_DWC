using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.RegistoMaoDeObraService
{
    public interface IRegMaoService
    {
        Task<RegistoMaoDeObra?> GetRegMaoByIdCliente(int assistenciaPk, Guid regMaoPk);
        Task<List<RegistoMaoDeObra>> AddRegMaoToClient(int assistenciaPk, RegistoMaoDeObra regMao);
        Task<List<RegistoMaoDeObra>?> UpdateRegMaoToCliente(int assistenciaPk, Guid regMaoPk, RegistoMaoDeObra regMaoRequest);
        Task<List<RegistoMaoDeObra>?> DeleteRegMaoToCliente(int assistenciaPk, Guid regMaoPk);
    }
}
