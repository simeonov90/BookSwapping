namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ILibraryService
    {
        Task ShareBookToLibrary(int bookId);
        Task UnShareBookFromLibrary(int id);
        Task<bool> IsBookShared(int id);
        Task<ICollection<Library>> GetAllBookFromLibrary();
        Task<int> CountOfBooksInLibrary();
        Task<ICollection<Library>> LastReceivedBooksToLibrary();
    }
}
