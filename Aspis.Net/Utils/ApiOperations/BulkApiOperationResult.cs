namespace AspisNet.Utils.ApiOperations {
	public struct InsertBulkApiOperationResult {

		public int Saved {  get; set; }

		public int Duplicated { get; set; }

		public int SkippedOrInvalid { get; set; }

		public int Expected {  get; set; }

		public int TotalProcessed => Saved + Duplicated + SkippedOrInvalid;

		public int DifferenceSaved => Expected - Saved;

		public int DifferenceProcessed => Expected - TotalProcessed;

		public InsertBulkApiOperationResult(int expectedInsertions)
		{
			this.Expected = expectedInsertions;
			this.Saved = 0;
			this.Duplicated = 0;
			this.SkippedOrInvalid = 0;
		}

	}
}
