namespace BookSwapping.Data
{
    using BookSwapping.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<BookCover> BookCovers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            base.OnModelCreating(builder);

            //one to many
            builder.Entity<Genre>()
                .HasMany(c => c.Books)
                .WithOne(g => g.Genre)
                .IsRequired();
            builder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(a => a.Author)
                .IsRequired();
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Books)
                .WithOne(u => u.User)
                .IsRequired();
            //one to one
            builder.Entity<BookCover>()
                .HasOne(b => b.Books)
                .WithOne(b => b.BookCover)
                .HasForeignKey<Book>(b => b.BookCoverId);
        }
    }
}
