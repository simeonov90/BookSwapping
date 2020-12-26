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
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly ILibraryService libraryService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(IBookService bookService, IGenreService genreService, ILibraryService libraryService, UserManager<ApplicationUser> userManager)
        {
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
                book.Genre = await  this.genreService.GetAllGenre();
                return View(book);
            }

            book.UserId = userManager.GetUserId(HttpContext.User);
            await this.bookService.CreateBook(book);

            return RedirectToAction("MyBook");
        }

        [AllowAnonymous]
        public async Task<IActionResult> MyBook(GetAllFromUserBookInputModel getAllBook)
        {
           getAllBook.UserId = userManager.GetUserId(HttpContext.User);
            
            return View(await this.bookService.GetAllBooksFromUser(getAllBook));
        }

        [Route("ShareBookToLibrary")]
        [Authorize]
        public async Task<IActionResult> ShareBookToLibrary(int id)
        {      
            var date = DateTime.UtcNow;
            var onlyDate = date.Date.ToString("dd/MM/yyyy");

            await this.libraryService.ShareBookToLibrary(id, onlyDate);

            return RedirectToAction("MyBook");
        }

    }
}
