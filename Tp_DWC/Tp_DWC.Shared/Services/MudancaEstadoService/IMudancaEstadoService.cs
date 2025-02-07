using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.MudancaEstadoService
{
    public interface IMudancaEstadoService
    {
        Task<MudancaEstado?> GetMudEstByIdCliente(int assistenciaPk, Guid mudEstPk);
        Task<bool> AddMudEstToClient(int assistenciaPk, MudancaEstado mudEst);
    }
}
