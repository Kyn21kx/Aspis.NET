namespace AspisNet.Utils.ApiOperations; 
public class ApiOperationException : Exception {

    public ApiOperationStatus Result { get; private set; }

    public ApiOperationException(string message, ApiOperationStatus result) : base(message)
    {
        Result = result;
    }
}
