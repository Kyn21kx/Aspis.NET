using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace AspisNet.Database; 

public class Repository<T, PK> : IRepository<T, PK> where PK : struct where T : DbEntity<PK>
{
#if DEBUG
		public readonly AspisDbContext context;
#else
		protected readonly AppDbContext context;
#endif
	public Repository(AspisDbContext context)
	{
		this.context = context;
	}

	public void Delete(T entity)
	{
		context.Set<T>().Remove(entity);
	}

	public async Task DeleteAsync(T entity)
	{
		Delete(entity);
		await context.SaveChangesAsync();
	}

	public async Task DeleteManyAsync(ICollection<T> entities)
	{
		context.Set<T>().RemoveRange(entities);
		await context.SaveChangesAsync();
	}

	public T? FirstOrDefault(PK id)
	{
		return context.Set<T>().Find(id);
	}

	public async Task<T?> FirstOrDefaultAsync(PK id)
	{
		return await context.Set<T>().FindAsync(id);
	}

	public IQueryable<T> GetAll()
	{
		return context.Set<T>().AsQueryable<T>();
	}

	public async Task<IQueryable<T>> GetAllAsync()
	{
		return await Task.FromResult(GetAll());
	}

	public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
	{
		IQueryable<T> query = context.Set<T>().AsQueryable();
		foreach (Expression<Func<T, object>> expression in propertySelectors)
		{
			query = query.Include(expression);
		}

		return query;
	}

	public async Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors)
	{
		return await Task.FromResult(GetAllIncluding(propertySelectors));
	}

	public T Insert(T entity)
	{
		return context.Set<T>().Add(entity).Entity;
	}

	public async Task<T> InsertAsync(T entity)
	{
		EntityEntry<T> entry = await context.Set<T>().AddAsync(entity);
		await context.SaveChangesAsync();
		return entry.Entity;
	}

	public async Task InsertManyAsync(ICollection<T> entities)
	{
		await context.Set<T>().AddRangeAsync(entities);
		await context.SaveChangesAsync();
	}

	public R Query<R>(Func<IQueryable<T>, R> queryMethod)
	{
		return queryMethod(GetAll());
	}

	public async Task<R> QueryAsync<R>(Func<IQueryable<T>, R> queryMethod)
	{
		return await Task.FromResult(Query(queryMethod));
	}

	public T Update(T entity)
	{
		var result = context.Set<T>().Update(entity).Entity;
		context.SaveChanges();
		return result;
	}

	public async Task<T> UpdateAsync(T entity)
	{
		return await Task.FromResult(Update(entity));
	}

	public async Task<IQueryable> UpdateManyAsync(ICollection<T> entities)
	{
		context.Set<T>().UpdateRange(entities);
		await context.SaveChangesAsync();
		return entities.AsQueryable();
	}
}
