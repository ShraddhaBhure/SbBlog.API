using Microsoft.EntityFrameworkCore;
using SbBlog.API.Models.Entities;

namespace SbBlog.API.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext( DbContextOptions options) : base (options)    
        {
            
        }
        public DbSet <Post> Posts { get; set; }
    }
    
}
