using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models;
using Tp_DWC.Web.Services.ClienteService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
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
        public async Task<ActionResult<List<Cliente>>> AddCliente(Cliente cliente)
        {
            try
            {
                var result = await _clienteService.AddCliente(cliente);
                return Ok(result);
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
    }
}
