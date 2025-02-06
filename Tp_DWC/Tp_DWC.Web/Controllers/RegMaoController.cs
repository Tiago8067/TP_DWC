using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.RegistoMaoDeObraService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegMaoController : ControllerBase
    {
        private readonly IRegMaoService _regMaoService;

        public RegMaoController(IRegMaoService regMaoService)
        {
            _regMaoService = regMaoService;
        }

        [HttpGet("{assistenciaPk}/{regMaoPk}")]
        public async Task<ActionResult<RegistoMaoDeObra>> GetRegMaoByIdCliente(int assistenciaPk, Guid regMaoPk)
        {
            var result = await _regMaoService.GetRegMaoByIdCliente(assistenciaPk, regMaoPk);

            if (result == null)
            {
                return NotFound("Esta Registo de mão de obra não existe para a assistencia especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByAssis/{assistenciaPk}")]
        public async Task<ActionResult<RegistoMaoDeObra>> AddRegMaoToClient(int assistenciaPk, [FromBody] RegistoMaoDeObra regMao)
        {
            try
            {
                var result = await _regMaoService.AddRegMaoToClient(assistenciaPk, regMao);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo de mão de obra:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByAssis/{assistenciaPk}/{regMaoPk}")]
        public async Task<IActionResult> UpdateRegMaoToCliente(int assistenciaPk, Guid regMaoPk, [FromBody] RegistoMaoDeObra regMao)
        {
            if (regMaoPk != regMao.PK_RegistoMaoDeObra)
            {
                return BadRequest("O ID Registo de mão de obra não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _regMaoService.UpdateRegMaoToCliente(assistenciaPk, regMaoPk, regMao);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar Registo de mão de obra");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo de mão de obra:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByAssis/{assistenciaPk}/{regMaoPk}")]
        public async Task<IActionResult> DeleteRegMaoToCliente(int assistenciaPk, Guid regMaoPk)
        {
            var result = await _regMaoService.DeleteRegMaoToCliente(assistenciaPk, regMaoPk);

            if (result == null)
            {
                return NotFound("Registo de mão de obra não encontrada para a assistencia especificado.");
            }

            return Ok(result);
        }


    }
}
