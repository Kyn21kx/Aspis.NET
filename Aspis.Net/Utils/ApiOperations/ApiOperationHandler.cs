using Microsoft.AspNetCore.Mvc;
using AspisNet.Network;
using System.Net;
using System.Text.Json;

namespace AspisNet.Utils.ApiOperations;

public class ApiOperationHandler {

    public static IActionResult Handle<T>(ApiOperationResult<T> operationResult, OperationHandlingOptions? options = null)
	{
        return InternalHandler(operationResult.OperationStatus, errorMessageOnFail: operationResult.ErrorMessage, data: operationResult.Data, options: options);
    }

    public static IActionResult Handle(ApiOperationResult operationResult, OperationHandlingOptions? options = null)
    {
        return InternalHandler(operationResult.OperationStatus, errorMessageOnFail: operationResult.ErrorMessage, options: options);
    }

    public static IActionResult HandleException(ApiOperationException ex)
    {
        return InternalHandler(ex.Result, errorMessageOnFail: $"Error: {ex.Result} with message: {ex.Message}, trace: ${ex.StackTrace}");
    }

    private static HttpStatusCode GetCodeFromResultStatus(ApiOperationStatus status)
    {
        switch (status)
        {
            case ApiOperationStatus.Success:
            case ApiOperationStatus.None:
                return HttpStatusCode.OK;

            case ApiOperationStatus.Updated:
            case ApiOperationStatus.Created:
                return HttpStatusCode.Created;

            case ApiOperationStatus.DataConflictError: 
                return HttpStatusCode.Conflict;

            case ApiOperationStatus.AuthorizationError: 
                return HttpStatusCode.Unauthorized;

            case ApiOperationStatus.EntityNotFoundError: 
                return HttpStatusCode.NotFound;

				case ApiOperationStatus.ValidationError:
					return HttpStatusCode.BadRequest;

            case ApiOperationStatus.InternalError:
            case ApiOperationStatus.UnkownError:
                return HttpStatusCode.InternalServerError;
            default:
                throw new NotImplementedException($"Case for status {status} not handled yet!");
        }
    }

    private static IActionResult InternalHandler(ApiOperationStatus status, string? errorMessageOnFail = null, object? data = null, OperationHandlingOptions? options = null)
    {
        HttpStatusCode resultCode = GetCodeFromResultStatus(status);
        if (ApiOperationStatus.IsErrorStatus.HasFlag(status))
        {
            //Return the error content, otherwise return the good one
            string messageContent = errorMessageOnFail ?? $"Error: {status}";
				return new ContentResult {
					Content = messageContent,
					StatusCode = (int)resultCode,
					ContentType = MimeTypesParser.ToMimeString(MimeTypes.TextPlain)
            };
        }
			//Null data
        if (data == null)
        {
            return new ContentResult {
                Content = errorMessageOnFail, //This will print the error if it's there, otherwise, null
                StatusCode = (int)resultCode,
                ContentType = MimeTypesParser.ToMimeString(options?.MimeType ?? MimeTypes.TextPlain)
            };
        }
			return HandleResultWithData(resultCode, data, options);
    }

		private static IActionResult HandleResultWithData(HttpStatusCode resultCode, object data, OperationHandlingOptions? options = null)
		{
			MimeTypes responseMimeType = options?.MimeType ?? MimeTypes.ApplicationJson;
			string parsedMimeType = MimeTypesParser.ToMimeString(responseMimeType);
			return responseMimeType switch {
				MimeTypes.ApplicationJson => new ContentResult {
					Content = JsonSerializer.Serialize(data),
					StatusCode = (int)resultCode,
					ContentType = parsedMimeType
				},
				//TODO: Make this a mask for downloadable files
				MimeTypes.ImageJpeg or MimeTypes.ApplicationPdf or MimeTypes.ApplicationMsExcel => MakeFileContentResult(data as byte[], parsedMimeType, options?.FileNameOnDownload),
            _ => throw new NotImplementedException($"Mime type {parsedMimeType} not handled in the Operation handler")
        };
		}

    private static FileContentResult MakeFileContentResult(byte[]? data, string mimeType, string? fileDownloadName = null)
    {
        if (data == null)
        {
            throw new Exception("Content could not be made into a valid binary file");
        }
        var result = new FileContentResult(data, mimeType);
        if (fileDownloadName != null)
        {
            result.FileDownloadName = fileDownloadName;
        }
        return result;
    }

}
