using Microsoft.EntityFrameworkCore;

namespace Session4_BoilerPlate.AddDbContext
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> Context) : base(Context)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
