namespace AspisNet.Utils.ApiOperations;

public class Paginated<T> {
	public ICollection<T> Items { get; private set; }

	public int Size => Items.Count;

	public int CurrentPage { get; private set; }

	public int TotalPages => MathUtils.CalculateTotalPageCount(TotalElements, MaxElements);

	public int TotalElements { get; private set; }

	public int RemainingElements => Math.Clamp(TotalElements - (MaxElements * (CurrentPage + 1)), 0, int.MaxValue);

	public bool IsEmpty => Size < 1;

	private int MaxElements { get; set; }

	private Paginated(ICollection<T> items, int currentPage, int totalElements, int maxElements)
	{
		this.Items = items;
		this.CurrentPage = currentPage;
		this.TotalElements = totalElements;
		this.MaxElements = maxElements;
	}

	public static Paginated<T> Of(ICollection<T> items, int currentPage, int totalElements, int maxElements) {
		var result = new Paginated<T>(items, currentPage, totalElements, maxElements);
		return result;
	}

	public static Paginated<T> Empty()
	{
		return new Paginated<T>(Array.Empty<T>(), 0, 0, 0);
	}

}
