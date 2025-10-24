using System.Globalization;
using Domain.Constants;

namespace Common.Strings;

public static class StringExtensions
{
	public static bool DateFormatTryParse(this string dateString, out DateTime parsedDateTime)
	{
		return DateTime.TryParseExact(
			dateString,
			DateTimeConstants.DateFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out parsedDateTime);
	}

	public static bool DateTimeFormatTryParse(this string dateString, out DateTime parsedDateTime)
	{
		return DateTime.TryParseExact(
			dateString,
			DateTimeConstants.DateTimeFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out parsedDateTime);
	}
}