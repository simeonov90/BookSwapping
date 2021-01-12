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
        private readonly IGenreService genreService;
        private readonly ApplicationDbContext db;

        public BookService
           (
            IAuthorService authorService,
            IBookCoverService bookCoverService,
            IGenreService genreService,
            ApplicationDbContext db
            )
        {
            this.authorService = authorService;
            this.bookCoverService = bookCoverService;
            this.genreService = genreService;
            this.db = db;
        }

        public async Task CreateBook(CreateBookInputModel create)
        {

            var memoryStream = new MemoryStream();
            var image = SixLabors.ImageSharp.Image.Load(create.FormFile.OpenReadStream());
            image.Mutate(x => x.Resize(200, 240));
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

        public async Task<ICollection<Book>> GetAllBooksFromUser(GetAllFromUserBookInputModel getAllBook)
        {
            var book = await db.Books.Where(x => x.UserId == getAllBook.UserId)
                .Include(c => c.BookCover)
                .Include(c => c.Author)
                .Include(c => c.Genre)
                .AsNoTracking()
                .ToListAsync();
            return book;
        }
        public async Task<ICollection<Book>> BookDetails(int id)
        {
            var book = await db.Books.Where(x => x.Id == id)
                .Include(x => x.BookCover)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();

            return book;
        }

        public async Task<EditBookInputViewModel> GetBookForEdit(int id)
        {
            var book = await db.Books.Where(x => x.Id == id)
                .Include(x => x.BookCover)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .AsNoTracking()
                .ToListAsync();

            EditBookInputViewModel edit = new EditBookInputViewModel();

            foreach (var b in book)
            {
                edit.BookName = b.BookCover.BookName;
                edit.Author = b.Author.Name;
                edit.TypeGenre = b.Genre.TypeGenre;
                edit.Genre = await this.genreService.GetAllGenre();
                edit.Description = b.BookCover.Description;
                edit.ExistingPhotoPath = b.BookCover.ImageContent;
            }

            return edit;
        }

        public async Task UpdateEditBook(int id, EditBookInputViewModel edit)
        {

            Book book = await db.Books.Where(x => x.Id == id)
                .Include(x => x.BookCover)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            book.BookCover.BookName = edit.BookName;
            book.Author.Name = edit.Author;
            book.Genre.TypeGenre = edit.TypeGenre;
            if (edit.FormFile != null)
            {
                var memoryStream = new MemoryStream();
                var image = SixLabors.ImageSharp.Image.Load(edit.FormFile.OpenReadStream());
                image.Mutate(x => x.Resize(200, 240));
                await image.SaveAsPngAsync(memoryStream);

                book.BookCover.ImageContent = memoryStream.ToArray();
            }
            book.BookCover.Description = edit.Description;
            db.BookCovers.Update(book.BookCover);
            db.Authors.Update(book.Author);
            db.Genres.Update(book.Genre);
            await db.SaveChangesAsync();
        }
    }
}
