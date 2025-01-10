using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_DWC.Shared.Models.EstadoModels
{
    public class Estado
    {
        [Key]
        public Guid PK_Estado { get; set; } = Guid.NewGuid();

        [Required]
        public string Descricao { get; set; }

        // Relacionamentos
        public List<Assistencia> Assistencias { get; set; } = new List<Assistencia>();
        public List<MudancaEstado> MudancasEstado { get; set; } = new List<MudancaEstado>();
    }
}
