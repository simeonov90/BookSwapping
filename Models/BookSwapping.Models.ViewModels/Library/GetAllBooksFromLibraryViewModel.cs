using System;

namespace BookSwapping.Models.ViewModels.Library
{
    public class GetAllBooksFromLibraryViewModel
    {
        public byte[] ImageContent { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public DateTime DateTime { get; set; }
        public string UploadBy { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
