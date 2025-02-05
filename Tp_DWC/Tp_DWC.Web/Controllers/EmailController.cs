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

        [HttpGet]
        public async Task<ActionResult<List<Email>>> GetAllEmails()
        {
            return await _emailService.GetAllEmails();
        }

        [HttpGet("{pk}")]
        public async Task<ActionResult<Email>> GetEmailById(Guid pk)
        {
            //var result = await _emailService.GetEmailById(pk);
            var result = await _emailService.GetEmailByIdCliente(pk);

            if (result == null)
            {
                return NotFound("Este Email não Existe");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Email>>> AddEmail(Email email)
        {
            try
            {
                var result = await _emailService.AddEmail(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Email:" + ex.Message);
                throw;
            }
        }

        [HttpPut("{pk}")]
        public async Task<ActionResult<List<Email>>> UpdateEmail(Guid pk, Email email)
        {
            var result = await _emailService.UpdateEmail(pk, email);

            if (result == null)
            {
                return NotFound("Erro ao atualizar o Email");
            }

            return Ok(result);
        }

        [HttpDelete("{pk}")]
        public async Task<ActionResult<List<Email>>> DeleteEmail(Guid pk)
        {
            var result = await _emailService.DeleteEmail(pk);

            if (result == null)
            {
                return NotFound("Erro ao apagar o Email");
            }

            return Ok(result);
        }
    }
}
