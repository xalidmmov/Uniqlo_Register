using NuGet.Common;
using Uniqlo_main.ViewModels.Product;

namespace Uniqlo_main.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string CoverImage { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<ProductImage>? Images { get; set; }
        public ICollection<Comment> Comments { get; set; }


        public static implicit operator Product(ProductCreateVM vm)
        {
            return new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                Quantity = vm.Quantity,
                Discount = vm.Discount,
                CategoryId = vm.CategoryId,

            };


        }
    }
}
