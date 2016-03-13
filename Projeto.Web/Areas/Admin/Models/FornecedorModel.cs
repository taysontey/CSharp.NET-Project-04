using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.Admin.Models
{
    public class FornecedorModelCadastro
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
    }

    public class FornecedorModelConsulta
    {
        public int IdFornecedor { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
    }
}