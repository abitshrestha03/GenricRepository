using Day4First.Data;
using Day4First.Models;
using Microsoft.EntityFrameworkCore;

namespace Day4First.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //getting author email
        public async Task<User> GetUserEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(dbUser => dbUser.Email == email);
        }

        //adding author
        public async Task<User> InsertUserAsync(User user)
        {
            User userEmailFound = await GetUserEmail(user.Email);
            if (userEmailFound != null)
            {
                return new User
                {
                    Id = userEmailFound.Id,
                    Username = userEmailFound.Username,
                    Email = userEmailFound.Email,
                };
            }
            try
            {
                User addedUser = (await _dbContext.Users.AddAsync(user)).Entity;
                await SaveUser();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while inserting the author.", ex);
            }
        }

        //save function
        public async Task SaveUser()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("An error occured while saving changes to database.");
            }
        }

        //author login check
        public async Task<UserCheckResult> CheckUser(User user)
        {
            User userEmailFound = await GetUserEmail(user.Email);
            if (userEmailFound == null)
            {
                return new UserCheckResult { IsSuccessful = false, Message = "Email not found!", UserFound = null };
            }
            var userPasswordRight = await _dbContext.Users.FirstOrDefaultAsync(dbUser => dbUser.Password == user.Password);
            if (userPasswordRight == null)
            {
                return new UserCheckResult { IsSuccessful = false, Message = "Password is incorrect!", UserFound = null };
            }
            return new UserCheckResult
            {
                IsSuccessful = true,
                Message = "Login success...",
                UserFound = new User
                {
                    Id = userPasswordRight.Id,
                    Username = userPasswordRight.Username,
                    Email = userPasswordRight.Email,
                }
            };
        }
    }
}
