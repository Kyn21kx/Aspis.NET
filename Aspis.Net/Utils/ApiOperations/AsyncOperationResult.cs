
namespace AspisNet.Utils.ApiOperations {

	public class AsyncOpResult<T> : Task<ApiOperationResult<T>> {
		public AsyncOpResult(Func<object?, ApiOperationResult<T>> function, object? state) : base(function, state)
		{
		}
	}

	public class AsyncOpResult : Task<ApiOperationResult> {
		public AsyncOpResult(Func<object?, ApiOperationResult> function, object? state) : base(function, state)
		{
		}
	}
}
