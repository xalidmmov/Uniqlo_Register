using Uniqlo_main.ViewModels.Product;
using Uniqlo_New.ViewModels.Slider;

namespace Uniqlo_New.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<SliderItemVM> Sliders { get; set; }
        public IEnumerable<ProductItemVM> Products { get; set; }
    }
}