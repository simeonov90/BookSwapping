namespace BookSwapping.Services
{
    using System.Threading.Tasks;
    public interface IAuthorService
    {
        Task CreateAuthorAsync(string name);

    }

}
