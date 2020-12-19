namespace BookSwapping.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Library
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
