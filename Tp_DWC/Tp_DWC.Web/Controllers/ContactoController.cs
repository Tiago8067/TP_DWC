using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.ContactoService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _contactoService;

        public ContactoController(IContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        #region crud simples só baseado no contacto
        [HttpGet]
        public async Task<ActionResult<List<Contacto>>> GetAllContactos()
        {
            return await _contactoService.GetAllContactos();
        }

        [HttpGet("{pk}")]
        public async Task<ActionResult<Contacto>> GetContactoById(Guid pk)
        {
            //var result = await _contactoService.GetContactoById(pk);
            var result = await _contactoService.GetContactoByIdCliente(pk);

            if (result == null)
            {
                return NotFound("Este Contacto não Existe");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Contacto>>> AddContacto(Contacto contacto)
        {
            try
            {
                var result = await _contactoService.AddContacto(contacto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Morada:" + ex.Message);
                throw;
            }
        }

        [HttpPut("{pk}")]
        public async Task<ActionResult<List<Contacto>>> UpdateContacto(Guid pk, Contacto contacto)
        {
            var result = await _contactoService.UpdateContacto(pk, contacto);

            if (result == null)
            {
                return NotFound("Erro ao atualizar o Morada");
            }

            return Ok(result);
        }

        [HttpDelete("{pk}")]
        public async Task<ActionResult<List<Contacto>>> DeleteContacto(Guid pk)
        {
            var result = await _contactoService.DeleteContacto(pk);

            if (result == null)
            {
                return NotFound("Erro ao apagar o Morada");
            }

            return Ok(result);
        }

        #endregion


        #region crud pelo cliente especificado

        [HttpGet("{clientePk}/{contactoPk}")]
        public async Task<ActionResult<Contacto>> GetContactoById(Guid clientePk, Guid contactoPk)
        {
            var result = await _contactoService.GetContactoByIdCliente(clientePk, contactoPk);

            if (result == null)
            {
                return NotFound("Esta contacto não existe para o cliente especificado.");
            }

            return Ok(result);
        }

        [HttpPost("ByCliente/{clientePk}")]
        public async Task<ActionResult<Contacto>> AddContactoToCliente(Guid clientePk, [FromBody] Contacto contacto)
        {
            try
            {
                var result = await _contactoService.AddContactoToClient(clientePk, contacto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Morada:" + ex.Message);
                throw;
            }
        }

        [HttpPut("ByCliente/{clientePk}/{contactoPk}")]
        public async Task<IActionResult> UpdateContactoToCliente(Guid clientePk, Guid contactoPk, [FromBody] Contacto contacto)
        {
            if (contactoPk != contacto.PK_Contacto)
            {
                return BadRequest("O ID da morada não corresponde ao informado na URL.");
            }

            try
            {
                var result = await _contactoService.UpdateContactoToCliente(clientePk, contactoPk, contacto);
                if (result == null)
                {
                    return NotFound("Erro ao atualizar o Contacto");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Exceção ao adionar Contacto:" + ex.Message);
                throw;
            }
        }

        [HttpDelete("ByCliente/{clientePk}/{contactoPk}")]
        public async Task<IActionResult> DeleteContactoForCliente(Guid clientePk, Guid contactoPk)
        {
            var result = await _contactoService.DeleteContactoToCliente(clientePk, contactoPk);

            if (result == null)
            {
                return NotFound("Contacto não encontrada para o cliente especificado.");
            }

            return Ok(result);
        }

        #endregion

    }
}
