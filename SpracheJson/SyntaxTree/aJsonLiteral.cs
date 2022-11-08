namespace SpracheJson;

/// <summary>
/// Represents a literal value in JSON:
/// A string, number, boolean, or null value
/// </summary>
public class JsonLiteral : IJsonValue
{
	/// <summary>
	/// A string representation of the value
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// The type the value is
	/// </summary>
	public JsonLiteralType ValueType { get; set; }

	public JsonLiteral(JsonLiteralType type)
	{
		ValueType = type;
	}

	public JsonLiteral(string value, JsonLiteralType type)
	{
		Value = value;
		ValueType = type;
	}

	/// <summary>
	/// Returns Value cast as the appropriate type.
	/// </summary>
	/// <returns></returns>
	public object Get()
	{
		switch (ValueType)
		{
			case JsonLiteralType.String:
				return Value;

			case JsonLiteralType.Number:
				return Convert.ToDouble(Value);

			case JsonLiteralType.Boolean:
				return (Value.ToLower() == "true") ? true : false;

			default:
				return null;
		}
	}

	public IJsonValue this[string key]
	{
		get { throw new NotImplementedException("Cannot access JSONArray by string."); }
		set { throw new NotImplementedException("Cannot access JSONArray by string."); }
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
	public string ToJSON()
	{
		var toReturn = "";

		switch (ValueType)
		{
			case JsonLiteralType.String:
				toReturn = "\"" + JSON.GetJSONString(Value) + "\"";
				break;

			case JsonLiteralType.Null:
				toReturn = "null";
				break;

			default:
				toReturn = Value;
				break;
		}

		return toReturn;
	}

	/// <summary>
	/// Returns the Value property
	/// </summary>
	/// <returns>this.Value</returns>
	public override string ToString()
	{
		return Value;
	}
}
