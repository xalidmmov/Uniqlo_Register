using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using Uniqlo_main.DataAccess;
using Uniqlo_main.ViewModels.Basket;

namespace Uniqlo_main.Controllers
{
    public class ProductController(UniqloDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(!id.HasValue) 
            {
                return BadRequest();   
            }
            var data= await _context.Products.Where(x=>x.Id==id.Value && !x.IsDeleted).Include(x=>x.Images).Include(x => x.Comments).ThenInclude(x => x.User).Include(x=>x.Ratings).ThenInclude(x=>x.User).FirstOrDefaultAsync();
           
            if (data is null) { return NotFound(); }
            ViewBag.Rating = 5;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
                int rating= await _context.ProductRatings.Where(x=>x.UserId==userId && x.ProductId==id).Select(x=>x.Rating).FirstOrDefaultAsync(); 
                ViewBag.Rating = rating==0 ? 5: rating;
            }
           
            return View(data);
        }
        public async Task<IActionResult> Rating(int productId,int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var data= await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId ==productId).FirstOrDefaultAsync();
            if(data is null)
            {
                await _context.ProductRatings.AddAsync(new Models.ProductRating
                {
                    UserId = userId,
                    ProductId = productId,
                    Rating = rating
                });
            }
            else
            {
                data.Rating=rating;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new {Id=productId});
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string commentText)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(commentText))
            {
                return RedirectToAction(nameof(Details), new { id = productId });
            }

            string userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
          

            var comment = new Comment
            {
                ProductId = productId,
                UserId = userIdString,
                Text = commentText,
                DatePosted = DateTime.Now
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = productId });
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            //if (!await _context.Products.AnyAsync(x => x.Id == id))
            //    return NotFound();
            var basketItems =JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");
            var item=basketItems.FirstOrDefault(x=>x.Id == id);
            if (item == null)
            {
                item = new BasketProductItemVM(id);
                
                basketItems.Add(item);
            }
            item.Count++;
            Response.Cookies.Append("basket", JsonSerializer.Serialize(basketItems));
            return Ok();
        }

    }
}
