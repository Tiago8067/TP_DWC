using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Shared.Models
{
    public class MudancaEstado
    {
        [Key]
        public Guid PK_MudancaEstado { get; set; } = Guid.NewGuid();

        // Relacionamento com a tabela que vai ter os Estados possiveis
        [Required]
        [ForeignKey("EstadoAtual")]
        public Guid EstadoAtualId { get; set; }
        public Estado EstadoAtual { get; set; }

        // Relacionamento com a tabela que vai ter os Estados possiveis
        [Required]
        [ForeignKey("NovoEstado")]
        public Guid NovoEstadoId { get; set; }
        public Estado NovoEstado { get; set; }

        [Required]
        public DateTime DataMudanca { get; set; }

        // Relacionamento com Assistencia
        [Required]
        [ForeignKey("Assistencia")]
        public int AssistenciaId { get; set; }
        public Assistencia Assistencia { get; set; }
    }
}
