using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Uniqlo_main.Models;

namespace Uniqlo_main.DataAccess
{
    public class UniqloDbContext:IdentityDbContext<User>
    {

        public DbSet<Category>Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public UniqloDbContext(DbContextOptions opt) : base(opt) { }
    }
}
