using System.Drawing;
using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.RegistoMaoDeObraService
{
    public class RegMaoService : IRegMaoService
    {
        private readonly ApplicationDbContext _context;

        public RegMaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistoMaoDeObra?> GetRegMaoByIdCliente(int assistenciaPk, Guid regMaoPk)
        {
            return await _context.RegistoMaoDeObras
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaoDeObra == regMaoPk);
        }

        public async Task<List<RegistoMaoDeObra>> AddRegMaoToClient(int assistenciaPk, RegistoMaoDeObra regMao)
        {
            regMao.AssistenciaId = assistenciaPk;
            regMao.DataRegisto = DateTime.Now;

            _context.RegistoMaoDeObras.Add(regMao);
            await _context.SaveChangesAsync();

            return await _context.RegistoMaoDeObras.ToListAsync();
        }

        public async Task<List<RegistoMaoDeObra>?> DeleteRegMaoToCliente(int assistenciaPk, Guid regMaoPk)
        {
            var result = await _context.RegistoMaoDeObras
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaoDeObra == regMaoPk);

            if (result == null) return null;

            _context.RegistoMaoDeObras.Remove(result);
            await _context.SaveChangesAsync();
            return await _context.RegistoMaoDeObras.ToListAsync();
        }

        public async Task<List<RegistoMaoDeObra>?> UpdateRegMaoToCliente(int assistenciaPk, Guid regMaoPk, RegistoMaoDeObra regMaoRequest)
        {
            var existing = await _context.RegistoMaoDeObras
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoMaoDeObra == regMaoPk);

            if (existing == null)
            {
                return await _context.RegistoMaoDeObras.ToListAsync();
            }

            // Atualiza os campos
            existing.QuantidadeHoras = regMaoRequest.QuantidadeHoras;
            existing.Descricao = regMaoRequest.Descricao;
            existing.PrecoUnitario = regMaoRequest.PrecoUnitario;
            //existing.PrecoTotal = regMaoRequest.PrecoTotal;
            existing.Observacoes = regMaoRequest.Observacoes;
            existing.DataRegisto = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.RegistoMaoDeObras.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateRegMaoToCliente: {ex.Message}");
                return await _context.RegistoMaoDeObras.ToListAsync();
            }
        }
    }
}
