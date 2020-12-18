namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(IBookService bookService, IGenreService genreService, UserManager<ApplicationUser> userManager)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult CreateBook()
        {
            var viewModel = new CreateBookInputModel();
            viewModel.Genre = this.genreService.GetAllGenre();
            return View(viewModel);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookInputModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genre = this.genreService.GetAllGenre();
                return View(book);
            }

            book.UserId = userManager.GetUserId(HttpContext.User);

            await this.bookService.CreateBook(book);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyBook(GetAllMyBookInputModel getAllBook)
        {
           getAllBook.UserId = userManager.GetUserId(HttpContext.User);
            
            return View(this.bookService.GetAllMyBookAsync(getAllBook));
        }

    }
}
