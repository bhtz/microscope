using Microscope.Api.Configurations;
using Microscope.Api.Middlewares;
using Microscope.Application;
using Microscope.Infrastructure;
using Microscope.Workflow;
using Microsoft.FeatureManagement;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => 
{
    lc.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.File("Logs/microscope.txt")
        .WriteTo.Console();
});

builder.Services
    .AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));
    
builder.Services.AddMicroscopeApplication();
builder.Services.AddCommonInfrastructure(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

builder.Services.AddWorkflow(builder.Configuration);
            
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationConfiguration(builder.Configuration);

var app = builder.Build();

var isMigrationEnabled = builder.Configuration.GetValue<bool>("EnableMigration");
var isWebConsoleEnabled = builder.Configuration.GetValue<bool>("EnableWebConsole");

if(app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else 
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditExOp.Api v1"));

if(isMigrationEnabled)
    app.Services.GetRequiredService<MicroscopeDbContext>().Migrate();

if(isWebConsoleEnabled)
{
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();
}

app.UseMiddleware<HttpExceptionMiddleware>();

app.UseHttpActivities();

app.UseRouting();
app.UseCors("allow-all");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

if(isWebConsoleEnabled)
    app.MapFallbackToFile("index.html");

app.Run();
