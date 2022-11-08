namespace SpracheJson;

/// <summary>
/// Represents a JSON object:
/// A collection of JSON pairs
/// </summary>
public class JsonObject : IJsonValue
{
	/// <summary>
	/// All the JSON pair objects
	/// </summary>
	public Dictionary<string, IJsonValue> Pairs { get; set; }

	public JsonObject()
	{
		Pairs = new Dictionary<string, IJsonValue>();
	}

	public JsonObject(IEnumerable<KeyValuePair<string, IJsonValue>> pairs)
	{
		Pairs = new Dictionary<string, IJsonValue>();
		if (pairs != null) foreach (var p in pairs) Pairs.Add(p.Key, p.Value);
	}

	/// <summary>
	/// Makes Pairs directly accessable
	/// </summary>
	/// <param name="key">The key of the IJSONValue</param>
	/// <returns>The IJSONValue at that key</returns>
	public IJsonValue this[string key]
	{
		get
		{
			if (Pairs.ContainsKey(key)) return Pairs[key];
			else throw new ArgumentException("Key not found: " + key);
		}
		set
		{
			if (Pairs.ContainsKey(key)) Pairs[key] = value;
			else throw new ArgumentException("Key not found: " + key);
		}
	}

	public IJsonValue this[int i]
	{
		get { throw new NotImplementedException("Cannot access JSONArray by string."); }
		set { throw new NotImplementedException("Cannot access JSONArray by string."); }
	}

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	public string ToJson()
	{
		var toReturn = "";
		foreach (var p in Pairs) toReturn += $"\"{p.Key}\": {p.Value.ToJson()},\r\n";
		toReturn = JSON.Tabify(toReturn[..^3]);
		return $"{{\r\n{toReturn}\r\n}}";
	}
}
