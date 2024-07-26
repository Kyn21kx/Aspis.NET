namespace AspisNet.Utils {
	internal class MathUtils {

		internal static int CalculateTotalPageCount(int totalElements, int pageSize)
		{
			return (int)Math.Ceiling(totalElements / (float)pageSize);
		}

	}
}
