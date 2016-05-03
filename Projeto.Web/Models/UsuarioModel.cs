using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //mapeamento..

namespace Projeto.Web.Models
{
    public class UsuarioViewModelLogin
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioViewModelCadastro
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirm { get; set; }
    }
}