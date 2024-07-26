using System.Globalization;
using System.Text;

namespace AspisNet.Extensions; 
public static class StringExtensions {

    public static byte[] ToByteArrayFromAscii(this string str) => Encoding.ASCII.GetBytes(str);

	public static string RemoveDiacritics(this string text)
	{
		ReadOnlySpan<char> normalizedString = text.Normalize(NormalizationForm.FormD);
		int i = 0;
		Span<char> span = text.Length < 1000
			? stackalloc char[text.Length]
			: new char[text.Length];

		foreach (char c in normalizedString)
		{
			if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				span[i++] = c;
		}

		return new string(span).Normalize(NormalizationForm.FormC);
	}
}
