using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.EmailService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("{clientePk}/{emailPk}")]
        public async Task<ActionResult<Contacto>> GetEmailById(Guid clientePk, Guid emailPk)
        {
            var result = await _emailService.GetEmailByIdCliente(clientePk, emailPk);

            if (result == null)
            {
                return NotFound("Esta email não existe para o cliente especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByCliente/{clientePk}")]
        public async Task<ActionResult<Contacto>> AddEmailToCliente(Guid clientePk, [FromBody] Email email)
        {
            try
            {
                var result = await _emailService.AddEmailToClient(clientePk, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar email:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByCliente/{clientePk}/{emailPk}")]
        public async Task<IActionResult> UpdateEmailToCliente(Guid clientePk, Guid emailPk, [FromBody] Email email)
        {
            if (emailPk != email.PK_Email)
            {
                return BadRequest("O ID email não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _emailService.UpdateEmailToCliente(clientePk, emailPk, email);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar o Email");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Email:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByCliente/{clientePk}/{emailPk}")]
        public async Task<IActionResult> DeleteEmailForCliente(Guid clientePk, Guid emailPk)
        {
            var result = await _emailService.DeleteEmailToCliente(clientePk, emailPk);

            if (result == null)
            {
                return NotFound("eMAIL não encontrada para o cliente especificado.");
            }

            return Ok(result);
        }

    }
}
