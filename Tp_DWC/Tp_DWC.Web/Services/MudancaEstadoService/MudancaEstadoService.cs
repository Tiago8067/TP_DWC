using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.MudancaEstadoService
{
    public class MudancaEstadoService : IMudancaEstadoService
    {
        private readonly ApplicationDbContext _context;

        public MudancaEstadoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MudancaEstado?> GetMudEstByIdCliente(int assistenciaPk, Guid mudEstPk)
        {
            return await _context.MudancasEstado
                .Include(m => m.EstadoAtual)
                .Include(m => m.NovoEstado)
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_MudancaEstado == mudEstPk);
        }

        public async Task<List<MudancaEstado>> AddMudEstToClient(int assistenciaPk, MudancaEstado mudEst)
        {
            mudEst.AssistenciaId = assistenciaPk;
            mudEst.DataMudanca = DateTime.Now;

            _context.MudancasEstado.Add(mudEst);
            await _context.SaveChangesAsync();

            return await _context.MudancasEstado.ToListAsync();
        }
    }
}
