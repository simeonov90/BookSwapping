namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Services.Contracts;
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
    }
}
