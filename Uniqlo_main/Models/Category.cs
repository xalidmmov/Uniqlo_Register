﻿namespace Uniqlo_main.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }=null!;
        public List<Product>? Products { get;  set; }
    }
}
