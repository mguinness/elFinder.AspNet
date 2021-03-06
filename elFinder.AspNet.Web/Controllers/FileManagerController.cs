using Microsoft.AspNetCore.Mvc;

namespace elFinder.AspNet.Web.Controllers
{
    [Route("file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}