namespace BookSwapping.Web.Controllers
{
    using BookSwapping.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]

    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet("author/{name}")]
        public async Task<IActionResult> Author(int id, string name)
        {
            if (!await this.authorService.AuthorExist(id, name))
            {
                return NotFound();
            }

            ViewData["Author"] = name;

            return View(await this.authorService.GetAllAuthorBooks(id));
        }
    }
}
