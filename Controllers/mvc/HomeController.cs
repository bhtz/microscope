using Microsoft.AspNetCore.Mvc;

namespace IronHasura.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
