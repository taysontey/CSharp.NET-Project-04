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

        public JsonResult DropDownCategoria()
        {
            try
            {
                var lista = new List<ProdutoModelCategoria>();

                CategoriaDal d = new CategoriaDal();

                foreach(Categoria c in d.FindAll())
                {
                    var model = new ProdutoModelCategoria();
                    model.IdCategoria = c.IdCategoria;
                    model.Nome = c.Nome;

                    lista.Add(model);
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult CadastrarCategoria(ProdutoModelCategoria model)
        {
            try
            {
                Categoria c = new Categoria();
                c.Nome = model.Nome;
                c.Descricao = model.Descricao;

                CategoriaDal d = new CategoriaDal();
                d.Insert(c);

                return Json("Categoria cadastrada com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult ExcluirCategoria(int id)
        {
            try
            {
                CategoriaDal d = new CategoriaDal();
                Categoria c = d.FindById(id);

                d.Delete(c);

                return Json("Categoria excluída.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}