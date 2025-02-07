using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.MudancaEstadoService
{
    public interface IMudancaEstadoService
    {
        Task<MudancaEstado?> GetMudEstByIdCliente(int assistenciaPk, Guid mudEstPk);
        Task<List<MudancaEstado>> AddMudEstToClient(int assistenciaPk, MudancaEstado mudEst);
    }
}
