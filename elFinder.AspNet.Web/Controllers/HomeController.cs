using Microsoft.AspNetCore.Mvc;

namespace elFinder.AspNet.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}