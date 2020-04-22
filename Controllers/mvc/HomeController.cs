using System.Diagnostics;
using IronHasura.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSwag.Annotations;

namespace IronHasura.Controllers
{
    [OpenApiIgnore]
    public class HomeController : Controller
    {
        private readonly string hasuraUrl;

        public HomeController(IConfiguration configuration)
        {
            this.hasuraUrl = configuration.GetValue<string>("IRONHASURA_CONSOLE_URL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [OpenApiIgnore]
        public IActionResult Index()
        {
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

        public IActionResult Error(string errorId) 
        {
            var err = new ErrorViewModel 
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier    
            };

            return View(err);
        }
    }
}
