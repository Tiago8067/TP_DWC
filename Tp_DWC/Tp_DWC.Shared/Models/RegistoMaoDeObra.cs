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
    public class RegistoMaoDeObra
    {
        [Key]
        public Guid PK_RegistoMaoDeObra { get; set; } = Guid.NewGuid();

        public int QuantidadeHoras { get; set; } // Quantidade (nº de horas)

        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoUnitario { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoTotal { get; set; }

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
