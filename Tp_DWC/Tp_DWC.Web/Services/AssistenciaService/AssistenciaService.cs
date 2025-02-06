using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.EmailService;

namespace Tp_DWC.Web.Services.AssistenciaService
{
    public class AssistenciaService : IAssistenciaService
    {
        private readonly ApplicationDbContext _context;

        public AssistenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Assistencia?> GetAssistenciaByIdCliente(Guid clientePk, int assistenciaPk)
        {
            return await _context.Assistencias
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.NumeroInterno == assistenciaPk);
        }

        public async Task<List<Assistencia>> AddAssistenciaToClient(Guid clientePk, Assistencia assistencia)
        {
            // Garante que a morada fique associada ao cliente passado na URL
            assistencia.ClienteId = clientePk;

            //estado por defeito para testar no inicio
            assistencia.EstadoId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            _context.Assistencias.Add(assistencia);
            await _context.SaveChangesAsync();

            return await _context.Assistencias.ToListAsync();
        }

        public async Task<List<Assistencia>?> UpdateAssistenciaToCliente(Guid clientePk, int assistenciaPk, Assistencia assistenciaRequest)
        {
            // Buscar a morada existente
            var existingAssistencia = await _context.Assistencias
                .FirstOrDefaultAsync(m => m.NumeroInterno == assistenciaPk && m.ClienteId == clientePk);

            if (existingAssistencia == null)
            {
                return await _context.Assistencias.ToListAsync();
            }

            // Atualiza os campos
            //existingAssistencia.DataCriacao = assistenciaRequest.DataCriacao;
            existingAssistencia.DataPrevisaoResolucao = assistenciaRequest.DataPrevisaoResolucao;
            existingAssistencia.DataPrevisaoEntrega = assistenciaRequest.DataPrevisaoEntrega;
            //se estado for resolvido adicionar data de conclusão
            //existingAssistencia.DataConclusao = assistenciaRequest.DataConclusao; 
            existingAssistencia.DescricaoProduto = assistenciaRequest.DescricaoProduto;
            existingAssistencia.DescricaoProblema = assistenciaRequest.DescricaoProblema;
            existingAssistencia.Observacoes = assistenciaRequest.Observacoes;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.Assistencias.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateMoradaToCliente: {ex.Message}");
                return await _context.Assistencias.ToListAsync();
            }
        }

        public async Task<List<Assistencia>?> DeleteAssistenciaToCliente(Guid clientePk, int assistenciaPk)
        {
            var assistencia = await _context.Assistencias
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.NumeroInterno == assistenciaPk);

            if (assistencia == null) return null;

            _context.Assistencias.Remove(assistencia);
            await _context.SaveChangesAsync();
            return await _context.Assistencias.ToListAsync();
        }
    }
}
