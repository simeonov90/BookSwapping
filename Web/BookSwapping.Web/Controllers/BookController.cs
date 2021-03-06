﻿namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BookSwapping.Web.Infrastructure.Claims;
    using System.Threading.Tasks;
    using BookSwapping.Services;

    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookCoverService bookCoverService;
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly ILibraryService libraryService;
        private readonly IAuthorService authorService;

        public BookController(IBookCoverService bookCoverService,
            IBookService bookService,
            IGenreService genreService,
            ILibraryService libraryService,
            IAuthorService authorService)
        {
            this.bookCoverService = bookCoverService;
            this.bookService = bookService;
            this.genreService = genreService;
            this.libraryService = libraryService;
            this.authorService = authorService;
        }

        
        public async Task<IActionResult> Book()
        {
           return View(await this.bookCoverService.GetAllByUser(this.User.GetUserId()));
        }

        
        public async Task<IActionResult> AboutBook(int id)
        {
            if (!await this.bookService.UserBookExist(id, this.User.GetUserId()))
            {
               return NotFound();
            }

            return View(await this.bookService.AboutBook(id));
        }

        public async Task<IActionResult> CreateBook()
        {
            var viewModel = new CreateBookInputModel();
            viewModel.GetAllGenre = await this.genreService.GetAllGenre();
            viewModel.GetAllAuthor = await this.authorService.GetAllAuthor();
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookInputModel book)
        {
            if (!ModelState.IsValid)
            {
                book.GetAllGenre = await this.genreService.GetAllGenre();
                return View(book);
            }

            await this.bookService.CreateBook(book, this.User.GetUserId());

            return RedirectToAction(nameof(Book));
        }

        [HttpGet]
        public async Task<IActionResult> BookDetails(int id)
        {
            if (await this.libraryService.IsBookShared(id) == false)
            {
                return NotFound();
            }

            return View(await this.bookService.BookDetails(id));
        }

        public async Task<IActionResult> ShareBookToLibrary(int id)
        {

            if (!await this.bookService.UserBookExist(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            await this.libraryService.ShareBookToLibrary(id);

            return RedirectToAction(nameof(Book));

        }

        public async Task<IActionResult> UnShareBookFromLibrary(int id)
        {
            if (!await this.bookService.UserBookExist(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            await this.libraryService.UnShareBookFromLibrary(id);

            return RedirectToAction(nameof(Book));
        }

        public async Task<IActionResult> Edit(int bookId)
        {
            if (!await this.bookService.UserBookExist(bookId, this.User.GetUserId()))
            {
                return NotFound();
            }
            return View(await this.bookService.GetBookForEdit(bookId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int bookId, EditBookInputViewModel edit)
        {
            await this.bookService.UpdateEditBook(bookId, edit);
            return RedirectToAction("Book");
        }

        public async Task<IActionResult> Delete(int bookId, int bookCoverId)
        {
            if (!await this.bookService.UserBookExist(bookId, this.User.GetUserId()))
            {
                return NotFound();
            }

            return View(await this.bookCoverService.GetBookCover(bookCoverId));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int bookCoverId)
        {

            await this.bookCoverService.Delete(bookCoverId);
            return RedirectToAction(nameof(Book));
        }
    }
}
