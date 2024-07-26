namespace AspisNet.Utils.ApiOperations {
	/// <summary>
	/// Possible results of any Logic operation (they should be used in a wrapper to return the proper status codes)
	/// The API could return more than one of these in the form or a binary or operation, so check for <see cref="Enum.HasFlag(Enum)"/>
	/// </summary>
	[Flags]
	public enum ApiOperationStatus : uint {
		None = 0u,
		Success = 1u,
		Created = 2u,
		Updated = 3u,
		/// <summary>
		/// The input of a process was not in the correct format
		/// </summary>
		ValidationError = 4u << 0,
		/// <summary>
		/// Tried to fetch data that does not exist in the Database
		/// </summary>
		EntityNotFoundError = 4u << 1,
		/// <summary>
		/// Tried to post data, but it currently exists in the Database
		/// </summary>
		DataConflictError = 4u << 2,
		/// <summary>
		/// A user tried to access a resource that they're not allowed to / they have incorrect credentials
		/// </summary>
		AuthorizationError = 4u << 3,
		/// <summary>
		/// Some behaviour internally in the program did not go as expected
		/// </summary>
		InternalError = 4u << 4,
		UnkownError = 4u << 5,
		IsErrorStatus = ValidationError | EntityNotFoundError | DataConflictError | AuthorizationError | InternalError | UnkownError
	}
}
