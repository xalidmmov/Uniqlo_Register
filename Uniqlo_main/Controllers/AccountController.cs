using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using Uniqlo_main.Enums;
using Uniqlo_main.Helpers;
using Uniqlo_main.Models;
using Uniqlo_main.Services.Abstracts;
using Uniqlo_main.ViewModels.Auths;

namespace Uniqlo_main.Controllers
{

    public class AccountController(UserManager<User> _userManager, SignInManager<User> _singInManager) : Controller

    {
       
        
        bool isAudenticated=>User.Identity?.IsAuthenticated ?? false;
        //public IActionResult Send()
        //{
        //    return Ok(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Account/VerifyEmail?Token=");
        //}
        public IActionResult Register()
        {
            if (isAudenticated) RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {
            if(isAudenticated) RedirectToAction("Index","Home");
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new User
            {
                Email = vm.Email,
                FullName = vm.FullName,
                UserName = vm.UserName,
                ProfileImageUrl = "photo.jpg"


            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            string token=await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //////_service.SendEmailConfirmation(user.Email, user.UserName, token);
            //return Content("Email sent");
            return View();
        }

        public async Task<IActionResult> Login()
        {
            if (isAudenticated) RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? returnUrl = null)
        {
            if (isAudenticated) RedirectToAction("Index", "Home");
            if (!ModelState.IsValid) return View();

            User? user = null;

            if (vm.UserNameOrEmail.Contains('@'))
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
            else
                user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);

            if (user is null)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }


            var result = await _singInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "You must confirm your acount");
                if (result.IsLockedOut)
                    ModelState.AddModelError("", "Wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                return View();
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                if(await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", new { Controller = "Dashboard", Area = "Admin" });
                }
                return RedirectToAction("Index","Home");
            }

            return LocalRedirect(returnUrl);
              
        }
        public async Task<IActionResult> LogOut()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   
        }
        //public async Task<IActionResult> Test()
        //{
        //    //SmtpClient smtp = new();
        //    //smtp.Host= "smtp.gmail.com";
        //    //smtp.Port = 587;
        //    //smtp.EnableSsl = true;
        //    //smtp.Credentials = new NetworkCredential("xalidhm-bp215@code.edu.az", "dbfh mqmw sdpr svoi");
        //    //MailAddress from = new MailAddress("xalidhm-bp215@code.edu.az", "Random message");
        //    //MailAddress to = new("elmir2004m@gmail.com");
        //    //MailMessage msg= new MailMessage(from,to);
        //    //msg.Subject = "Mail";
        //    //msg.Body = "<p>Mail gonderildi From this <a>link</a></p>";
        //    //msg.IsBodyHtml = true;
        //    //smtp.Send(msg);
        //    //return Ok("alindi");  
        //    //_service.SendAsync().Wait();
        //    return Ok("alindi");
        //}
        //public async Task<IActionResult> VerifyEmail(string token,string user)
        //{
        //   var entity= await _userManager.FindByNameAsync(user);
        //    if(entity is null)
        //    {
        //        return BadRequest();
        //    }
        //    var result =await _userManager.ConfirmEmailAsync(entity, token);
        //    if (!result.Succeeded)
        //    {
        //        StringBuilder sb=new StringBuilder();
        //        foreach(var item in token)
        //        {
        //            sb.AppendLine(item.ToString());
        //        }
        //        return Content(sb.ToString());
        //    }
        //    await _singInManager.SignInAsync(entity, true);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
