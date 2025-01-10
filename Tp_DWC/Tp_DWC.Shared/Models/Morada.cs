using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models.TiposModels;

namespace Tp_DWC.Shared.Models
{
    public class Morada
    {
        [Key]
        public Guid PK_Morada { get; set; } = Guid.NewGuid();

        public string NomeMorada { get; set; }

        public string MoradaCompleta { get; set; }

        public string Empresa { get; set; }

        public string TipoMorada { get; set; }

        public string CodigoPostal { get; set; }

        public string Localidade { get; set; }

        public string Pais { get; set; }

        [MaxLength(9)]
        public string NIF { get; set; }

        // Relacionamento com Tipo de Morada
        /*[ForeignKey("TipoMorada")]
        public int TipoMoradaId { get; set; }
        public TipoMorada TipoMorada { get; set; }*/

        // Relacionamento com Cliente
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
