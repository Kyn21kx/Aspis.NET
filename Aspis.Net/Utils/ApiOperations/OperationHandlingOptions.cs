using AspisNet.Network;

namespace AspisNet.Utils.ApiOperations {
	public class OperationHandlingOptions {

		public MimeTypes MimeType { get; set; } = MimeTypes.ApplicationJson;

		public string? FileNameOnDownload { get; set; }

	}
}
