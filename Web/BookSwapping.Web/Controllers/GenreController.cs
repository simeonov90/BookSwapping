namespace BookSwapping.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BookSwapping.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    [Route("{controller}")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("{genre}")]
        public async Task<IActionResult> GetAllBooksFromGenre(string genre)
        {
            ViewData["Genre"] = genre;

            return View(await this.genreService.GetAllBooksFromGenre(genre));
        }
    }
}
