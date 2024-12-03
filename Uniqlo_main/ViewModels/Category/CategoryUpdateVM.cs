using System.ComponentModel.DataAnnotations;

namespace Uniqlo_main.ViewModels.Category
{
    public class CategoryUpdateVM
    {
        [MaxLength(32), Required]
        public string Name { get; set; } = null!;
    }
}
