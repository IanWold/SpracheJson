using Sprache;

namespace SpracheJson;

internal static class Extensions
{
	/// <summary>
	/// Returns a string in JSON format (with all the appropriate escape characters placed)
	/// </summary>
	/// <param name="toGet">The string to convert to valid JSON</param>
	/// <returns>A JSON string</returns>
	internal static string ToJsonString(this string toGet) =>
		string.Concat(toGet.ToCharArray().Select(s => s switch
		{
			'/' => "\\/",
			'\\' => "\\\\",
			'\b' => "\\b",
			'\f' => "\\f",
			'\n' => "\\n",
			'\r' => "\\r",
			'\t' => "\\t",
			'"' => "\\\"",
			_ => s.ToString(),
		}));

	/// <summary>
	/// Inserts a tab character after each newline to ease formatting
	/// </summary>
	/// <param name="toTab">The string to be tabbed</param>
	/// <returns></returns>
	internal static string Tabify(this string toTab) =>
		string.Concat(toTab.Split('\n').Select(l => $"\t{l}\n"))[..^1];
}
