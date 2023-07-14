
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models.Model;

namespace MyWebApplication.DataAccessLayer
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
