using BlogProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>// Identity'yi buraya entegre ediyoruz
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Tabloları belirtiyoruz:
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        // İlişkileri kurabiliriz (isteğe bağlı)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Blog ↔ User (One to Many)
            builder.Entity<BlogPost >()
                .HasOne(b => b.User)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog ↔ Category (Many to One)
            builder.Entity<BlogPost>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Comment ↔ Blog (Many to One)
            builder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment ↔ User (Many to One)
            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // 🔧 bu satır düzeltildi
                                                    // Like ↔ Blog (Many to One)
            builder.Entity<Like>()
                .HasOne(l => l.Blog)
                .WithMany(b => b.Likes)
                .HasForeignKey(l => l.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Like ↔ User (Many to One)
            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}