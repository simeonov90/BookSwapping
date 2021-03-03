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
        Task<AboutBookViewModel> AboutBook(int bookId);
        Task<IEnumerable<BookDetailsViewModel>> BookDetails(int id);
        Task<EditBookInputViewModel> GetBookForEdit(int id);
        Task UpdateEditBook(int bookId, EditBookInputViewModel edit);
        Task<bool> UserBookExist(int bookId, string userId);
    }
}
