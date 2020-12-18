namespace BookSwapping.Data.Models
{
    using BookSwapping.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string TypeGenre { get; set; }

        [MaxLength(GlobalConstants.GenreDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
