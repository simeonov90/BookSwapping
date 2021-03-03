namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Genre;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext db;

        public GenreService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<GetAllBooksFromGenreViewModel>> GetAllBooksFromGenre(string genre)
        {
            var booksFromGenre = await this.db.Books.Where(x => x.Genre.TypeGenre == genre)
                .Select(x => new GetAllBooksFromGenreViewModel
                {
                    AuthorName = x.Author.Name,
                    AuthorId = x.AuthorId,
                    BookName = x.BookCover.BookName,
                    BookId = x.BookCoverId,
                    ImageContent = x.BookCover.ImageContent
                })
                .AsNoTracking()
                .ToListAsync();

            return booksFromGenre;
        }

        public async Task<IEnumerable<string>> GetAllGenre()
        {
            return await this.db.Genres.Select(x => x.TypeGenre).ToListAsync();
        }


    }
}
