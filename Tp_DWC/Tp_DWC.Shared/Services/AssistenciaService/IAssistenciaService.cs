using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.AssistenciaService
{
    public interface IAssistenciaService
    {
        Task<Assistencia?> GetAssistenciaByIdCliente(Guid clientePk, int assistenciaPk);
        Task<bool> DeleteAssistenciaToCliente(Guid clientePk, int assistenciaPk);
        Task<bool> AddAssistenciaToCliente(Guid clientePk, Assistencia assistencia);
        Task<bool> UpdateAssistenciaToCliente(Guid clientePk, int assistenciaPk, Assistencia assistencia);
    }
}
