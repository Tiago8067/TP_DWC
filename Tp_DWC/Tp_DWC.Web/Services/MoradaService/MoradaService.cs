using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Pages.PageMorada;

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

        public async Task<Morada?> GetMoradaByIdCliente(Guid clientePk, Guid moradaPk)
        {
            return await _context.Moradas
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.PK_Morada == moradaPk);
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
            return await _context.Moradas.ToListAsync();

            // Retorna todas as moradas associadas ao cliente
            //return await _context.Moradas
            //        .Where(m => m.ClienteId == morada.ClienteId)
            //        .ToListAsync(); 
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
    
        public async Task<List<Morada>> AddMoradaToClient(Guid clientePk, Morada morada)
        {
            // Garante que a morada fique associada ao cliente passado na URL
            morada.ClienteId = clientePk;

            _context.Moradas.Add(morada);
            await _context.SaveChangesAsync();

            // Retorna o recurso criado com a rota de obtenção dele
            //return CreatedAtAction(nameof(GetMorada), new { pk = morada.PK_Morada }, morada);
            return await _context.Moradas.ToListAsync();
        }

        public async Task<List<Morada>?> UpdateMoradaToCliente(Guid clientePk, Guid moradaPk, Morada moradaRequest)
        {
            // Buscar a morada existente
            var existingMorada = await _context.Moradas
                .FirstOrDefaultAsync(m => m.PK_Morada == moradaPk && m.ClienteId == clientePk);

            if (existingMorada == null)
            {
                return await _context.Moradas.ToListAsync(); 
            }

            // Atualiza os campos
            existingMorada.NomeMorada = moradaRequest.NomeMorada;
            existingMorada.MoradaCompleta = moradaRequest.MoradaCompleta;
            existingMorada.Empresa = moradaRequest.Empresa;
            existingMorada.TipoMorada = moradaRequest.TipoMorada;
            existingMorada.CodigoPostal = moradaRequest.CodigoPostal;
            existingMorada.Localidade = moradaRequest.Localidade;
            existingMorada.Pais = moradaRequest.Pais;
            existingMorada.NIF = moradaRequest.NIF;

            try
            {
                await _context.SaveChangesAsync();
                return await _context.Moradas.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no UpdateMoradaToCliente: {ex.Message}");
                return await _context.Moradas.ToListAsync();
            }
        }

        public async Task<List<Morada>?> DeleteMoradaToCliente(Guid clientePk, Guid moradaPk)
        {
            var morada = await _context.Moradas
                .FirstOrDefaultAsync(m => m.ClienteId == clientePk && m.PK_Morada == moradaPk);

            if (morada == null) return null;

            _context.Moradas.Remove(morada);
            await _context.SaveChangesAsync();
            return await _context.Moradas.ToListAsync();
        }
    }
}
