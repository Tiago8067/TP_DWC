using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Web.Services.EstadoService
{
    public interface IEstadoService
    {
        Task<List<Estado>> GetAllEstados();
    }
}
