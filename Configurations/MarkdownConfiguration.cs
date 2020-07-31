using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.Markdown;

namespace Microscope.Configurations
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