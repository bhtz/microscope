using Microsoft.Extensions.DependencyInjection;

namespace IronHasura.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerDocument(opt =>
            {
                opt.PostProcess = doc =>
                {
                    doc.Info.Version = "v1";
                    doc.Info.Title = "MICROSCOPE";
                    doc.Info.Description = "Digital Platform Factory";
                    doc.Info.TermsOfService = "None";
                };
            });

            return services;
        }
    }
}