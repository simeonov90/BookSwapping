using System.ComponentModel.DataAnnotations;

namespace BookSwapping.Data.Models
{
    public class RequestedBook
    {
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
