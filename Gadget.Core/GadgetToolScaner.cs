using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Html;

namespace Gadget.Core
{
    public sealed class GadgetToolScaner
    {
        private GadgetToolScaner()
        {

        }

        public static readonly GadgetToolScaner Instance = new GadgetToolScaner();

        public Dictionary<string, HtmlString> Scan()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();

            var gadgetToolTypes = asm.DefinedTypes.Where(
                    tp => tp.IsClass && tp.GetInterfaces().Any(
                              itp => itp.Name.Equals(typeof(IGadgetTool<>).Name)))
                .ToList();

            Dictionary<string, HtmlString> typeHtml = new Dictionary<string, HtmlString>();

            foreach (var gadgetToolType in gadgetToolTypes)
            {
                var para = gadgetToolType.GetMethod("Go").GetParameters()[0];

                var gadgetData = para.ParameterType.Assembly.CreateInstance(para.ParameterType.FullName) as GadgetData;

                if (gadgetData == null)
                {
                    continue;
                }

                typeHtml.Add(gadgetToolType.FullName, gadgetData.GenerateHtmlString());
            }

            return typeHtml;
        }

        public string ExecuteTool(string type, GadgetData inputData)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();

            var tool = asm.CreateInstance(type);

            string result = string.Empty;

            if (tool != null)
            {
                result = tool.GetType().GetMethod("Go").Invoke(tool, new object[] { inputData }).ToString();
            }

            return result;
        }
    }
}
