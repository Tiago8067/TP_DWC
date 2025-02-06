using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.RegistoMaterialService
{
    public class RegMatService : IRegMatService
    {
        private readonly ApplicationDbContext _context;

        public RegMatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistoMaterial?> GetRegMatByIdCliente(int assistenciaPk, Guid regMatPk)
        {
            return await _context.RegistoMaterials
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaterial == regMatPk);
        }

        public async Task<List<RegistoMaterial>> AddRegMatToClient(int assistenciaPk, RegistoMaterial regMat)
        {
            regMat.AssistenciaId = assistenciaPk;
            regMat.DataRegisto = DateTime.Now;

            _context.RegistoMaterials.Add(regMat);
            await _context.SaveChangesAsync();

            return await _context.RegistoMaterials.ToListAsync();
        }

        public async Task<List<RegistoMaterial>?> DeleteRegMatToCliente(int assistenciaPk, Guid regMatPk)
        {
            var result = await _context.RegistoMaterials
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaterial == regMatPk);

            if (result == null) return null;

            _context.RegistoMaterials.Remove(result);
            await _context.SaveChangesAsync();
            return await _context.RegistoMaterials.ToListAsync();
        }

        public async Task<List<RegistoMaterial>?> UpdateRegMatToCliente(int assistenciaPk, Guid regMatPk, RegistoMaterial regMatRequest)
        {
            var existing = await _context.RegistoMaterials
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaterial == regMatPk);

            if (existing == null)
            {
                return await _context.RegistoMaterials.ToListAsync();
            }

            // Atualiza os campos
            existing.QuantidadeMaterial = regMatRequest.QuantidadeMaterial;
            existing.Descricao = regMatRequest.Descricao;
            existing.PrecoUnitario = regMatRequest.PrecoUnitario;
            //existing.PrecoTotal = regMaoRequest.PrecoTotal;
            existing.Observacoes = regMatRequest.Observacoes;
            existing.DataRegisto = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.RegistoMaterials.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateRegMatToCliente: {ex.Message}");
                return await _context.RegistoMaterials.ToListAsync();
            }
        }
    }
}
