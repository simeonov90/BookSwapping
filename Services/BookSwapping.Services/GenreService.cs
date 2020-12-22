namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext db;

        public GenreService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<string>> GetAllGenre()
        {
            return await this.db.Genres.Select(x => x.TypeGenre).ToListAsync();
        }
    }
}
