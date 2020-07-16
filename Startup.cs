using IronHasura.Configurations;
using IronHasura.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.Markdown;
using IronHasura.GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using com.ironhasura.Areas.Identity.Data;

namespace IronHasura
{
    public class Startup
    {
        private string ConnexionString { get; set; }
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.ConnexionString = Configuration.GetConnectionString("MCSP_DATA_CS");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // IdentityModelEventSource.ShowPII = true;
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<IronHasuraDbContext>(opt => opt.UseNpgsql(this.ConnexionString, o => o.SetPostgresVersion(9, 6)));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            });

            services.AddIdentityConfiguration();
            services.AddIdentityServerConfiguration(this.Configuration);
            services.AddAuthConfiguration(this.Configuration);
            services.AddAuthorizationConfiguration(this.Configuration);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddGraphQLConfiguration();

            services.AddSwaggerConfiguration();
            services.AddStorageConfiguration(this.Configuration);
            services.AddMarkdownConfiguration();
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
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            this.InitializeDatabase(app);

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors("AllowSpecificOrigin");
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMarkdown();

            app.UseGraphQL<IronHasuraSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app) 
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IdentityDataContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>().Database.Migrate();
            }
        }
    }
}
