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
    }
}
