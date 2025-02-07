using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Shared.Services.EstadoService
{
    public interface IEstadoService
    {
        Task<IEnumerable<Estado>?> GetAllEstados();
        Task<string?> GetEstadoDescricaoById(Guid id);
    }
}
