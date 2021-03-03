namespace BookSwapping.Models.ViewModels.Book
{
    public class BookDetailsViewModel
    {
        public byte[] ImageContent { get; set; }
        public string BookName { get; set; }        
        public string Description { get; set; }     
        public string AuthorName { get; set; }      
        public string Genre { get; set; }
        public string UploadBy { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

    }
}
