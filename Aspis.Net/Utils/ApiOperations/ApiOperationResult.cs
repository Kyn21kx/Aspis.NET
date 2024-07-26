namespace AspisNet.Utils.ApiOperations {
	public class ApiOperationResult {
        public ApiOperationStatus OperationStatus { get; set; }

		public string ErrorMessage { get; set; }

		public ApiOperationResult(ApiOperationStatus operationStatus, string errorMessage = null)
		{
			this.OperationStatus = operationStatus;
			this.ErrorMessage = errorMessage;
		}

    }

    public class ApiOperationResult<T> {
        public ApiOperationStatus OperationStatus { get; set; }

        public T Data { get; set; }

		public string? ErrorMessage { get; set; }


		public ApiOperationResult(ApiOperationStatus operationStatus, T data, string? errorMessage = null)
        {
            OperationStatus = operationStatus;
            Data = data;
			this.ErrorMessage = errorMessage;
        }

	}


	public class PaginatedOperationResult<T> : ApiOperationResult<Paginated<T>> {
		public PaginatedOperationResult(ApiOperationStatus operationStatus, Paginated<T> data, string? errorMessage = null) : base(operationStatus, data, errorMessage)
		{
		}
	}

}
