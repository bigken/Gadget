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
                        FirstName = "ken",
                        LastName = "wang",
                        Email = "bigken@163.com"
                    });
                }

                await dbContext.SaveChangesAsync();

                if (!dbContext.Articles.Any())
                {
                    await dbContext.Articles.AddAsync(new Article()
                    {
                        Author = dbContext.Authors.First(),
                        ContextFilePath = "../article/empty.html",
                        Title = "hello world"
                    });
                }

                await dbContext.SaveChangesAsync();

                if (!dbContext.Comments.Any())
                {
                    await dbContext.Comments.AddAsync(new Comment()
                    {
                        Content = "gadget",
                        Article = dbContext.Articles.First(),
                        Author = dbContext.Authors.First()
                    });
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
