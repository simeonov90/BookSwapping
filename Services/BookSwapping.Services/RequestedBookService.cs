namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class RequestedBookService : IRequestedBookService
    {
        private readonly ApplicationDbContext db;

        public RequestedBookService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DidIWantThisBook(string userId, int bookId)
        {
            var wontBook = await this.db.RequestedBooks.AnyAsync(c => c.UserId == userId && c.BookId == bookId);

            return wontBook;
        }

        public async Task<bool> ItsMineBook(string userId, int bookId)
        {
            var itsMyBook = await this.db.Books.Where(c => c.Id == bookId).Select(c => c.UserId == userId).FirstOrDefaultAsync();

            return itsMyBook;
        }

        public async Task RejectTheRequest(string userId, int bookId)
        {
            var existTheRequest = await this.db.RequestedBooks.Where(c => c.UserId == userId && c.BookId == bookId).FirstOrDefaultAsync();

            if (existTheRequest != null)
            {
                this.db.RequestedBooks.Remove(existTheRequest);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task RequestThisBook(string userId, int bookId)
        {
            var requestBook = new RequestedBook
            {
                UserId = userId,
                BookId = bookId
            };

            var ExistTheRequest = await this.db.RequestedBooks.AnyAsync(c => c.UserId == userId && c.BookId == bookId);

            if (!ExistTheRequest)
            {
                await this.db.RequestedBooks.AddAsync(requestBook);
                await this.db.SaveChangesAsync();
            }
             
        }

        public async Task<IEnumerable<string>> ListWithUsers(int bookId)
        {
            var users = await db.RequestedBooks.Where(c => c.BookId == bookId).Select(c => c.User.UserName).ToListAsync();

            return users;
        }

    }
}
