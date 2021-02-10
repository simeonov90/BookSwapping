namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
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

            var isExistAuthor = db.Authors.Where(x => x.Name == name).Select(x => x.Name).FirstOrDefault();
            if (isExistAuthor == null)
            {
                await this.db.Authors.AddAsync(author);
                await this.db.SaveChangesAsync();
            }

        }

        public async Task<ICollection<Library>> GetAllAuthorBooks(int id)
        {
            var authorBooks = await db.Libraries.Where(x => x.Book.AuthorId == id)
                .Include(x => x.Book).ThenInclude(x => x.BookCover)
                .AsNoTracking()
                .ToListAsync();

            return authorBooks;
        }
        public async Task<bool> AuthorExist(int id, string name)
        {
            var exits = await this.db.Authors.AnyAsync(x => x.Id == id && x.Name == name);
            return exits;
        }
    }
}
