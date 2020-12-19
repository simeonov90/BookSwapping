namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Services.Contracts;
    using System;
    using System.Threading.Tasks;

    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext db;

        public LibraryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task ShareBookToLibrary(int bookId, DateTime date)
        {
            var library = new Library
            {
                BookId = bookId,
                Date = date
            };

            await this.db.Libraries.AddAsync(library);
            await this.db.SaveChangesAsync();

        }
    }
}
