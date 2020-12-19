namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IBookService
    {
        Task CreateBook(CreateBookInputModel create);
        ICollection<Book> GetAllBooksFromUser(GetAllFromUserBookInputModel getAllBook);
        ICollection<Book> GetAllBooksCoverAndAuthor();
    }
}
