using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_DWC.Shared.Models.TiposModels
{
    public class TipoMorada
    {
        [Key]
        public Guid PK_TipoMorada { get; set; } = Guid.NewGuid();

        public string Descricao_Tipo_Morada;
    }
}
