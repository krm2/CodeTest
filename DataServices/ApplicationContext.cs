using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataServices
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
    }
}
