using Tp_DWC.Shared.Models;

namespace Tp_DWC.Web.DTO
{
    public class ClienteCompletoDTO
    {
        public Cliente Cliente { get; set; }
        public List<Morada> Moradas { get; set; }
        public List<Contacto> Contactos { get; set; }
        public List<Email> Emails { get; set; }
    }
}
