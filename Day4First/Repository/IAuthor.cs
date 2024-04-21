using Day4First.Models;

namespace Day4First.Repository
{
    public interface IAuthor
    {
        Task<Author> InsertAuthorAsync(Author author);
        Task SaveAuthor();
        Task<Author> GetAuthorEmail(string email);
        Task<AuthorCheckResult> CheckAuthor(Author author);
    }
}
