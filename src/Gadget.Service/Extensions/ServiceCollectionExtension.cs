using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Gadget.IService;

namespace Gadget.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddGadgetService(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
        }
    }
}
