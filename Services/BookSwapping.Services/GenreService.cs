namespace BookSwapping.Services
{
    using BookSwapping.Data;
    using BookSwapping.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext db;

        public GenreService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<string> GetAllGenre()
        {
            return this.db.Genres.Select(x => x.TypeGenre).ToList();
        }
    }
}
