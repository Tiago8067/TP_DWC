using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.MoradaService
{
    public interface IMoradaService
    {
        //Task<IEnumerable<Morada>?> AllMoradas();
        Task<Morada?> GetMorada(Guid pk);
        Task<bool> AddMorada(Morada morada, Guid idCliente);
        Task<bool> UpdateMorada(Guid pk, Morada morada);
        Task<bool> DeleteMorada(Guid pk);
    }
}
