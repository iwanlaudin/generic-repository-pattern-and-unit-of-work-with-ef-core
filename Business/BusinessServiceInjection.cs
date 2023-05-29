using GenericRepositoryPattern.Business.Interfaces;
using GenericRepositoryPattern.Business.Services;

namespace GenericRepositoryPattern.Business
{
    public static class BusinessServiceInjection
    {
        public static IServiceCollection AddBusinessServiceInjection(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}