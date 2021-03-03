namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Author;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext db;
        public AuthorService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAuthorAsync(string name)
        {

            var author = new Author()
            {                
                Name = name
            };

            var isExistAuthor = await db.Authors.Where(x => x.Name == name).Select(x => x.Name).FirstOrDefaultAsync();

            if (isExistAuthor == null)
            {
                await this.db.Authors.AddAsync(author);
                await this.db.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<GetAllAuthorBooksViewModel>> GetAllAuthorBooks(int id)
        {
            var authorBooks = await db.Books.Where(x => x.AuthorId == id)
                .Select(x => new GetAllAuthorBooksViewModel
                {
                    ImageContent = x.BookCover.ImageContent,
                    BookName = x.BookCover.BookName,
                    Genre = x.Genre.TypeGenre,
                    BookId = x.Id
                })
                .AsNoTracking()
                .ToListAsync();

            return authorBooks;
        }
        public async Task<bool> AuthorExist(int id, string name)
        {
            var exits = await this.db.Authors.AnyAsync(x => x.Id == id && x.Name == name);
            return exits;
        }

        public async Task<IEnumerable<string>> GetAllAuthor()
        {
            return await this.db.Authors.Select(x => x.Name).ToListAsync();
        }
    }
}
