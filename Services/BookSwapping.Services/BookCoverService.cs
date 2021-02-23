namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.BookCover;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class BookCoverService : IBookCoverService
    {
        private readonly ApplicationDbContext db;
        public BookCoverService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateBookCoverAsync(string bookName, Task<byte[]> imageContent, string description)
        {
            var bookCover = new BookCover
            {
                BookName = bookName,
                ImageContent = await imageContent,
                Description = description
            };

            await this.db.BookCovers.AddAsync(bookCover);
            await this.db.SaveChangesAsync();
        }      
        public async Task<bool> Delete(int bookCoverId)
        {
            var book = await db.BookCovers.FindAsync(bookCoverId);

            if (book == null)
            {
                return false;
            }

            db.BookCovers.Remove(book);

            await db.SaveChangesAsync();

            return true;
        }

        public async Task<BookCover> GetBookCover(int id)
        {
            var getBookCover = await db.BookCovers.FindAsync(id);

            if (getBookCover == null)
            {
                return null;
            }

            return getBookCover;
        }
        public async Task<IEnumerable<GetAllByUserViewModel>> GetAllByUser(string userId)
        {
            var getAll = await db.BookCovers.Where(c => c.Books.UserId == userId).Select(c => new GetAllByUserViewModel
            {
                ImageContent = c.ImageContent,
                BookName = c.BookName,
                BookId = c.Books.Id
            })
                .AsNoTracking()
                .ToListAsync();

            return getAll;
        }
    }
}
