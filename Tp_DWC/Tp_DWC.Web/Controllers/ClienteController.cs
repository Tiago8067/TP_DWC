using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.ContactoService;
using Tp_DWC.Web.Services.EmailService;
using Tp_DWC.Web.Services.MoradaService;
using Tp_DWC.Web.Services.ClienteService;
using Tp_DWC.Web.DTO;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        //private readonly IMoradaService _moradaService;
        //private readonly IContactoService _contactoService;
        //private readonly IEmailService _emailService;

        //public ClienteController(IClienteService clienteService, IMoradaService moradaService, IContactoService contactoService, IEmailService emailService)
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
            //_moradaService = moradaService;
            //_contactoService = contactoService;
            //_emailService = emailService;
        }   

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAllClientes()
        {
            return await _clienteService.GetAllClientes();
        }

        [HttpGet("{pk}")]
        public async Task<ActionResult<Cliente>> GetClienteById(Guid pk)
        {
            var result = await _clienteService.GetClientesById(pk);

            if (result == null)
            {
                return NotFound("Este Cliente não Existe");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Cliente>>> AddCliente(Cliente cliente/*, Morada morada, Contacto contacto, Email email*/)
        {
            try
            {
                var resultCliente = await _clienteService.AddCliente(cliente);
                //var resultMorada = await _moradaService.AddMorada(morada);
                //var resultContacto = await _contactoService.AddContacto(contacto);
                //var resultEmail = await _emailService.AddEmail(email);
                //var result = resultCliente + resultMorada + resultContacto + resultEmail;
                //return Ok(result);
                return Ok(resultCliente);
            }
            catch (Exception ex) 
            {
                return BadRequest("Exceção ao adionar Cliente:" + ex.Message);
                throw;
            }
        }

        [HttpPut("{pk}")]
        public async Task<ActionResult<List<Cliente>>> UpdateCliente(Guid pk, Cliente cliente)
        {
            var result = await _clienteService.UpdateCliente(pk, cliente);

            if (result == null)
            {
                return NotFound("Erro ao atualizar o cliente");
            }

            return Ok(result);
        }

        [HttpDelete("{pk}")]
        public async Task<ActionResult<List<Cliente>>> DeleteCliente(Guid pk)
        {
            var result = await _clienteService.DeleteCliente(pk);

            if (result == null)
            {
                return NotFound("Erro ao apagar o cliente");
            }

            return Ok(result);
        }

        [HttpPost("adicionar-cliente-completo")]
        public async Task<ActionResult> AddClienteComDetalhes([FromBody] ClienteCompletoDTO clienteCompletoDto)
        {
            try
            {
                var cliente = clienteCompletoDto.Cliente;
                var moradas = clienteCompletoDto.Moradas;
                var contactos = clienteCompletoDto.Contactos;
                var emails = clienteCompletoDto.Emails;

                var resultado = await _clienteService.AddClienteComDetalhes(cliente, moradas, contactos, emails);

                if (resultado)
                {
                    return Ok("Cliente e detalhes adicionados com sucesso.");
                }
                return BadRequest("Erro ao adicionar cliente e detalhes.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exceção ao adicionar cliente: {ex.Message}");
            }
        }

    }
}
