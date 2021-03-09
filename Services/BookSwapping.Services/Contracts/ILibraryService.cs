namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Models.ViewModels.Library;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ILibraryService
    {
        Task ShareBookToLibrary(int bookId);
        Task UnShareBookFromLibrary(int id);
        Task<bool> IsBookShared(int id);
        Task<IEnumerable<GetAllBooksFromLibraryViewModel>> GetAllBooksFromLibrary();
        Task<int> CountOfBooksInLibrary();
        Task<IEnumerable<LastReceivedBooksToLibraryViewModel>> LastReceivedBooksToLibrary();
        Task<IEnumerable<GetAllSharedBooksFromUserViewModel>> GetAllSharedBooksFromUser(string userId);
        Task<bool> DoesTheUserHaveBooksToDisplay(string username);
    }
}
