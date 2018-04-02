using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gadget.Core;
using Gadget.Core.Tools.JsonSchema;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace Gadget.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["GadgetToolDic"] = GadgetToolScaner.Instance.Scan();

            return View();
        }

        public IActionResult Run(string tool)
        {
            ViewData["tool"] = tool;

            var dic = GadgetToolScaner.Instance.Scan();

            if (dic.Keys.Any(x => x.Equals(tool, StringComparison.CurrentCultureIgnoreCase)))
            {
                ViewData["Html"] = dic.First(x => x.Key.Equals(tool, StringComparison.CurrentCultureIgnoreCase)).Value;
            }

            return View();
        }

        [HttpPost]
        public JsonResult JsonSchemaResult([FromBody] JsonSchemaInputData inputData)
        {
            var tool = Request.Query["tool"];

            return new JsonResult(GadgetToolScaner.Instance.ExecuteTool(tool, inputData));
        }
    }
}
