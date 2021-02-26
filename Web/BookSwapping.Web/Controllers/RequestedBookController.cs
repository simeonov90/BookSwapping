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

        public RequestedBookController(IRequestedBookService requestedBookService)
        {
            this.requestedBookService = requestedBookService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequestThisBook(string userId, int bookId)
        {
            if (await requestedBookService.ItsMineBook(this.User.GetUserId(), bookId))
            {
                return BadRequest();
            }

            await this.requestedBookService.RequestThisBook(userId, bookId);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RejectTheRequest(string userId, int bookId)
        {
            if (!await requestedBookService.DidIWantThisBook(this.User.GetUserId(), bookId))
            {
                await this.requestedBookService.RejectTheRequest(userId, bookId);

                return RedirectToAction("Index", "Home");
            }

            return BadRequest();

        }
    }
}
