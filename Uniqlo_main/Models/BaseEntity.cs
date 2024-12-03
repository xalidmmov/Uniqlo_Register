namespace Uniqlo_main.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatTime { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
