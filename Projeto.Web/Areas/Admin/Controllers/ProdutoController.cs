﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Entity.Entities;
using Projeto.DAL.Persistence;
using Projeto.Web.Areas.Admin.Models;
using System.IO;

namespace Projeto.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProdutoController : Controller
    {
        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            return View();
        }

        #region DropDowns

        public JsonResult DropDownFornecedor()
        {
            try
            {
                var lista = new List<ProdutoModelFornecedor>();

                FornecedorDal d = new FornecedorDal();

                foreach (Fornecedor f in d.FindAll())
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

                foreach (Categoria c in d.FindAll())
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

        #endregion

        #region Métodos AJAX(Categoria)

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

        #endregion

        #region Métodos AJAX(Produto)

        [HttpPost]
        public JsonResult CadastrarProduto(ProdutoModelCadastro model, HttpPostedFileBase file)
        {
            try
            {
                Produto p = new Produto();
                p.Nome = model.Nome;
                p.Preco = model.Preco;
                p.Quantidade = model.Quantidade;
                p.Foto = Guid.NewGuid().ToString() + "." + Path.GetExtension(file.FileName);
                p.IdCategoria = model.IdCategoria;
                p.IdFornecedor = model.IdFornecedor;

                file.SaveAs(HttpContext.Server.MapPath("/Imagens/") + p.Foto);

                ProdutoDal d = new ProdutoDal();
                d.Insert(p);

                return Json("Produto cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public JsonResult ConsultarProduto()
        {
            try
            {
                var lista = new List<ProdutoModelConsulta>();

                ProdutoDal d = new ProdutoDal();

                foreach (Produto p in d.FindAll())
                {
                    var model = new ProdutoModelConsulta();
                    model.IdProduto = p.IdProduto;
                    model.Nome = p.Nome;
                    model.Preco = p.Preco;
                    model.Quantidade = p.Quantidade;
                    model.Categoria = p.Categoria.Nome;
                    model.Fornecedor = p.Fornecedor.Nome;
                    model.Foto = p.Foto;

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
        public JsonResult ExcluirProduto(int id)
        {
            try
            {
                ProdutoDal d = new ProdutoDal();
                Produto p = d.FindById(id);

                if (p != null)
                {
                    d.Delete(p);
                    return Json("Produto excluído.");
                }
                else
                {
                    return Json("Produto não encontrado.");
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