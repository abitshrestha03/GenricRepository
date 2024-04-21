namespace Day4First.Models
{
  public class Comment
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public DateTime PostedAt { get; set; }
            public int BookId { get; set; }
            public Book Book { get; set; } 
            public int UserId { get; set; } 
            public User User { get; set; } 
        }

}
