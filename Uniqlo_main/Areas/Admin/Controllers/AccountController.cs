using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Uniqlo_main.Models;
using Uniqlo_main.ViewModels.Auths;

namespace Uniqlo_main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController(UserManager<User> _userManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {

            if(!ModelState.IsValid) { 
            return View();}
            User user=new User
                {
                Email = vm.Email,
                FullName = vm.FullName,
                UserName = vm.UserName,
                ProfileImageUrl="photo.jpg"


            };
            var result=await _userManager.CreateAsync(user, vm.Password);
            if(result.Succeeded)
            {
                foreach(var error  in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View();
            }

            return View();
        }
    }
}
