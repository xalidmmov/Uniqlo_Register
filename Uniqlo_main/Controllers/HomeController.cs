using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo_main.DataAccess;
using Uniqlo_main.ViewModels.Product;
using Uniqlo_New.ViewModels.Home;
using Uniqlo_New.ViewModels.Slider;

namespace Uniqlo_main.Controllers
{
    public class HomeController(UniqloDbContext _context) : Controller
    {

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM();
            vm.Sliders = await _context.Sliders.Where(x => x.IsDeleted == false).Select(x => new SliderItemVM
            {
                ImageUrl = x.ImageUrl,
                Link = x.Link,
                Subtitle = x.SubTtile,
                Title = x.Title,
            }).ToListAsync();
            vm.Products = await _context.Products.Where(x => x.IsDeleted == false).Select(x => new ProductItemVM
            {
                Discount = x.Discount,
                Name = x.Name,
                Id = x.Id,
                ImageUrl = x.CoverImage,
                IsInStock = x.Quantity > 0,
                Price = x.SellPrice,
            }
            ).ToListAsync();

            return View(vm);
            // return View(await _context.Products.Where(x => x.IsDeleted == false).Include(x => x.Category).ToListAsync());
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
     
    }
}
