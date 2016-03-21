using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Projeto.DAL.Generics;
using Projeto.Entity.Entities;
using Projeto.DAL.DataSource;

namespace Projeto.DAL.Persistence
{
    public class ProdutoDal : GenericDal<Produto>
    {
        public List<Produto> FindByCategoria(string categoria)
        {
            return Con.Produto
                .Where(p => p.Categoria.Nome.Equals(categoria))
                .ToList();
        }
    }
}
