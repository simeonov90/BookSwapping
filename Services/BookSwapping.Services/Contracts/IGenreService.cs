namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Genre;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<IEnumerable<string>> GetAllGenre();
        Task<IEnumerable<GetAllBooksFromGenreViewModel>> GetAllBooksFromGenre(string genre);
    }
}
