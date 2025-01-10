using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_DWC.Shared.Models
{
    public class Cliente
    {
        [Key]
        public Guid PK_Cliente { get; set; } = Guid.NewGuid(); //vai ser o Numero do cliente (gerado pelo sistema)

        public string Nome { get; set; }

        public string Website { get; set; }

        public bool Ativo { get; set; }

        // Relacionamentos
        public List<Morada> Moradas { get; set; } = new List<Morada>();

        public List<Contacto> Contactos { get; set; } = new List<Contacto>();

        public List<Email> Emails { get; set; } = new List<Email>();

        public List<Assistencia> Assistencias { get; set; } = new List<Assistencia>();



        //public string FuncionarioId { get; set; } = string.Empty; // Associado ao funcionário
    }
}
