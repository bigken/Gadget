namespace Gadget.Api
{
    using System;
    using System.Linq;
    using Gadget.Data;
    using Gadget.Data.Entity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = serviceProvider.GetRequiredService<GadgetDbContext>())
            {
                await dbContext.Database.EnsureCreatedAsync();
            }

            // only seed data in dev & qa stage
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
                    await dbContext.Articles.AddRangeAsync(new[]{
                            new Article(){
                                Author = dbContext.Authors.First(),
                                ContextFilePath = "../article/empty.html",
                                Title = "hello world 1"
                            },
                            new Article(){
                            Author = dbContext.Authors.First(),
                            ContextFilePath = "../article/empty.html",
                            Title = "hello world 2"
                            }
                        }
                    );
                }

                await dbContext.SaveChangesAsync();

                if (!dbContext.Comments.Any())
                {
                    await dbContext.Comments.AddRangeAsync(new[]{
                        new Comment(){
                            Content = "comment 1",
                            Article = dbContext.Articles.First(),
                            Author = dbContext.Authors.First()
                        },
                        new Comment(){
                            Content = "comment 2",
                            Article = dbContext.Articles.First(),
                            Author = dbContext.Authors.First()
                        }});
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
