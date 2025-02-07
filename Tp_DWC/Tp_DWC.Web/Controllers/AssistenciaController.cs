using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.EmailService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssistenciaController : ControllerBase
    {
        private readonly IAssistenciaService _assistenciaService;

        public AssistenciaController(IAssistenciaService service) 
        { 
            _assistenciaService = service;
        }

        [HttpGet("{clientePk}/{assistenciaPk}")]
        public async Task<ActionResult<Assistencia>> GetAssistenciaByIdCliente(Guid clientePk, int assistenciaPk)
        {
            var result = await _assistenciaService.GetAssistenciaByIdCliente(clientePk, assistenciaPk);

            if (result == null)
            {
                return NotFound("Esta assistencia não existe para o cliente especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByCliente/{clientePk}")]
        public async Task<ActionResult<Assistencia>> AddAssistenciaToClient(Guid clientePk, [FromBody] Assistencia assistencia)
        {
            try
            {
                //var result = await _assistenciaService.AddAssistenciaToClient(clientePk, assistencia);
                var result = await _assistenciaService.AddAssistenciaToClientv2(clientePk, assistencia);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar assistencia:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByCliente/{clientePk}/{assistenciaPk}")]
        public async Task<IActionResult> UpdateAssistenciaToCliente(Guid clientePk, int assistenciaPk, [FromBody] Assistencia assistencia)
        {
            if (assistenciaPk != assistencia.NumeroInterno)
            {
                return BadRequest("O ID assistencia não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _assistenciaService.UpdateAssistenciaToCliente(clientePk, assistenciaPk, assistencia);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar assistencia");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar assistencia:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByCliente/{clientePk}/{assistenciaPk}")]
        public async Task<IActionResult> DeleteAssistenciaToCliente(Guid clientePk, int assistenciaPk)
        {
            var result = await _assistenciaService.DeleteAssistenciaToCliente(clientePk, assistenciaPk);

            if (result == null)
            {
                return NotFound("assistencia não encontrada para o cliente especificado.");
            }

            return Ok(result);
        }


    }
}
