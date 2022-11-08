namespace SpracheJson;

/// <summary>
/// Represents a JSON object:
/// A collection of JSON pairs
/// </summary>
public class JsonObject : IJsonValue
{
	public JsonObject() =>
		Pairs = new();

	public JsonObject(IEnumerable<KeyValuePair<string, IJsonValue>>? pairs) =>
		Pairs = new(pairs ?? Enumerable.Empty<KeyValuePair<string, IJsonValue>>());

	/// <summary>
	/// All the JSON pair objects
	/// </summary>
	public Dictionary<string, IJsonValue> Pairs { get; set; }

	/// <summary>
	/// Makes Pairs directly accessable
	/// </summary>
	/// <param name="key">The key of the IJSONValue</param>
	/// <returns>The IJSONValue at that key</returns>
	public IJsonValue this[string key]
	{
		get =>
			Pairs.ContainsKey(key)
				? Pairs[key]
				: throw new ArgumentException("Key not found: " + key);
		set =>
			Pairs[key] =
				Pairs.ContainsKey(key)
					? value
					: throw new ArgumentException("Key not found: " + key);
	}

	public IJsonValue this[int i]
	{
		get =>
			i >= 0 && i < Pairs.Count
				? Pairs.ElementAt(i).Value
				: throw new IndexOutOfRangeException($"{i} is outside the bounds of the JsonObject.");
		set =>
			Pairs[Pairs.ElementAt(i).Key] =
				i >= 0 && i < Pairs.Count
					? value
					: throw new IndexOutOfRangeException($"{i} is outside the bounds of the JsonObject.");
	}

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	public string ToJson() =>
		$"{{\r\n{string.Concat(Pairs.Select(p => $"\"{p.Key}\": {p.Value.ToJson()},\r\n"))[..^3].Tabify()}\r\n}}";
}
