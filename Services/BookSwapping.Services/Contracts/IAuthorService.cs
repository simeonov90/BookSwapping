namespace BookSwapping.Services
{
    using BookSwapping.Models.ViewModels.Author;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAuthorService
    {
        Task CreateAuthorAsync(string name);
        Task<IEnumerable<GetAllAuthorBooksViewModel>> GetAllAuthorBooks(int id);
        Task<bool> AuthorExist(int id, string name);
        Task<IEnumerable<string>> GetAllAuthor();
    }

}
