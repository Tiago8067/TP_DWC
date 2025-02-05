using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;

namespace Tp_DWC.Shared.Services.ContactoService
{
    public interface IContactoService
    {
        Task<IEnumerable<Contacto>?> AllContactos();
        Task<Contacto?> GetContacto(Guid pk);
        Task<bool> AddContacto(Contacto contacto, Guid idCliente);
        Task<bool> UpdateContacto(Guid pk, Contacto contacto);
        Task<bool> DeleteContacto(Guid pk);

        Task<Contacto?> GetContactoByIdCliente(Guid clientePk, Guid contactoPk);
        Task<bool> DeleteContactoToCliente(Guid clientePk, Guid contactoPk);
        Task<bool> AddContactoToCliente(Guid clientePk, Contacto contacto);
        Task<bool> UpdateContactoToCliente(Guid clientePk, Guid contactoPk, Contacto contacto);
    }
}
