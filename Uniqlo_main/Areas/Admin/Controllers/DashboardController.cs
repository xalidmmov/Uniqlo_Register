using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniqlo_main.Enums;

namespace Uniqlo_main.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles=nameof(Roles.Admin))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
