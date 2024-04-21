using Day4First.Models;

namespace Day4First.Repository
{
    public interface IUserRepository
    {
        Task<User> InsertUserAsync(User user);
        Task SaveUser();
        Task<User> GetUserEmail(string email);
        Task<UserCheckResult> CheckUser(User user);
    }
}
