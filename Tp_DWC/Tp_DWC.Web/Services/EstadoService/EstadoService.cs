using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Web.Services.EstadoService
{
    public class EstadoService : IEstadoService
    {
        private readonly ApplicationDbContext _context;

        public EstadoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetAllEstados()
        {
            return await _context.Estados.ToListAsync();
        }

        public async Task<string?> GetEstadoDescricaoById(Guid id)
        {
            return await _context.Estados
                         .Where(e => e.PK_Estado == id)
                         .Select(e => e.Descricao)
                         .FirstOrDefaultAsync();
        }
    }
}
