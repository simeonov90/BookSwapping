namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IBookService
    {
        Task CreateBook(CreateBookInputModel create);
        Task<ICollection<Book>> GetAllBooksFromUser(GetAllFromUserBookInputModel getAllBook);
        Task<ICollection<Book>> BookDetails(int id);
        Task<EditBookInputViewModel> GetBookForEdit(int id);
        Task UpdateEditBook(int id,EditBookInputViewModel edit);
    }
}
