namespace BookSwapping.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Library
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public Library()
        {
            Date = DateTime.UtcNow;
        }
    }
}
