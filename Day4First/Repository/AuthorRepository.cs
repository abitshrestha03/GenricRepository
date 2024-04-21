using Day4First.Data;
using Day4First.Models;
using Microsoft.EntityFrameworkCore;

namespace Day4First.Repository
{
    public class AuthorRepository : IAuthor
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        //getting author email
        public async Task<Author> GetAuthorEmail(string email)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(dbAuthor => dbAuthor.Email == email);
        }

        //adding author
        public async Task<Author> InsertAuthorAsync(Author author)
        {
            Author authorEmailFound =await GetAuthorEmail(author.Email);
            if (authorEmailFound != null)
            {
                return new Author
                {
                    Id=authorEmailFound.Id,
                    Username=authorEmailFound.Username,
                    Email=authorEmailFound.Email,
                };
            }
            try
            {
                Author addedAuthor = (await _dbContext.Authors.AddAsync(author)).Entity;
                await SaveAuthor();
                return null;
            }catch(Exception ex)
            {
                throw new Exception("An error occured while inserting the author.", ex);
            }
        }

        //save function
        public async Task SaveAuthor()
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
        public async Task<AuthorCheckResult>CheckAuthor(Author author)
        {
            Author authorEmailFound=await GetAuthorEmail(author.Email);
            if (authorEmailFound == null)
            {
                return new AuthorCheckResult { IsSuccessful=false,Message="Email not found!",AuthorFound=null};
            }
            var authorPasswordRight =await _dbContext.Authors.FirstOrDefaultAsync(dbAuthor => dbAuthor.Password == author.Password);
            if (authorPasswordRight==null)
            {
                return new AuthorCheckResult { IsSuccessful = false, Message = "Password is incorrect!" ,AuthorFound=null};
            }
            return new AuthorCheckResult { IsSuccessful = true, Message = "Login success...",
                AuthorFound=new Author
                {
                    Id = authorPasswordRight.Id,
                    Username = authorPasswordRight.Username,
                    Email = authorPasswordRight.Email,
                }
            };
        }
    }
}
