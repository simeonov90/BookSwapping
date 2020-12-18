namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext db;
        public AuthorService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAuthorAsync(string name)
        {

            var author = new Author()
            {                
                Name = name
            };

            var isExistAuthor = db.Authors.Where(x => x.Name == name).Select(x => x.Name).FirstOrDefault();
            if (isExistAuthor == null)
            {
                await this.db.Authors.AddAsync(author);
                await this.db.SaveChangesAsync();
            }

        }
    }
}
