using System.Linq.Expressions;

namespace AspisNet.Database;

public interface IRepository<T, PK> where PK : struct where T : DbEntity<PK>
{

	IQueryable<T> GetAll();

	Task<IQueryable<T>> GetAllAsync();

	T? FirstOrDefault(PK id);

	Task<T?> FirstOrDefaultAsync(PK id);

	IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors);

	Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors);

	R Query<R>(Func<IQueryable<T>, R> queryMethod);

	Task<R> QueryAsync<R>(Func<IQueryable<T>, R> queryMethod);

	T Insert(T entity);

	Task<T> InsertAsync(T entity);

	T Update(T entity);

	void Delete(T entity);

	Task DeleteAsync(T entity);

	Task<T> UpdateAsync(T entity);

	Task InsertManyAsync(ICollection<T> entities);

	Task<IQueryable> UpdateManyAsync(ICollection<T> entities);

	Task DeleteManyAsync(ICollection<T> entities);

}
