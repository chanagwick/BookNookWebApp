using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookNookWebApp.Models;

namespace BookNookWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Existing DbSets
        public DbSet<Books> Books { get; set; } = default!;
        public DbSet<Forums> Forums { get; set; } = default!;

        // New DbSets
        public DbSet<Topics> Topics { get; set; } = default!;
        public DbSet<ForumPost> ForumPosts { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Add any relationships or constraints

            // Topic to ForumPost relationship
            modelBuilder.Entity<Topics>()
                .HasMany(t => t.ForumPosts)
                .WithOne(p => p.Topic)
                .HasForeignKey(p => p.TopicId)
                .OnDelete(DeleteBehavior.NoAction);

            // ForumPost to Comment relationship
            modelBuilder.Entity<ForumPost>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.ForumPost)
                .HasForeignKey(c => c.ForumPostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
