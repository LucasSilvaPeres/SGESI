using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models

{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        [DisplayAttribute(Name = "Nome")]
        public string Name { get; set; }

        [DisplayAttribute(Name = "Senha")]
        public string Password { get; set; }
        public string Salt { get; set; }

        [DisplayAttribute(Name = "Ativo ?")]
        public bool Active { get; set; }
    }
}
