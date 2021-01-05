namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [Authorize]
        public async Task<IActionResult> Author(int id, string name)
        {
            ViewData["Author"] = name;
            return View(await this.authorService.GetAllAuthorBooks(id));
        }
    }
}
