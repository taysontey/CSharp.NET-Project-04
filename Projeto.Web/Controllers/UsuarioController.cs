using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Entity.Entities;
using Projeto.DAL.DataSource;
using Projeto.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace Projeto.Web.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {

        private UserManager<Usuario> userManager;

        public UsuarioController()
        {
            userManager = new UserManager<Usuario>(
                            new UserStore<Usuario>(
                                new Conexao()));
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AutenticarUsuario(UsuarioViewModelLogin model)
        {
            try
            {
                Usuario u = userManager.Find(model.Login, model.Senha);

                if (u != null)
                {
                    var ticket = userManager.CreateIdentity(u, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(ticket);

                    return Json(new
                    {
                        redirectUrl = Url.Action("Index", "Home", new { area = "AreaRestrita" }),
                        isRedirect = true
                    });
                }
                else
                {
                    return Json("Acesso Negado.");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public JsonResult CadastrarUsuario(UsuarioViewModelCadastro model, HttpPostedFileBase file)
        {
            try
            {
                Usuario u = new Usuario();
                u.Nome = model.Nome;
                u.Sobrenome = model.Sobrenome;
                u.UserName = model.Login;

                u.Foto = Guid.NewGuid().ToString() + "."
                        + Path.GetExtension(file.FileName);

                //upload da imagem..
                file.SaveAs(HttpContext.Server.MapPath("/Imagens/") + u.Foto);


                IdentityResult resultado = userManager.Create(u, model.Senha);

                if (resultado.Succeeded)
                {
                    return Json("Usuário " + u.Nome + ", cadastrado com sucesso.");
                }
                else
                {
                    return Json("Erro ao cadastrar usuario" + string.Join(", ", resultado.Errors));
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Usuario");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            userManager.Dispose();
        }
    }
}