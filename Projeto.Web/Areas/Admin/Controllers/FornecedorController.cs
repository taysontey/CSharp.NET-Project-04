using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}