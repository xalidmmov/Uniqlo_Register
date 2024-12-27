using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Uniqlo_main.DataAccess;
using Uniqlo_main.ViewModels.Basket;

namespace Uniqlo_main.ViewComponents
{
    public class HeaderViewComponent(UniqloDbContext _context):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketIds =JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");
            var prods=await _context.Products.Where(x => basketIds.Select(y => y.Id).Any(y => y == x.Id)).Select(x => new ProductItemVM
            {
                Id = x.Id,
                Discount = x.Discount,
                ImageUrl = x.CoverImage,
                SellPrice = x.SellPrice,
            }).ToListAsync();
            
            foreach(var item in prods)
            {
                item.Count=basketIds!.FirstOrDefault(x => x.Id == item.Id)!.Count;
            }
            return View(prods);
        }

    }
}
