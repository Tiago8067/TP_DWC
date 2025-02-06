using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tp_DWC.Shared.Models
{
    public class RegistoFotografico
    {
        [Key]
        public Guid PK_RegistoFotografico { get; set; } = Guid.NewGuid();

        public string Foto { get; set; } // Caminho ou URL da foto

        public string Observacoes { get; set; }

        [Required]
        public DateTime DataRegisto { get; set; }

        // Relacionamento com Assistencia
        [Required]
        [ForeignKey("Assistencia")]
        public int AssistenciaId { get; set; }
        [JsonIgnore]  // Isso evita que o ASP.NET Core valide ou serializa essa propriedade
        public Assistencia? Assistencia { get; set; }

    }
}
