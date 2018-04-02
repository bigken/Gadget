using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json.Linq;

namespace Gadget.Core
{
    public class GadgetData
    {
        public string RawMaterial { get; set; }

        public virtual HtmlString GenerateHtmlString()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            StringBuilder html=new StringBuilder("<div>");

            foreach (var property in properties)
            {
                html.Append(property.Name);
                html.Append(":");
                
                if (property.PropertyType == typeof(System.String))
                {
                    html.Append(
                        $"<textarea tag='gadgettool' name='{property.Name}' id='{property.Name}' rows='10' cols='100'>");
                        
                       html.Append("</textarea>");
                }

                if (property.PropertyType.IsEnum)
                {
                   
                    html.Append($"<select tag='gadgettool' name='{property.Name}' id='{property.Name}'>");
                    
                    foreach (var emumValue in Enum.GetValues(property.PropertyType))
                    {
                        html.Append($"<option>{emumValue}</option>");
                    }
                    
                    html.Append("</select>");
                }

                html.Append("<br/>");
            }

            html.Append("</div>");
            
            return new HtmlString(html.ToString());
        }
    }
}
