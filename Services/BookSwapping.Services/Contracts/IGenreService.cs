namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<IEnumerable<string>> GetAllGenre();
        Task<ICollection<Book>> GetAllBooksFromGenre(string genre);
    }
}
