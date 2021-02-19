using System.Collections.Generic;

namespace BookSwapping.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int BookCoverId { get; set; }
        public virtual BookCover BookCover { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Library Libraries { get; set; }
        public ICollection<RequestedBook> RequestedBooks { get; set; } = new List<RequestedBook>();
    }
}
