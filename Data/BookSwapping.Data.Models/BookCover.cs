namespace BookSwapping.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BookSwapping.Common;
    public class BookCover
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.BookNameMaxLength)]
        public string BookName { get; set; }

        [Required]
        public byte[] ImageContent { get; set; }

        [MaxLength(GlobalConstants.BookDescriptionMaxLength)]
        public string Description { get; set; }

        public Book Books { get; set; }
    }
}
