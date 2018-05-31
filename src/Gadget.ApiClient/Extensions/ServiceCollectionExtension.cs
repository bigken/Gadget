using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Gadget.IService;

namespace Gadget.ApiClient.Extensions
{
   public static class ServiceCollectionExtension
    {
        public static void AddGadgetApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(configuration.GetSection("GadgetApiBaseAddress").Value)
            };
          
            services.AddSingleton(client);

            services.AddSingleton<IAuthorService, AuthorServiceClient>();
        }
    }
}
