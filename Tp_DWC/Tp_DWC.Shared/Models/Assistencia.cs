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
    public class Assistencia
    {
        [Key]
        public int NumeroInterno { get; set; } // Gerado pelo sistema

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime DataPrevisaoResolucao { get; set; }

        public DateTime DataPrevisaoEntrega { get; set; }

        public DateTime? DataConclusao { get; set; } // Opcional

        [MaxLength(200)]
        public string DescricaoProduto { get; set; }

        public string DescricaoProblema { get; set; }

        public string Observacoes { get; set; }

        //[Required]
        //public string Estado { get; set; } // Resolvido, Pendente, etc.
        // Relacionamento com a tabela que vai ter os Estados possiveis
        [Required]
        [ForeignKey("Estado")]
        public Guid EstadoId { get; set; } // Estado atual da assistência
        public Estado Estado { get; set; }

        // Relacionamentos
        public List<RegistoFotografico> RegistosFotograficos { get; set; } = new List<RegistoFotografico>();

        public List<RegistoMaoDeObra> RegistosMaoDeObra { get; set; } = new List<RegistoMaoDeObra>();

        public List<RegistoMaterial> RegistosMateriais { get; set; } = new List<RegistoMaterial>();
        
        public List<MudancaEstado> MudancasEstado { get; set; } = new List<MudancaEstado>();

        // Relacionamento com Cliente
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
