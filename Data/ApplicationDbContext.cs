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
        public DbSet<BookNookWebApp.Models.Books> Books { get; set; } = default!;
        public DbSet<BookNookWebApp.Models.Forums> Forums { get; set; } = default!;
    }
}
