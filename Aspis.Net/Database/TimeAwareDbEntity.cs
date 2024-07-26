namespace AspisNet.Database;

public class TimeAwareDbEntity<T> : DbEntity<T>, ITimeAwareDbEntity where T : struct {

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

	public void SetCreatedAt(DateTime createdAt)
	{
		this.CreatedAt = createdAt;
	}

	public void SetLastUpdatedAt(DateTime updatedAt)
	{
		this.LastUpdatedAt = updatedAt;
	}
}
