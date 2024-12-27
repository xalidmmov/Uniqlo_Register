using Uniqlo_main.Models;

public class Comment
{
    public int Id { get; set; } 
    public int ProductId { get; set; } 
    public string UserId { get; set; }
    public string Text { get; set; } 
    public DateTime DatePosted { get; set; } 

    public Product Product { get; set; }

   
    public User User { get; set; }
}
