using Microsoft.EntityFrameworkCore;

namespace E_Commerce_DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
    {
    }


    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();



    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;

            if (entry.State == EntityState.Deleted) //&& entity is ISoftDelete
                entry.State = EntityState.Modified;
                entity.GetType().GetProperty("IsDelete")?.SetValue(entity, true);
        }
        return base.SaveChanges();
    }
}
