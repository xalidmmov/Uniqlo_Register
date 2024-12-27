using Uniqlo_main.DataAccess;

namespace Uniqlo_main.Services
{
    public class LayoutService(UniqloDbContext _context)
    {
        public string GetContact()
        {
            return "Baku State University";
        }
    }
}
