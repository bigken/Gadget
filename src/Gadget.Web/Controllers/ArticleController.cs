using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gadget.Web.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Prepare()
        {
            return View();
        }
    }
}