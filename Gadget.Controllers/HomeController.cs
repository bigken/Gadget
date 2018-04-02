using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gadget.Core.Tools.JsonSchema;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace Gadget.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var schemaVersions = Enum.GetValues(typeof(SchemaVersion));

            ViewData["schemaVersions"] = schemaVersions;

            return View();
        }

        [HttpPost]
        public JsonResult JsonSchemaResult([FromBody] JsonSchemaInputData inputData)
        { 
            Gadget.Core.Tools.JsonSchema.JsonSchemaTool jsonSchemaTool =
                new JsonSchemaTool();

            var result = jsonSchemaTool.Go(inputData);
            
            return new JsonResult(result)
            {
                
            };
        }
    }
}
