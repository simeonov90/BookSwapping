namespace BookSwapping.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BookSwapping.Web.Models;
    using BookSwapping.Services.Contracts;
    using BookSwapping.Models.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryService libraryService;

        public HomeController(ILogger<HomeController> logger, ILibraryService libraryService)
        {
            _logger = logger;
            this.libraryService = libraryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.libraryService.LastReceivedBooksToLibrary());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
