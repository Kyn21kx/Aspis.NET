
namespace AspisNet.Network; 
public enum MimeTypes {
	TextPlain,
	TextHtml,
	TextCss,
	ApplicationJson,
	ApplicationXml,
	ApplicationPdf,
	ApplicationMsWord,
	ApplicationMsExcel,
	ApplicationMspowerpoint,
	ApplicationZip,
	ImageJpeg,
	ImagePng,
	ImageGif,
	AudioMpeg,
	AudioOgg,
	VideoMp4,
	VideoWebm,
	ApplicationOctetStream
};

public static class MimeTypesParser
{
	public static string ToMimeString(MimeTypes mimeType)
	{
		return mimeType switch {
			MimeTypes.TextPlain => "text/plain",
			MimeTypes.TextHtml => "text/html",
			MimeTypes.TextCss => "text/css",
			MimeTypes.ApplicationJson => "application/json",
			MimeTypes.ApplicationXml => "application/xml",
			MimeTypes.ApplicationPdf => "application/pdf",
			MimeTypes.ApplicationMsWord => "application/msword",
			MimeTypes.ApplicationMsExcel => "application/vnd.ms-excel",
			MimeTypes.ApplicationMspowerpoint => "application/vnd.ms-powerpoint",
			MimeTypes.ApplicationZip => "application/zip",
			MimeTypes.ImageJpeg => "image/jpeg",
			MimeTypes.ImagePng => "image/png",
			MimeTypes.ImageGif => "image/gif",
			MimeTypes.AudioMpeg => "audio/mpeg",
			MimeTypes.AudioOgg => "audio/ogg",
			MimeTypes.VideoMp4 => "video/mp4",
			MimeTypes.VideoWebm => "video/webm",
			MimeTypes.ApplicationOctetStream => "application/octet-stream",
			_ => throw new NotImplementedException($"MIME type {mimeType} is not implemented."),
		};
	}

	public static MimeTypes Parse(string input)
	{
		return input.ToLower() switch {
			"text/plain" => MimeTypes.TextPlain,
			"text/html" => MimeTypes.TextHtml,
			"text/css" => MimeTypes.TextCss,
			"application/json" => MimeTypes.ApplicationJson,
			"application/xml" => MimeTypes.ApplicationXml,
			"application/pdf" => MimeTypes.ApplicationPdf,
			"application/msword" => MimeTypes.ApplicationMsWord,
			"application/vnd.ms-excel" => MimeTypes.ApplicationMsExcel,
			"application/vnd.ms-powerpoint" => MimeTypes.ApplicationMspowerpoint,
			"application/zip" => MimeTypes.ApplicationZip,
			"image/jpeg" => MimeTypes.ImageJpeg,
			"image/png" => MimeTypes.ImagePng,
			"image/gif" => MimeTypes.ImageGif,
			"audio/mpeg" => MimeTypes.AudioMpeg,
			"audio/ogg" => MimeTypes.AudioOgg,
			"video/mp4" => MimeTypes.VideoMp4,
			"video/webm" => MimeTypes.VideoWebm,
			"application/octet-stream" => MimeTypes.ApplicationOctetStream,
			_ => throw new ArgumentException($"Invalid MIME type string: {input}"),
		};
	}
}
