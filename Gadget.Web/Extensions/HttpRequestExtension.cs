using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Gadget.Web.Extensions
{
    public static class HttpRequestExtension
    {
        public static bool IsSwaggerRequest(this HttpRequest request)
        {
            var key = "IsSwaggerRequest";

            if (!request.HttpContext.Items.Keys.Contains(key))
            {
                request.HttpContext.Items.Add(key,
                    request.Path.Value.IndexOf("swagger", StringComparison.CurrentCultureIgnoreCase) > 0);
            }

            return (bool)request.HttpContext.Items[key];
        }
    }
}
