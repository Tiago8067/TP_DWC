using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.TiposModels;
using System.Text.Json.Serialization;

namespace Tp_DWC.Shared.Models
{
    public class Email
    {
        [Key]
        public Guid PK_Email { get; set; } = Guid.NewGuid();

        public string TipoEmail { get; set; }

        //[Required]
        //[EmailAddress]
        public string EnderecoEmail { get; set; }

        // Relacionamento com Tipo de Morada
        /*[ForeignKey("TipoEmail")]
        public int TipoEmailId { get; set; }
        public TipoEmail TipoEmail { get; set; }*/

        // Relacionamento com Cliente
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        [JsonIgnore]  // Isso evita que o ASP.NET Core valide ou serializa essa propriedade
        public Cliente? Cliente { get; set; }
    }
}
