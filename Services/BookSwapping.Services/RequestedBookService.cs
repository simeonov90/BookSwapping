using BookSwapping.Data;
using BookSwapping.Services.Contracts;

namespace BookSwapping.Services
{
    public class RequestedBookService : IRequestedBookService
    {
        private readonly ApplicationDbContext db;

        public RequestedBookService(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
