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

        #region Métodos AJAX(Fornecedor)

        [HttpPost]
        public JsonResult CadastrarFornecedor(FornecedorModelCadastro model)
        {
            try
            {
                Fornecedor f = new Fornecedor()
                {
                    Nome = model.Nome,
                    CNPJ = model.CNPJ
                };

                FornecedorDal d = new FornecedorDal();
                d.Insert(f);

                return Json("Fornecedor " + f.Nome + ", cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult EditarFornecedor(int id)
        {
            try
            {
                FornecedorDal d = new FornecedorDal();
                Fornecedor f = d.FindById(id);

                if (f != null)
                {
                    var model = new FornecedorModelConsulta();
                    model.IdFornecedor = f.IdFornecedor;
                    model.Nome = f.Nome;
                    model.CNPJ = f.CNPJ;

                    return Json(model);
                }
                else
                {
                    return Json("Fornecedor não encontrado.");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult AtualizarFornecedor(FornecedorModelConsulta model)
        {
            try
            {
                Fornecedor f = new Fornecedor()
                {
                    IdFornecedor = model.IdFornecedor,
                    Nome = model.Nome,
                    CNPJ = model.CNPJ
                };

                FornecedorDal d = new FornecedorDal();
                d.Update(f);

                return Json("Fornecedor atualizado.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public JsonResult ConsultarFornecedor()
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
        public JsonResult ExcluirFornecedor(int id)
        {
            try
            {
                FornecedorDal d = new FornecedorDal();
                Fornecedor f = d.FindById(id);

                if (f != null)
                {
                    d.Delete(f);
                    return Json("Fornecedor excluído.");
                }
                else
                {
                    return Json("Fornecedor não encontrado.");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        #endregion
    }
}