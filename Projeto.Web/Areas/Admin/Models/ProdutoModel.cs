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

    public class ProdutoModelCadastro
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Foto { get; set; }
        public int IdCategoria { get; set; }
        public int IdFornecedor { get; set; }
    }

    public class ProdutoModelConsulta
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public string Foto { get; set; }
    }
}