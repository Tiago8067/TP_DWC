using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.MoradaService
{
    public interface IMoradaService
    {
        Task<List<Morada>> GetAllMoradas();
        Task<Morada?> GetMoradasById(Guid pk);
        Task<List<Morada>> AddMorada(Morada morada);
        Task<List<Morada>?> UpdateMorada(Guid pk, Morada moradaRequest);
        Task<List<Morada>?> DeleteMorada(Guid pk);
        Task<Morada?> GetMoradaByIdCliente(Guid pk);
    }
}
