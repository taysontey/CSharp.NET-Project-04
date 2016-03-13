using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Entity.Entities;
using Projeto.DAL.Persistence;
using Projeto.Web.Areas.Admin.Models;

namespace Projeto.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Cadastrar(FornecedorModelCadastro model)
        {
            try
            {
                Fornecedor f = new Fornecedor();
                f.Nome = model.Nome;
                f.CNPJ = model.CNPJ;

                FornecedorDal d = new FornecedorDal();
                d.Insert(f);

                return Json("Fornecedor " + f.Nome + ", cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}