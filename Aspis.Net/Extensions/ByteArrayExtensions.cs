namespace AspisNet.Extensions;

public static class ByteArrayExtensions {

    public static string ToAsciiString(this byte[] arr) => System.Text.Encoding.ASCII.GetString(arr, 0, arr.Length);

}
