namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Library;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext db;

        public LibraryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> CountOfBooksInLibrary()
        {
            return await db.Libraries.CountAsync();
        }

        public async Task UnShareBookFromLibrary(int id)
        {
                var library = await db.Libraries.Where(x => x.BookId == id).FirstOrDefaultAsync();
                this.db.Libraries.Remove(library);
                await this.db.SaveChangesAsync();          
        }

        public async Task<IEnumerable<GetAllSharedBooksFromUserViewModel>> GetAllSharedBooksFromUser(string username) 
        {
            var book = await this.db.Libraries.Where(x => x.Book.User.UserName == username)
                .Select(x => new GetAllSharedBooksFromUserViewModel
                {
                    ImageContent = x.Book.BookCover.ImageContent,
                    BookName = x.Book.BookCover.BookName,
                    BookId = x.BookId,
                })
                .AsNoTracking()
                .ToListAsync();

                return book;
        }
        public async Task<IEnumerable<GetAllBooksFromLibraryViewModel>> GetAllBooksFromLibrary()
        {

            var getAllBookFromLibrary = db.Libraries
                .Select(x => new GetAllBooksFromLibraryViewModel
                {
                    ImageContent = x.Book.BookCover.ImageContent,
                    BookName = x.Book.BookCover.BookName,
                    AuthorName = x.Book.Author.Name,
                    Genre = x.Book.Genre.TypeGenre,
                    DateTime = x.Date,
                    UploadBy = x.Book.User.UserName,
                    BookId = x.BookId,
                    AuthorId = x.Book.AuthorId
                })
                .AsNoTracking()
                .ToListAsync();
            
            return await getAllBookFromLibrary;
        }

        public async Task<IEnumerable<LastReceivedBooksToLibraryViewModel>> LastReceivedBooksToLibrary()
        {

            var lastReceivedBooks = db.Libraries
                .Select(x => new LastReceivedBooksToLibraryViewModel
                {
                    ImageContent = x.Book.BookCover.ImageContent,
                    BookName = x.Book.BookCover.BookName,
                    AuthorName = x.Book.Author.Name,
                    BookId = x.BookId,
                    AuthorId = x.Book.AuthorId,
                    LibraryId = x.Id
                })
                .OrderByDescending(x => x.LibraryId)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            return await lastReceivedBooks;
        }

        public async Task ShareBookToLibrary(int bookId)
        {
            var library = new Library
            {
                BookId = bookId,
            };

            if (!db.Libraries.Select(x => x.BookId).Contains(bookId))
            {
                await this.db.Libraries.AddAsync(library);
                await this.db.SaveChangesAsync();
            }

        }

        public async Task<bool> IsBookShared(int id)
        {
            var isExist = await db.Libraries.AnyAsync(x => x.BookId == id);
            return isExist;
        }
        public async Task<bool> DoesTheUserHaveBooksToDisplay(string username)
        {
            var bookToDisplay = await this.db.Libraries.AnyAsync(x => x.Book.User.UserName == username);

            return bookToDisplay;
        }
    }
}
