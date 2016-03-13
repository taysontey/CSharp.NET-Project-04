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

        public ActionResult Consulta()
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

        [HttpGet]
        public JsonResult Consultar()
        {
            try
            {
                var lista = new List<FornecedorModelConsulta>();

                FornecedorDal d = new FornecedorDal();

                foreach (Fornecedor f in d.FindAll())
                {
                    var model = new FornecedorModelConsulta();
                    model.IdFornecedor = f.IdFornecedor;
                    model.Nome = f.Nome;
                    model.CNPJ = f.CNPJ;

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
        public JsonResult Excluir(FornecedorModelConsulta model)
        {
            try
            {
                FornecedorDal d = new FornecedorDal();
                Fornecedor f = d.FindById(model.IdFornecedor);

                d.Delete(f);

                return Json("Fornecedor excluído.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}