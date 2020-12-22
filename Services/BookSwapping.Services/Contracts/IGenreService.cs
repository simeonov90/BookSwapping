namespace BookSwapping.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<List<string>> GetAllGenre();
    }
}
