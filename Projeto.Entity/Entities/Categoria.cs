using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entity.Entities
{
    public class Categoria
    {
        public virtual int IdCategoria { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }

        #region Relacionamentos

        public virtual ICollection<Produto> Produtos { get; set; }

        #endregion
    }
}
