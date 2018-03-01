using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLL.Domain
{
    public class Amigo
    {
        public Amigo() { }

        [Key]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Sobrenome")]
        public string SobreNome { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "Entre com a data de aniversário")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Nascimento { get; set; }

        public bool CheckBoxAmigo { get; set; }
    }
}
