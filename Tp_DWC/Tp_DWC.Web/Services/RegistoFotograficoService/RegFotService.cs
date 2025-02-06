using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Pages.PageCliente;

namespace Tp_DWC.Web.Services.RegistoFotograficoService
{
    public class RegFotService : IRegFotService
    {
        private readonly ApplicationDbContext _context;

        public RegFotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistoFotografico?> GetRegFotByIdCliente(int assistenciaPk, Guid regFotPk)
        {
            return await _context.RegistoFotograficos
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoFotografico == regFotPk);
        }

        public async Task<List<RegistoFotografico>> AddRegFotToClient(int assistenciaPk, RegistoFotografico regFot)
        {
            regFot.AssistenciaId = assistenciaPk;
            regFot.DataRegisto = DateTime.Now;

            _context.RegistoFotograficos.Add(regFot);
            await _context.SaveChangesAsync();

            return await _context.RegistoFotograficos.ToListAsync();
        }

        public async Task<List<RegistoFotografico>?> UpdateRegFotToCliente(int assistenciaPk, Guid regFotPk, RegistoFotografico regFotRequest)
        {
            var existing = await _context.RegistoFotograficos
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoFotografico == regFotPk);

            if (existing == null)
            {
                return await _context.RegistoFotograficos.ToListAsync();
            }

            // Atualiza os campos
            existing.Foto = regFotRequest.Foto;
            existing.Observacoes = regFotRequest.Observacoes;
            existing.DataRegisto = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.RegistoFotograficos.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateMoradaToCliente: {ex.Message}");
                return await _context.RegistoFotograficos.ToListAsync();
            }
        }

        public async Task<List<RegistoFotografico>?> DeleteRegFotToCliente(int assistenciaPk, Guid regFotPk)
        {
            var result = await _context.RegistoFotograficos
                .FirstOrDefaultAsync(m => m.AssistenciaId == assistenciaPk && m.PK_RegistoFotografico == regFotPk);

            if (result == null) return null;

            _context.RegistoFotograficos.Remove(result);
            await _context.SaveChangesAsync();
            return await _context.RegistoFotograficos.ToListAsync();
        }



    }
}
