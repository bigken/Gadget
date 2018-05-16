using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gadget.Data;
using Gadget.Data.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Gadget.Api
{
    public class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        { 
            //only init data in dev & qa stage
            var env = serviceProvider.GetRequiredService<IHostingEnvironment>();

            if (!new[] { "Development", "qa" }.Contains(env.EnvironmentName))
            {
                return;
            }

            using (var dbContext = serviceProvider.GetRequiredService<GadgetDbContext>())
            {
                if (!dbContext.Authors.Any())
                {
                    await dbContext.Authors.AddAsync(new Author()
                    {
                        Name = "ken"
                    });
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
