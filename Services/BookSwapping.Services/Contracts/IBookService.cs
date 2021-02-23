namespace BookSwapping.Services.Contracts
{
    using BookSwapping.Data.Models;
    using BookSwapping.Models.InputModels.Book;
    using BookSwapping.Models.ViewModels.Book;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IBookService
    {
        Task CreateBook(CreateBookInputModel create, string userId);
        Task<OptionsViewModel> Options(int bookId);
        Task<ICollection<Book>> BookDetails(int id);
        Task<EditBookInputViewModel> GetBookForEdit(int id);
        Task UpdateEditBook(int id, EditBookInputViewModel edit);
        Task<bool> UserBookExist(int bookId, string userId);
    }
}
