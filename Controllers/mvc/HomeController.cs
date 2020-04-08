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

        [Route("/home/data")]
        public IActionResult Hasura()
        {

            return Redirect(this.hasuraUrl);
        }
    }
}
