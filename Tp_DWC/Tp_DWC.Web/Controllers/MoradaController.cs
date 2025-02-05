using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.MoradaService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoradaController : ControllerBase
    {
        private readonly IMoradaService _moradaService;

        public MoradaController(IMoradaService moradaService)
        {
            _moradaService = moradaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Morada>>> GetAllMoradas()
        {
            return await _moradaService.GetAllMoradas();
        }

        [HttpGet("{clientePk}/{moradaPk}")]
        public async Task<ActionResult<Morada>> GetMoradaById(Guid clientePk, Guid moradaPk)
        {
            var result = await _moradaService.GetMoradaByIdCliente(clientePk, moradaPk);

            if (result == null)
            {
                return NotFound("Esta morada não existe para o cliente especificado.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Morada>>> AddMorada(Morada morada)
        {
            try
            {
                var result = await _moradaService.AddMorada(morada);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Morada:" + ex.Message);
                throw;
            }
        }

        [HttpPut("{pk}")]
        public async Task<ActionResult<List<Morada>>> UpdateMorada(Guid pk, Morada morada)
        {
            var result = await _moradaService.UpdateMorada(pk, morada);

            if (result == null)
            {
                return NotFound("Erro ao atualizar o Morada");
            }

            return Ok(result);
        }

        [HttpDelete("{pk}")]
        public async Task<ActionResult<List<Morada>>> DeleteMorada(Guid pk)
        {
            var result = await _moradaService.DeleteMorada(pk);

            if (result == null)
            {
                return NotFound("Erro ao apagar o Morada");
            }

            return Ok(result);
        }

        // POST api/Morada/ByCliente/{clientePk}
        // Insere uma nova morada para o cliente especificado
        [HttpPost("ByCliente/{clientePk}")]
        public async Task<ActionResult<Morada>> AddMoradaToCliente(Guid clientePk, [FromBody] Morada morada)
        {
            Console.WriteLine($"[DEBUG] Iniciando AddMoradaByClient para cliente: {clientePk}");
            if (!ModelState.IsValid)
            {
                // Retorna os erros de validação para facilitar o debug
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _moradaService.AddMoradaToClient(clientePk, morada);
                Console.WriteLine("[DEBUG] Morada adicionada com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Erro no AddMoradaByClient: {ex}");
                return BadRequest("Exceção ao adionar Morada:" + ex.Message);
                throw;
            }
        }

         //PUT api/Morada/ByCliente/{clientePk}/{moradaPk}
         //Atualiza uma morada que pertence ao cliente
        [HttpPut("ByCliente/{clientePk}/{moradaPk}")]
        public async Task<IActionResult> UpdateMoradaToCliente(Guid clientePk, Guid moradaPk, [FromBody] Morada morada)
        {
            if (moradaPk != morada.PK_Morada)
            {
                return BadRequest("O ID da morada não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _moradaService.UpdateMoradaToCliente(clientePk, moradaPk, morada);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar o Morada");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Morada:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByCliente/{clientePk}/{moradaPk}")]
        public async Task<IActionResult> DeleteMoradaForCliente(Guid clientePk, Guid moradaPk)
        {
            var result = await _moradaService.DeleteMoradaToCliente(clientePk, moradaPk);

            if (result == null)
            {
                return NotFound("Morada não encontrada para o cliente especificado.");
            }

            return Ok(result);
        }
    }
}
