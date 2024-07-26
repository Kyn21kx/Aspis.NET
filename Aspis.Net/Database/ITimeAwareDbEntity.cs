namespace AspisNet.Database;

public interface ITimeAwareDbEntity {

	void SetCreatedAt(DateTime createdAt);

	void SetLastUpdatedAt(DateTime updatedAt);

}
