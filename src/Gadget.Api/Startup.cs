using System;
using System.Diagnostics;
using Gadget.IService;
using Gadget.Service;
using Gadget.Service.Extensions;
using Microsoft.Extensions.Logging;

namespace Gadget.Api
{
    using System.IO;
    using Gadget.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDatabase(services);

            services.AddGadgetService();

            services.AddCors(option =>
            {
                option.AddPolicy("default-cors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();

                c.SwaggerDoc("v1", new Info { Title = "Gadget Service API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue<bool>("SwaggerEnabled"))
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gadget Service API"); });
            }

            app.UseCors("default-cors");

            app.UseMvc();
        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("GadgetDB");

            Console.WriteLine($"GadgetDB ConnectionString Is ${connectionString}");

            services.AddDbContext<GadgetDbContext>(
                options => options.UseMySql(connectionString, b => b.MigrationsAssembly("Gadget.Api"))
            );
        }
    }
}
