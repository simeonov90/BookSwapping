using BookSwapping.Models.ViewModels.Genre;

namespace BookSwapping.Models.ViewModels.Library
{
    public class LastReceivedBooksToLibraryViewModel : GetAllBooksFromGenreViewModel
    {
        public int LibraryId { get; set; }
    }
}
