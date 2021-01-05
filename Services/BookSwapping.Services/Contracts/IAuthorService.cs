namespace BookSwapping.Services
{
    using BookSwapping.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAuthorService
    {
        Task CreateAuthorAsync(string name);
        Task<ICollection<Library>> GetAllAuthorBooks(int id);
    }

}
