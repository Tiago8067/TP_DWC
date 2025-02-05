using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.TiposModels;

namespace Tp_DWC.Shared.Models
{
    public class Contacto
    {
        [Key]
        public Guid PK_Contacto { get; set; } = Guid.NewGuid();

        public string TipoContacto { get; set; }

        public string Numero { get; set; }

        // Relacionamento com Tipo de Morada
        /*[ForeignKey("TipoContacto")]
        public int TipoContactoId { get; set; }
        public TipoContacto TipoContacto { get; set; }*/

        // Relacionamento com Cliente
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        [JsonIgnore]  // Isso evita que o ASP.NET Core valide ou serializa essa propriedade
        public Cliente? Cliente { get; set; }
    }
}
