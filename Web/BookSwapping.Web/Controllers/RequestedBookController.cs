namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Services.Contracts;
    using BookSwapping.Web.Infrastructure.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class RequestedBookController : Controller
    {
        private readonly IRequestedBookService requestedBookService;
        private readonly ILibraryService libraryService;

        public RequestedBookController(IRequestedBookService requestedBookService, ILibraryService libraryService)
        {
            this.requestedBookService = requestedBookService;
            this.libraryService = libraryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequestThisBook(int bookId)
        {
            if (await requestedBookService.ItsMineBook(this.User.GetUserId(), bookId))
            {
                return BadRequest();
            }

            if (await this.libraryService.IsBookShared(bookId) == false)
            {
                return BadRequest();
            }

            await this.requestedBookService.RequestThisBook(this.User.GetUserId(), bookId);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RejectTheRequest(int bookId)
        {
            if (await requestedBookService.DidIWantThisBook(this.User.GetUserId(), bookId))
            {
                await this.requestedBookService.RejectTheRequest(this.User.GetUserId(), bookId);

                return RedirectToAction("Index", "Home");
            }

            return BadRequest();

        }
    }
}
