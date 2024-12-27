using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Uniqlo_main.DataAccess;
using Uniqlo_main.Extensions;
using Uniqlo_main.Helpers;
using Uniqlo_main.Models;
using Uniqlo_main.Services;
using Uniqlo_main.Services.Abstracts;
using Uniqlo_main.Services.Implements;

namespace Uniqlo_main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<UniqloDbContext>(opt => { opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")); });
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 3;
                //opt.SignIn.RequireConfirmedEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Lockout.MaxFailedAccessAttempts =5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            }
            ).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.AccessDeniedPath = "/Home/AccessDenied";
            }

            
        );
           /////////// builder.Services.AddScoped<IEmailService, EmailService>();
           builder.Services.AddScoped<LayoutService>();
            var opt = new SmtpOptions();

            
           
            //SmtpOptions opt = new();
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Name));
            var app = builder.Build();

            //builder.Services.AddSession();

            app.UseStaticFiles();

            app.UseUserSeed();
            //app.UseSession();

            app.MapControllerRoute(name: "Default",
                pattern: "Register",
                defaults: new { controller = "Account", action = "Register" });

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(name:"default", pattern:"{Controller=Home}/{Action=Index}/{id?}");

            app.Run();
        }
    }
}
