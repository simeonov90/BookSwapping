namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using System.Threading.Tasks;
    public interface IBookCoverService
    {
        Task CreateBookCoverAsync(string bookName, Task<byte[]> imageContent, string description);
        Task<BookCover> GetBookCover(int id);
        Task<bool> Delete(int id);
    }
}
