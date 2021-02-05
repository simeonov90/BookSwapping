namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Services.Contracts;
    using System.Linq;
    using System.Threading.Tasks;
    public class BookCoverService : IBookCoverService
    {
        private readonly ApplicationDbContext db;
        public BookCoverService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateBookCoverAsync(string bookName, byte[] imageContent, string description)
        {
            var bookCover = new BookCover
            {
                BookName = bookName,
                ImageContent = imageContent,
                Description = description
            };

            await this.db.BookCovers.AddAsync(bookCover);
            await this.db.SaveChangesAsync();
        }      
        public async Task<bool> Delete(int id)
        {
            var book = await db.BookCovers.FindAsync(id);

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
    }
}
