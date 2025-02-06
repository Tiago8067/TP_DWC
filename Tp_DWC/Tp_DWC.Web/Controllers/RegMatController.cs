using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.RegistoMaterialService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegMatController : ControllerBase
    {
        private readonly IRegMatService _regMatService;

        public RegMatController(IRegMatService regMatService)
        {
            _regMatService = regMatService;
        }

        [HttpGet("{assistenciaPk}/{regMatPk}")]
        public async Task<ActionResult<RegistoMaterial>> GetRegMatByIdCliente(int assistenciaPk, Guid regMatPk)
        {
            var result = await _regMatService.GetRegMatByIdCliente(assistenciaPk, regMatPk);

            if (result == null)
            {
                return NotFound("Esta Registo de material não existe para a assistencia especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByAssis/{assistenciaPk}")]
        public async Task<ActionResult<RegistoMaterial>> AddRegMatToClient(int assistenciaPk, [FromBody] RegistoMaterial regMat)
        {
            try
            {
                var result = await _regMatService.AddRegMatToClient(assistenciaPk, regMat);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo de material:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByAssis/{assistenciaPk}/{regMatPk}")]
        public async Task<IActionResult> UpdateRegMatToCliente(int assistenciaPk, Guid regMatPk, [FromBody] RegistoMaterial regMat)
        {
            if (regMatPk != regMat.PK_RegistoMaterial)
            {
                return BadRequest("O ID Registo de material não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _regMatService.UpdateRegMatToCliente(assistenciaPk, regMatPk, regMat);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar Registo de material");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Registo de material:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByAssis/{assistenciaPk}/{regMatPk}")]
        public async Task<IActionResult> DeleteRegMaoToCliente(int assistenciaPk, Guid regMatPk)
        {
            var result = await _regMatService.DeleteRegMatToCliente(assistenciaPk, regMatPk);

            if (result == null)
            {
                return NotFound("Registo de material não encontrada para a assistencia especificado.");
            }

            return Ok(result);
        }


    }
}
