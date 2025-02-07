using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.MudancaEstadoService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MudancaEstadoController : ControllerBase
    {
        private readonly IMudancaEstadoService _mudancaEstadoService;

        public MudancaEstadoController(IMudancaEstadoService mudancaEstadoService)
        {
            _mudancaEstadoService = mudancaEstadoService;
        }

        [HttpGet("{assistenciaPk}/{mudEstPk}")]
        public async Task<ActionResult<MudancaEstado>> GetMudEstByIdCliente(int assistenciaPk, Guid mudEstPk)
        {
            var result = await _mudancaEstadoService.GetMudEstByIdCliente(assistenciaPk, mudEstPk);

            if (result == null)
            {
                return NotFound("Esta Mudança de Estado não existe para a assistencia especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByAssis/{assistenciaPk}")]
        public async Task<ActionResult<MudancaEstado>> AddRegMaoToClient(int assistenciaPk, [FromBody] MudancaEstado mudEst)
        {
            try
            {
                var result = await _mudancaEstadoService.AddMudEstToClient(assistenciaPk, mudEst);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Mudança de Estado :" + ex.Message);
                throw;
            }
        }


    }
}
