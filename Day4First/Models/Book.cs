using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day4First.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }   
        public string Language { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Likes { get; set; }
        public int AuthorId { get;set; }
        public Author Author { get; set; }
        public List<int> LikedBy { get; set; } = new List<int>();
        public List<Comment> Comments { get; set; }
    }
}
