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
    public class ProdutoController : Controller
    {
        public ActionResult Cadastro()
        {
            return View();
        }

        public JsonResult DropDownFornecedor()
        {
            try
            {
                var lista = new List<ProdutoModelFornecedor>();

                FornecedorDal d = new FornecedorDal();

                foreach(Fornecedor f in d.FindAll())
                {
                    var model = new ProdutoModelFornecedor();
                    model.IdFornecedor = f.IdFornecedor;
                    model.Nome = f.Nome;

                    lista.Add(model);
                }

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}