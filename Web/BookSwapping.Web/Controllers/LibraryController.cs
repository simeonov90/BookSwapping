namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryService libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        [Route("{controller}")]
        public async Task<IActionResult> AllBookInLibrary()
        {
            return View(await this.libraryService.GetAllBooksFromLibrary());
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> UserBook(string username)
        {
            if (await this.libraryService.DoesTheUserHaveBooksToDisplay(username) != false)
            {
                return View(await this.libraryService.GetAllSharedBooksFromUser(username));
            }

            return NotFound();
        }
    }
}
