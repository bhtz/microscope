using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IronHasura.Controllers
{
    public class HomeController : Controller
    {
        private readonly string hasuraUrl;

        public HomeController(IConfiguration configuration)
        {
            this.hasuraUrl = configuration.GetValue<string>("IRONHASURA_CONSOLE_URL");
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("/home/data")]
        public IActionResult Hasura()
        {

            return Redirect(this.hasuraUrl);
        }

        [Route("/home/docs/{doc}")]
        public IActionResult Docs(string doc)
        {
            ViewBag.Filename = doc + ".md";
            return View();
        }
    }
}
