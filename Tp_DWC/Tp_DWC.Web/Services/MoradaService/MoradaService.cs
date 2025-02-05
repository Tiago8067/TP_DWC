using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.MoradaService
{
    public class MoradaService : IMoradaService
    {
        private readonly ApplicationDbContext _context;

        public MoradaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Morada>> GetAllMoradas()
        {
            return await _context.Moradas.Include(m => m.Cliente).ToListAsync();
        }

        public async Task<Morada?> GetMoradasById(Guid pk)
        {
            return await _context.Moradas.Include(m => m.Cliente).FirstOrDefaultAsync(m => m.PK_Morada == pk);
        }

        public async Task<Morada?> GetMoradaByIdCliente(Guid pk)
        {
            return await _context.Moradas.FirstOrDefaultAsync(m => m.ClienteId == pk);
        }

        public async Task<List<Morada>> AddMorada(Morada morada)
        {
            // Verifique se o cliente existe no banco de dados
            var cliente = await _context.Clientes.FindAsync(morada.ClienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            _context.Moradas.Add(morada);
            await _context.SaveChangesAsync();
            //return await _context.Moradas.ToListAsync();

            // Retorna todas as moradas associadas ao cliente
            return await _context.Moradas
                    .Where(m => m.ClienteId == morada.ClienteId)
                    .ToListAsync(); 
        }

        public async Task<List<Morada>?> UpdateMorada(Guid pk, Morada moradaRequest)
        {
            var existing = await _context.Moradas.FindAsync(pk);
            if (existing == null) return null;

            existing.NomeMorada = moradaRequest.NomeMorada;
            existing.MoradaCompleta = moradaRequest.MoradaCompleta;
            existing.Empresa = moradaRequest.Empresa;
            existing.TipoMorada = moradaRequest.TipoMorada;
            existing.CodigoPostal = moradaRequest.CodigoPostal;
            existing.Localidade = moradaRequest.Localidade;
            existing.Pais = moradaRequest.Pais;
            existing.NIF = moradaRequest.NIF;

            await _context.SaveChangesAsync();
            return await _context.Moradas.ToListAsync();
        }

        public async Task<List<Morada>?> DeleteMorada(Guid pk)
        {
            var existing = await _context.Moradas.FindAsync(pk);
            if (existing == null) return null;

            _context.Moradas.Remove(existing);
            await _context.SaveChangesAsync();
            return await _context.Moradas.ToListAsync();
        }
    }
}
