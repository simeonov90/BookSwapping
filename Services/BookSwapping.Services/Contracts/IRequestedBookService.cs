namespace BookSwapping.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IRequestedBookService
    {
        Task<bool> DidIWantThisBook(string userId, int bookId);
        Task<bool> ItsMineBook(string userId, int bookId);
        Task RequestThisBook(string username, int bookId);
        Task RejectTheRequest(string userId, int bookId);
        Task<IEnumerable<string>> ListWithUsers(int bookId);
    }
}
