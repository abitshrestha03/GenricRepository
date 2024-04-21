namespace Day4First.Models
{
    public class AuthorCheckResult
    {

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public Author? AuthorFound { get; set; }
    }
}
