using Microscope;
using Microscope.Configurations;
using Microscope.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microscope.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            
            services.AddInfrastructure(Configuration);
            services.AddStorage(Configuration);
            
            services.AddRazorPages();
            services.AddControllers();
            services.AddCorsConfiguration(Configuration);
            services.AddSwaggerConfiguration(Configuration);
            services.AddAuthenticationConfiguration(Configuration);
            services.AddAuthorizationConfiguration(Configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHttpsRedirection();
            }

            if(Configuration.GetValue<bool>("EnableMigration"))
            {
                this.InitializeDatabase(app);
            }
            
            var isWebConsoleEnabled = Configuration.GetValue<bool>("EnableWebConsole");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microscope.Api v1"));
            
            if(isWebConsoleEnabled)
            {
                app.UseBlazorFrameworkFiles();
                app.UseStaticFiles();
            }
            
            app.UseCors("allow-all");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                
                if(isWebConsoleEnabled)
                    endpoints.MapFallbackToFile("index.html");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<MicroscopeDbContext>().Migrate();
            }
        }
    }
}
