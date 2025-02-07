using Microsoft.AspNetCore.Mvc;
using Tp_DWC.Shared.Models.EstadoModels;
using Tp_DWC.Web.Services.EstadoService;

namespace Tp_DWC.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Estado>>> GetAllEstados()
        {
            var estados = await _estadoService.GetAllEstados();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetEstadoDescricao(Guid id)
        {
            var descricao = await _estadoService.GetEstadoDescricaoById(id);
            if (descricao == null)
            {
                return NotFound("Estado não encontrado.");
            }
            return Ok(descricao);
        }

    }
}
