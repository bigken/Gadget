using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Commons.Collections;
using Gadget.Core;
using Gadget.Core.Tools.JsonSchema;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

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


        public IActionResult VelocityTest()
        {
            return View();
        }

        VelocityEngine templateEngine = new VelocityEngine();

        [HttpPost]
        public JsonResult ExecuteVelocity(string inputString)
        {
            var productList = new[]
             {
                new
                {
                    img =
                    "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1522839643312&di=0d913be96539c5b107b443cca7c15a81&imgtype=0&src=http%3A%2F%2Fimg.sanbengzi.com%2F1354040218-1054532924-21-0.jpg",
                    name = "亲子一日团",
                    price = 168
                },
                new
                {
                    img =
                    "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1522839643312&di=1b2d4eba388a7676497c477dfd4cfa44&imgtype=0&src=http%3A%2F%2F05.imgmini.eastday.com%2Fmobile%2F20161207%2F20161207110425_d5f72ceb3f084b1856affb2284d5298e_1.jpeg",
                    name = "亲子二日团",
                    price = 168
                },
                new
                {
                    img =
                    "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1523434459&di=825c3f6b5cb378ce2a17edc8acd7685d&imgtype=jpg&er=1&src=http%3A%2F%2Fres5.lekan.com%2Fkids%2Ffigure%2F1839_330x350.png",
                    name = "亲子夏令营",
                    price = 168
                },
                new
                {
                    img =
                    "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1522839756654&di=d1196f821cfa8f5cb6f4d0cf316924a0&imgtype=0&src=http%3A%2F%2Fpic7.qiyipic.com%2Fimage%2F20140513%2F46%2F22%2F52%2Fli_23808_li_601.jpg",
                    name = "成人吃鸡一日团",
                    price = 168
                }
            };

            templateEngine.Init();

            VelocityContext context = new VelocityContext();

            context.Put("productList", productList);

            System.IO.StringWriter writer = new System.IO.StringWriter();

            templateEngine.Evaluate(context, writer, "mystring", HttpUtility.UrlDecode(inputString));

            var output = writer.GetStringBuilder().ToString();

            return new JsonResult(output);
        }
    }
}