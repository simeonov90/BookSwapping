namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    public class BookService : IBookService
    {
        private readonly IAuthorService authorService;
        private readonly IBookCoverService bookCoverService;
        private readonly ApplicationDbContext db;

        public BookService
           (
            IAuthorService authorService,
            IBookCoverService bookCoverService,
            ApplicationDbContext db
            )
        {
            this.authorService = authorService;
            this.bookCoverService = bookCoverService;
            this.db = db;
        }

        public async Task CreateBook(CreateBookInputModel create)
        {

            var memoryStream = new MemoryStream();
            var image = SixLabors.ImageSharp.Image.Load(create.FormFile.OpenReadStream());
            image.Mutate(x => x.Resize(240, 240));
            await image.SaveAsPngAsync(memoryStream);

            await this.bookCoverService.CreateBookCoverAsync(create.BookName, memoryStream.ToArray(), create.Description);
            await this.authorService.CreateAuthorAsync(create.Author);

            var authorId = db.Authors.Where(a => a.Name == create.Author).Select(a => a.Id).FirstOrDefault();
            var bookCoverId = db.BookCovers.Where(b => b.BookName == create.BookName).ToList().Select(x => x.Id).Last();

            var genreId = db.Genres.Where(g => g.TypeGenre == create.TypeGenre).Select(g => g.Id).FirstOrDefault();


            var books = new Book
            {
                AuthorId = authorId,
                BookCoverId = bookCoverId,
                GenreId = genreId,
                UserId = create.UserId
            };
            await this.db.Books.AddAsync(books);
            await this.db.SaveChangesAsync();

        }

        public ICollection<Book> GetAllMyBookAsync(GetAllMyBookInputModel getAllBook)
        {
            ICollection<Book> book = db.Books.Where(x => x.UserId == getAllBook.UserId)
                .Include(c => c.BookCover)
                .Include(c => c.Author)
                .Include(c => c.Genre)
                .ToList();
            return book;
        }

    }
}
