namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using System.Threading.Tasks;
    public interface IBookCoverService
    {
        Task CreateBookCoverAsync(string bookName, byte[] imageContent, string description);
        Task<BookCover> GetBookCover(int id);
        Task Delete(int id);
    }
}
