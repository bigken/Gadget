using System.Threading.Tasks;
using Gadget.Web.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Gadget.Web.Middlewares
{
    /*
     * limte the call frequence
     */
    public class FrequencyControlMiddleware
    {
        private IOptions<FrequencyControlConfig> _rateConfig;
        private readonly RequestDelegate _next;

        public FrequencyControlMiddleware(RequestDelegate next,
            IOptions<FrequencyControlConfig> rateConfig)
        {
            _next = next;
            _rateConfig = rateConfig;
        }

        public async Task Invoke(HttpContext context)
        {
            //TODO

            //await context.Response.WriteAsync("you request too much");

            await _next(context);
        }
    }

    public static class FrequencyControlMiddlewareExtension
    {
        public static IApplicationBuilder UseFrequencyControll(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FrequencyControlMiddleware>();
        }
    }
}
