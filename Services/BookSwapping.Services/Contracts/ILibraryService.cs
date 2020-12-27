namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.ViewModels.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ILibraryService
    {
        Task ShareBookToLibrary(int bookId, string date);
        Task DeleteBookFromLibrary(int id);
        Task<ICollection<Library>> GetAllBookFromLibrary();
        int CountOfBooksInLibrary();
        Task<ICollection<Library>> LastReceivedBooksToLibrary();
    }
}
