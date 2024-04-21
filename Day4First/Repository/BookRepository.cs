using Day4First.Data;
using Day4First.Models;

namespace Day4First.Repository
{
    public class BookRepository : IBookRepository
    {
        ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void InsertBook(Book book)
        {
            _dbContext.Add(book);
            _dbContext.SaveChanges();
        }
    }
}
