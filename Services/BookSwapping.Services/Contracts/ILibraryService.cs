namespace BookSwapping.Services.Contracts
{
    using System;
    using System.Threading.Tasks;
    public interface ILibraryService
    {
        Task ShareBookToLibrary(int bookId, DateTime date);
    }
}
