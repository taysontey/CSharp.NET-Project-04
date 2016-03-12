using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entity.Entities
{
    public class Fornecedor
    {
        public virtual int IdFornecedor { get; set; }
        public virtual string Nome { get; set; }
        public virtual string CNPJ { get; set; }

        #region Relacionamentos

        public virtual ICollection<Produto> Produtos { get; set; }

        #endregion
    }
}
