namespace BookSwapping.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public ApplicationUser(string userName) 
            : base(userName)
        {
        }

        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<RequestedBook> RequestedBooks { get; set; } = new List<RequestedBook>();
    }
}
