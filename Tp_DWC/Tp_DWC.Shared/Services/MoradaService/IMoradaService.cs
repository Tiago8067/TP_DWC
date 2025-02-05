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
        Task<Morada?> GetMorada(Guid clientePk, Guid moradaPk);
        Task<bool> AddMorada(Morada morada, Guid idCliente);
        Task<bool> UpdateMorada(Guid pk, Morada morada);
        Task<bool> DeleteMoradaToCliente(Guid clientePk, Guid moradaPk);
        Task<bool> AddMoradaToCliente(Guid clientePk, Morada morada);
        Task<bool> UpdateMoradaToCliente(Guid clientePk, Guid moradaPk, Morada morada);
    }
}
