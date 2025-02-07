using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Models.EstadoModels;
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
                .Include(m => m.RegistosFotograficos)
                .Include(m => m.RegistosMaoDeObra)
                .Include(m => m.RegistosMateriais)
                //.Include(a => a.Estado)
                .Include(m => m.MudancasEstado)
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.NumeroInterno == assistenciaPk);
        }

        public async Task<List<Assistencia>> AddAssistenciaToClient(Guid clientePk, Assistencia assistencia)
        {
            // Garante que a morada fique associada ao cliente passado na URL
            assistencia.ClienteId = clientePk;

            //estado por defeito para testar no inicio
            //assistencia.EstadoId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            //Guid estadoResolvido = Guid.Parse("44444444-4444-4444-4444-444444444444");
            //if(assistencia.EstadoId == estadoResolvido)
            //{
            //    assistencia.DataConclusao = DateTime.Now;
            //}

            _context.Assistencias.Add(assistencia);
            await _context.SaveChangesAsync();

            return await _context.Assistencias.ToListAsync();
        }

        public async Task<Assistencia?> AddAssistenciaToClientv2(Guid clientePk, Assistencia assistencia)
        {
            assistencia.ClienteId = clientePk;
            _context.Assistencias.Add(assistencia);
            await _context.SaveChangesAsync();

            // Opcional: se por algum motivo o objeto não estiver atualizado, você pode recarregá-lo:
            await _context.Entry(assistencia).ReloadAsync();

            return assistencia;
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
            //Guid estadoResolvido = Guid.Parse("44444444-4444-4444-4444-444444444444");
            //if (assistenciaRequest.EstadoId == estadoResolvido)
            //{
            //    assistenciaRequest.DataConclusao = DateTime.Now;
            //}
            assistenciaRequest.DataConclusao = assistenciaRequest.DataConclusao;
            existingAssistencia.DescricaoProduto = assistenciaRequest.DescricaoProduto;
            existingAssistencia.DescricaoProblema = assistenciaRequest.DescricaoProblema;
            existingAssistencia.Observacoes = assistenciaRequest.Observacoes;
            existingAssistencia.EstadoId = assistenciaRequest.EstadoId;

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

        public async Task<List<string>> GetContaCorrente(Guid estadoId, Guid clientePk)
        {
            return await _context.Assistencias
                .Where(a => a.ClienteId == clientePk && a.EstadoId == estadoId)
                .Select(a => $"{a.NumeroInterno} | {a.DataCriacao:dd/MM/yyyy} | {(a.DataConclusao.HasValue ? a.DataConclusao.Value.ToString("dd/MM/yyyy") : "N/A")}")
                .ToListAsync();
        }


    }
}
