namespace Day4First.Models
{
    public class UserCheckResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public User? UserFound { get; set; }
    }
}
