using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_DWC.Shared.Models.TiposModels
{
    public class TipoEmail
    {
        [Key]
        public Guid PK_TipoEmail { get; set; } = Guid.NewGuid();

        public string Descricao_Tipo_Email;
    }
}
