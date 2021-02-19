namespace BookSwapping.Data.Models
{
    public class RequestedBook
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
