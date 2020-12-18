namespace BookSwapping.Data.Models
{
    using BookSwapping.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.AuthorMinLength)]
        [MaxLength(GlobalConstants.AuthorMaxLength)]
        public string Name { get; set; }

        public string Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}