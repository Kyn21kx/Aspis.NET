using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace AspisNet.Database; 
public class AspisDbContext : DbContext
{

	public AspisDbContext(DbContextOptions<AspisDbContext> options) : base(options)
	{
	}

	public override int SaveChanges()
	{
		IEnumerable<EntityEntry?> entries = ChangeTracker
		.Entries()
		.Where(e => e.Entity is ITimeAwareDbEntity && (
			e.State == EntityState.Added
			|| e.State == EntityState.Modified));

		foreach (EntityEntry entityEntry in entries)
		{
			(entityEntry.Entity as ITimeAwareDbEntity).SetLastUpdatedAt(DateTime.UtcNow);

			if (entityEntry.State == EntityState.Added)
			{
				(entityEntry.Entity as ITimeAwareDbEntity).SetCreatedAt(DateTime.UtcNow);
			}
		}
		return base.SaveChanges();
	}
}
