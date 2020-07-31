using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IdentityServer4.Models;
using Microscope.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace Microscope.Controllers
{
    [OpenApiIgnore]
    public class HomeController : Controller
    {
        private readonly string hasuraUrl;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
            this.hasuraUrl = configuration.GetValue<string>("MCSP_HASURA_CONSOLE_URL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [OpenApiIgnore]
        public IActionResult Index()
        {
            this._logger.LogWarning("test serilog");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("/home/data")]
        [OpenApiIgnore]
        public IActionResult Hasura()
        {
            return Redirect(this.hasuraUrl);
        }

        [Route("/home/docs/{doc}")]
        [OpenApiIgnore]
        public IActionResult Docs(string doc)
        {
            ViewBag.Filename = doc + ".md";
            return View();
        }

        [Route("/settings")]
        [OpenApiIgnore]
        public IActionResult Settings()
        {
            var cs = this._configuration.GetConnectionString("MCSP_DATA_CS");

            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = cs;

            var model  = new SettingsViewModel 
            {
                HasuraConsoleUrl = this._configuration.GetValue<string>("MCSP_HASURA_CONSOLE_URL"),
                FileAdapter = this._configuration.GetValue<string>("MCSP_FILE_ADAPTER"),
                Container = this._configuration.GetValue<string>("MCSP_STORAGE_CONTAINER"),
                IsMicrosoftEnable = this._configuration.GetSection("ExternalProviders:Microsoft").Exists(),
                IsTwitterEnable = this._configuration.GetSection("ExternalProviders:Twitter").Exists(),
                IsLinkedInEnable = this._configuration.GetSection("ExternalProviders:LinkedIn").Exists(),
                IsGoogleEnable = this._configuration.GetSection("ExternalProviders:Google").Exists(),
                IsDropboxEnable = this._configuration.GetSection("ExternalProviders:Dropbox").Exists(),
                IsGithubEnable = this._configuration.GetSection("ExternalProviders:Github").Exists(),
                IsOIDCEnable = this._configuration.GetSection("ExternalProviders:OIDC").Exists(),
                Clients = this._configuration.GetSection("Clients").Get<List<Client>>(),
                DatabaseName = builder["Database"] as string,
                DatabaseHost = builder["Server"] as string,
            };

            return View(model);
        }

        public IActionResult Error(string errorId) 
        {
            var err = new ErrorViewModel 
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier    
            };

            return View(err);
        }

        [Route("/logs")]
        [OpenApiIgnore]
        public IActionResult Logs() 
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "./Logs");

            var logFilePath = Directory
                .GetFiles(path)
                .LastOrDefault();

            if(!string.IsNullOrEmpty(logFilePath))
            {
                var data = System.IO.File.ReadAllBytes(logFilePath);

                return new FileContentResult(data, "text/plain");
            }
            else
            {
                return Content("Microscope logs file not exist");
            }

        }
    }
}
