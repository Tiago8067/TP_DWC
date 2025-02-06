using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.RegistoFotograficoService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegFotController : ControllerBase
    {
        private readonly IRegFotService _regFotService;

        public RegFotController(IRegFotService regFotService)
        {
            _regFotService = regFotService;
        }

        [HttpGet("{assistenciaPk}/{regFotPk}")]
        public async Task<ActionResult<Assistencia>> GetRegFotByIdCliente(int assistenciaPk, Guid regFotPk)
        {
            var result = await _regFotService.GetRegFotByIdCliente(assistenciaPk, regFotPk);

            if (result == null)
            {
                return NotFound("Esta Registo fotográfico não existe para a assistencia especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByAssis/{assistenciaPk}")]
        public async Task<ActionResult<Assistencia>> AddRegFotToClient(int assistenciaPk, [FromBody] RegistoFotografico regFot)
        {
            try
            {
                var result = await _regFotService.AddRegFotToClient(assistenciaPk, regFot);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo fotográfico:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByAssis/{assistenciaPk}/{regFotPk}")]
        public async Task<IActionResult> UpdateRegFotToCliente(int assistenciaPk, Guid regFotPk, [FromBody] RegistoFotografico regFot)
        {
            if (regFotPk != regFot.PK_RegistoFotografico)
            {
                return BadRequest("O ID Registo fotográfico não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _regFotService.UpdateRegFotToCliente(assistenciaPk, regFotPk, regFot);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar Registo fotográfico");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo fotográfico:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByAssis/{assistenciaPk}/{regFotPk}")]
        public async Task<IActionResult> DeleteRegFotToCliente(int assistenciaPk, Guid regFotPk)
        {
            var result = await _regFotService.DeleteRegFotToCliente(assistenciaPk, regFotPk);

            if (result == null)
            {
                return NotFound("Registo fotográfico não encontrada para a assistencia especificado.");
            }

            return Ok(result);
        }


    }
}
