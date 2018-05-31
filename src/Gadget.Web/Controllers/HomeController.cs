using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gadget.IService;
using Microsoft.AspNetCore.Mvc;
using Gadget.Web.Models;

namespace Gadget.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorService _authorService;

        public HomeController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test(int id)
        {
            var author = await _authorService.GetAuthor(id);

            return View(author);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
