using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Projeto.Entity.Entities
{
    public class Usuario : IdentityUser
    {
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual string Foto { get; set; }
    }
}
