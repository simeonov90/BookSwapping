namespace BookSwapping.Services.Contracts
{
    using System.Threading.Tasks;
    public interface IBookCoverService
    {
        Task CreateBookCoverAsync(string bookName, byte[] imageContent, string description);
    }
}
