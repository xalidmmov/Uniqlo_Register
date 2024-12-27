﻿using System.ComponentModel.DataAnnotations;

namespace Uniqlo_main.ViewModels.Product
{
    public class ProductUpdateVM
    {

        [MaxLength(32, ErrorMessage = "Product Name's must be less than 32"), Required(ErrorMessage = "Product Name bosh ola bilmez!")]
        public string ProductName { get; set; }
        [MaxLength(400, ErrorMessage = "Product Description's must be less than 400"), Required(ErrorMessage = "Product Description bosh ola bilmez!")]
        public string ProductDescription { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        [Required]
        public decimal SellPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile? CoverImage { get; set; }
        [Required]
        public int? CategoryID { get; set; }
    }
}
