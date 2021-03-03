namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Models.ViewModels.Book;
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Http;
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

        public async Task CreateBook(CreateBookInputModel create, string userId)
        {

            await this.bookCoverService.CreateBookCoverAsync(create.BookName, CreateImageAsync(create.FormFile), create.Description);
            await this.authorService.CreateAuthorAsync(create.Author);

            var authorId = db.Authors.Where(a => a.Name == create.Author).Select(a => a.Id).FirstOrDefault();
            var bookCoverId = db.BookCovers.Where(b => b.BookName == create.BookName).ToList().Select(x => x.Id).Last();
            var genreId = db.Genres.Where(g => g.TypeGenre == create.TypeGenre).Select(g => g.Id).FirstOrDefault();

            var books = new Book
            {
                AuthorId = authorId,
                BookCoverId = bookCoverId,
                GenreId = genreId,
                UserId = userId
            };

            await this.db.Books.AddAsync(books);
            await this.db.SaveChangesAsync();
        }
        public async Task<bool> UserBookExist(int bookId, string userId)
        {
            var exist = await db.Books.AnyAsync(x => x.Id == bookId && x.UserId == userId);
            return exist;
        }

        public async Task<AboutBookViewModel> AboutBook(int bookId)
        {
            var book = await db.Books.Where(c => c.Id == bookId)
                .Include(c => c.BookCover)
                .Include(c => c.Author)
                .Include(c => c.Genre)
                .Select(c => new AboutBookViewModel
                {
                    ImageContent = c.BookCover.ImageContent,
                    Name = c.BookCover.BookName,
                    Author = c.Author.Name,
                    Description = c.BookCover.Description,
                    Genre = c.Genre.TypeGenre,
                    Id = c.Id,
                    BookCoverId = c.BookCoverId
                })
                .FirstAsync();
                
            
            return book;
        }
        public async Task<IEnumerable<BookDetailsViewModel>> BookDetails(int id)
        {
            var book = await db.Books.Where(x => x.Id == id)
            .Select(x => new BookDetailsViewModel
            {
                ImageContent = x.BookCover.ImageContent,
                BookName = x.BookCover.BookName,               
                Description = x.BookCover.Description,
                AuthorName = x.Author.Name,                
                TypeGenre = x.Genre.TypeGenre,
                UploadBy = x.User.UserName,
                BookId = x.Id,
                AuthorId = x.AuthorId,
            })
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
                .Select(x => new EditBookInputViewModel
                {
                    BookName = x.BookCover.BookName,
                    Author = x.Author.Name,
                    TypeGenre = x.Genre.TypeGenre,
                    Description = x.BookCover.Description,
                    ExistingPhotoPath = x.BookCover.ImageContent
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            book.GetAllAuthor = await this.authorService.GetAllAuthor();
            book.GetAllGenre = await this.genreService.GetAllGenre();

            return book;
        }
        public async Task UpdateEditBook(int id, EditBookInputViewModel edit)
        {
            var book = await db.Books.Where(x => x.Id == id)
                .Include(x => x.BookCover)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var genreEditId = await db.Genres.Where(x => x.TypeGenre == edit.TypeGenre).Select(x => x.Id).FirstOrDefaultAsync();
            var authorEditId = await db.Authors.Where(x => x.Name == edit.Author).Select(x => x.Id).FirstOrDefaultAsync();

            if (authorEditId != 0)
            {
                book.AuthorId = authorEditId;
            }
            else
            {
                await this.authorService.CreateAuthorAsync(edit.Author);

                var newAuthorId = await db.Authors.Where(x => x.Name == edit.Author).Select(x => x.Id).FirstOrDefaultAsync();
                book.AuthorId = newAuthorId;
            }

            if (edit.FormFile != null)
            {
                book.BookCover.ImageContent = await CreateImageAsync(edit.FormFile);
            }

            book.BookCover.BookName = edit.BookName;
            book.GenreId = genreEditId;
            book.BookCover.Description = edit.Description;

            db.BookCovers.Update(book.BookCover);
            db.Books.Update(book);

            await db.SaveChangesAsync();
        }

        private async Task<byte[]> CreateImageAsync(IFormFile formFile)
        {
            var memoryStream = new MemoryStream();
            var image = SixLabors.ImageSharp.Image.Load(formFile.OpenReadStream());
            image.Mutate(x => x.Resize(200, 240));
            await image.SaveAsPngAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
