namespace Uniqlo_main.Models
{
    public class Slider:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string SubTtile { get; set; } = null!;
        public string? Link { get; set; } 
        public string ImageUrl { get; set; }= null!;
    }
}
