namespace BookSwapping.Models.ViewModels.Book
{
    public class AboutBookViewModel
    {
        public byte[] ImageContent { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int BookId { get; set; }
        public int BookCoverId { get; set; }


    }
}
