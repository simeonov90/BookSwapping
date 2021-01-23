namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class BookController : Controller
    {
        private readonly IBookCoverService bookCoverService;
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly ILibraryService libraryService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(IBookCoverService bookCoverService, IBookService bookService, IGenreService genreService, ILibraryService libraryService, UserManager<ApplicationUser> userManager)
        {
            this.bookCoverService = bookCoverService;
            this.bookService = bookService;
            this.genreService = genreService;
            this.libraryService = libraryService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> CreateBook()
        {
            var viewModel = new CreateBookInputModel();
            viewModel.Genre = await this.genreService.GetAllGenre();
            return View(viewModel);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookInputModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genre = await this.genreService.GetAllGenre();
                return View(book);
            }

            book.UserId = userManager.GetUserId(this.User);
            await this.bookService.CreateBook(book);

            return RedirectToAction("MyBook");
        }

        [Authorize]
        public async Task<IActionResult> MyBook(GetAllFromUserBookInputModel getAllBook)
        {
           getAllBook.UserId = userManager.GetUserId(this.User);
            
            return View(await this.bookService.GetAllBooksFromUser(getAllBook));
        }
        [Authorize]
        public async Task<IActionResult> BookDetails(int id)
        {
            if (await this.libraryService.IsBookShared(id) != 0)
            {
                return View(await this.bookService.BookDetails(id));
            }

            return RedirectToAction("Error", "Home");
        }

        [Authorize]
        public async Task<IActionResult> ShareBookToLibrary(int id)
        {      
            var date = DateTime.UtcNow;
            var onlyDate = date.Date.ToString("dd/MM/yyyy");

            await this.libraryService.ShareBookToLibrary(id, onlyDate);

            return RedirectToAction("MyBook");
        }

        [Authorize]
        public async Task<IActionResult> UnShareBookFromLibrary(int id)
        {
            await this.libraryService.UnShareBookFromLibrary(id);

            return RedirectToAction("MyBook");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id, string userId)
        {
            var currUser = userManager.GetUserId(this.User);
            if (currUser != userId)
            {
                return View("Error", "Home");
            }
            return View(await this.bookService.GetBookForEdit(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookInputViewModel edit)
        {            
            await this.bookService.UpdateEditBook(id, edit);
            return RedirectToAction("MyBook");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            var currUser = userManager.GetUserId(this.User);
            
            if (id == 0 || userId != currUser)
            {
                return View("Error", "Home");
            }
            
            return View(await this.bookCoverService.GetBookCover(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.bookCoverService.Delete(id);
            return RedirectToAction("MyBook");
        }
    }
}
