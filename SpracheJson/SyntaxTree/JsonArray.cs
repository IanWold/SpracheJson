namespace SpracheJson;

/// <summary>
/// Represents a JSON array:
/// A collection of unnamed (anonymous) JSON values
/// </summary>
public class JsonArray : IJsonValue
{
	/// <summary>
	/// All the JSON values
	/// </summary>
	public List<IJsonValue> Elements { get; set; }

	public JsonArray()
	{
		Elements = new List<IJsonValue>();
	}

	public JsonArray(IEnumerable<IJsonValue> elements)
	{
		Elements = new List<IJsonValue>();
		if (elements != null) foreach (var e in elements) Elements.Add(e);
	}

	public IJsonValue this[string key]
	{
		get { throw new NotImplementedException("Cannot access JSONArray by string."); }
		set { throw new NotImplementedException("Cannot access JSONArray by string."); }
	}

	/// <summary>
	/// Makes Elements directly accessable
	/// </summary>
	/// <param name="i">The index of the IJSONValue</param>
	/// <returns>The IJSONValue at index i</returns>
	public IJsonValue this[int i]
	{
		get
		{
			if (0 <= i && i < Elements.Count) return Elements[i];
			else throw new IndexOutOfRangeException(i.ToString() + " is out of range.");
		}
		set
		{
			if (0 <= i && i < Elements.Count) Elements[i] = value;
			else throw new IndexOutOfRangeException(i.ToString() + " is out of range.");
		}
	}

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	public string ToJson()
	{
		var toReturn = "";
		foreach (var e in Elements) toReturn += $"{e.ToJson()},\r\n";
		toReturn = JSON.Tabify(toReturn[..^3]);
		return $"[\r\n{toReturn}\r\n]";
	}
}
