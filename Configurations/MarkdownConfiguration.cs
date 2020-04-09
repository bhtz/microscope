using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.Markdown;

namespace IronHasura.Configurations
{
    public static class MarkdownConfiguration
    {
        public static IServiceCollection AddMarkdownConfiguration(this IServiceCollection services)
        {
            services.AddMarkdown();

            return services;
        }
    }
}