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
        public async Task Delete(int id)
        {
            var book = db.BookCovers.Where(x => x.Id == id).FirstOrDefault();
            db.BookCovers.Remove(book);
            await db.SaveChangesAsync();
        }

        public async Task<BookCover> GetBookCover(int id)
        {
            var getBookCover = await db.BookCovers.FindAsync(id);
            return getBookCover;
        }
    }
}
