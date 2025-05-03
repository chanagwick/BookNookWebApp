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

            // Topic to Comment relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TopicId)
                .OnDelete(DeleteBehavior.NoAction);

            // User to Comment relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Self-referencing Comment relationship (Replies)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete loops
        }
    }
}
