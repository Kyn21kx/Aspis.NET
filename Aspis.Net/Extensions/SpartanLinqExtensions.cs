using Microsoft.EntityFrameworkCore;
using AspisNet.Utils.ApiOperations;
using AspisNet.Utils;

namespace AspisNet.Extensions;

public static class SpartanLinqExtensions {

	public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int size)
	{
		return query.Skip(pageIndex * size).Take(size);
	}

	public static async Task<Paginated<T>> ToPaginatedCollectionAsync<T>(this IQueryable<T> query, int page, int size, int totalCount)
	{
		int totalPages = MathUtils.CalculateTotalPageCount(totalCount, size);
		if (page >= totalPages)
		{
			throw new ApiOperationException(
				$"Can't get page {page} of {totalPages}, forgot to use 0 based indexing or provide values?",
				ApiOperationStatus.ValidationError
			);
		}
		T[] collection = await query.ToArrayAsync();
		return Paginated<T>.Of(collection, page, totalCount, size);
	}

}
