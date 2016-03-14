using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.Admin.Models
{
    public class ProdutoModelFornecedor
    {
        public int IdFornecedor { get; set; }
        public string Nome { get; set; }
    }

    public class ProdutoModelCategoria
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}