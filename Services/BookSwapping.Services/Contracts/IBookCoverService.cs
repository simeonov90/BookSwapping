namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.BookCover;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IBookCoverService
    {
        Task CreateBookCoverAsync(string bookName, Task<byte[]> imageContent, string description);
        Task<BookCover> GetBookCover(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<GetAllByUserViewModel>> GetAllByUser(string userId);
    }
}
