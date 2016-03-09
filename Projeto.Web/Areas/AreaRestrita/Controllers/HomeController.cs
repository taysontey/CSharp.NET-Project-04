using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    [Authorize] //requer autenticação..
    public class HomeController : Controller
    {
        // GET: AreaRestrita/Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}