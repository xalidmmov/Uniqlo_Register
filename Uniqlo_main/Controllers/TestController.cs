using Microsoft.AspNetCore.Mvc;

namespace Uniqlo_main.Controllers
{
    public class TestController : Controller
    {
        public IActionResult SetSession(string key,string value)
        {
            HttpContext.Session.SetString(key,value);
            return Ok();
        }
        public async Task<IActionResult> GetSession(string key)
        {
            HttpContext.Session.Remove(key);
            return Content(HttpContext.Session.GetString(key));
        }
        public async Task<IActionResult> SetCookie(string key, string value)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                //Expires=new DateTime(2024, 12, 31, 00, 00, 00)
                MaxAge = TimeSpan.FromMinutes(1)
            }) ;
            return Ok();
        }
        public async Task<IActionResult> GetCookie(string key)
        {
            return Content(HttpContext.Request.Cookies[key]);
        }
    }
}
