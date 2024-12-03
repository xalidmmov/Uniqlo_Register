using Microsoft.AspNetCore.Mvc;

namespace Uniqlo_main.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
