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
        public DbSet<Library> Libraries { get; set; }
        public DbSet<RequestedBook> RequestedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            base.OnModelCreating(builder);

            //many to many
            builder.Entity<RequestedBook>()
                .HasKey(rq => new { rq.UserId, rq.BookId });
            builder.Entity<RequestedBook>()
                .HasOne(rb => rb.User)
                .WithMany(rb => rb.RequestedBooks)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<RequestedBook>()
                .HasOne(rb => rb.Book)
                .WithMany(rb => rb.RequestedBooks)
                .OnDelete(DeleteBehavior.Restrict);


            //one to many
            builder.Entity<Genre>()
                .HasMany(c => c.Books)
                .WithOne(g => g.Genre)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(a => a.Author)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Books)
                .WithOne(u => u.User)
                .IsRequired();
                          
            //one to one
            builder.Entity<BookCover>()
                .HasOne(b => b.Books)
                .WithOne(b => b.BookCover)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Book>(b => b.BookCoverId);

            builder.Entity<Book>()
                .HasOne(x => x.Libraries)
                .WithOne(x => x.Book)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Library>(x => x.BookId);
        }
    }
}
