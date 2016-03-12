using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entity.Entities
{
    public class Produto
    {
        public virtual int IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual int Quantidade { get; set; }
        public int IdFornecedor { get; set; }
        public int IdCategoria { get; set; }

        #region Relacionamentos

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Categoria Categoria { get; set; }

        #endregion
    }
}
