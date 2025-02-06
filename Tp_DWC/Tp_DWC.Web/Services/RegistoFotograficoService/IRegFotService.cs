using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.RegistoFotograficoService
{
    public interface IRegFotService
    {
        Task<RegistoFotografico?> GetRegFotByIdCliente(int assistenciaPk, Guid regFotPk);
        Task<List<RegistoFotografico>> AddRegFotToClient(int assistenciaPk, RegistoFotografico regFot);
        Task<List<RegistoFotografico>?> UpdateRegFotToCliente(int assistenciaPk, Guid regFotPk, RegistoFotografico regFotRequest);
        Task<List<RegistoFotografico>?> DeleteRegFotToCliente(int assistenciaPk, Guid regFotPk);
    }
}
