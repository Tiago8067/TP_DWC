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

        [HttpGet("{pk}")]
        public async Task<ActionResult<Morada>> GetMoradaById(Guid pk)
        {
            //var result = await _moradaService.GetMoradasById(pk);
            var result = await _moradaService.GetMoradaByIdCliente(pk);

            if (result == null)
            {
                return NotFound("Este Morada não Existe");
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
    }
}
