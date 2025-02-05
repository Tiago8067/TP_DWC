using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.Services.ContactoService
{
    public interface IContactoService
    {
        Task<List<Contacto>> GetAllContactos();
        Task<Contacto?> GetContactoById(Guid pk);
        Task<List<Contacto>> AddContacto(Contacto contacto);
        Task<List<Contacto>?> UpdateContacto(Guid pk, Contacto contactoRequest);
        Task<List<Contacto>?> DeleteContacto(Guid pk);
        Task<Contacto?> GetContactoByIdCliente(Guid pk);

        Task<Contacto?> GetContactoByIdCliente(Guid clientePk, Guid contactoPk);
        Task<List<Contacto>> AddContactoToClient(Guid clientePk, Contacto contacto);
        Task<List<Contacto>?> UpdateContactoToCliente(Guid clientePk, Guid contactoPk, Contacto contactoRequest);
        Task<List<Contacto>?> DeleteContactoToCliente(Guid clientePk, Guid contactoPk);
    }
}
