namespace Gadget.Service.Extensions
{
    using Gadget.IService;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {
        public static void AddGadgetService(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
        }
    }
}
