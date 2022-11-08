namespace SpracheJson;

/// <summary>
/// Represents a JSON array:
/// A collection of unnamed (anonymous) JSON values
/// </summary>
public class JsonArray : IJsonValue
{
	public JsonArray() =>
		Elements = new();

	public JsonArray(IEnumerable<IJsonValue>? elements) =>
		Elements = new(elements ?? Enumerable.Empty<IJsonValue>());

	/// <summary>
	/// All the JSON values
	/// </summary>
	public List<IJsonValue> Elements { get; set; }

	/// <summary>
	/// Makes Elements directly accessable
	/// </summary>
	/// <param name="i">The index of the IJSONValue</param>
	/// <returns>The IJSONValue at index i</returns>
	public IJsonValue this[int i]
	{
		get =>
			i >= 0 && i < Elements.Count
				? Elements[i]
				: throw new IndexOutOfRangeException(i.ToString() + " is out of range.");
		set =>
			Elements[i] =
				i >= 0 && i < Elements.Count
					? value
					: throw new IndexOutOfRangeException(i.ToString() + " is out of range.");
	}

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	public string ToJson() =>
		$"[\r\n{string.Concat(Elements.Select(e => $"{e.ToJson()},\r\n"))[..^3].Tabify()}\r\n]";
}
