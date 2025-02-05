using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.EmailService
{
    public interface IAssistenciaService
    {
        Task<Assistencia?> GetAssistenciaByIdCliente(Guid clientePk, int assistenciaPk);
        Task<List<Assistencia>> AddAssistenciaToClient(Guid clientePk, Assistencia assistencia);
        Task<List<Assistencia>?> UpdateAssistenciaToCliente(Guid clientePk, int assistenciaPk, Assistencia assistenciaRequest);
        Task<List<Assistencia>?> DeleteAssistenciaToCliente(Guid clientePk, int assistenciaPk);
    }
}
