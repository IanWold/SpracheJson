namespace SpracheJson;

/// <summary>
/// Represents a literal value in JSON:
/// A string, number, boolean, or null value
/// </summary>
public class JsonLiteral : IJsonValue
{
	public JsonLiteral(string? value, JsonLiteralType type)
	{
		Value = value;
		ValueType = type;
	}

	/// <summary>
	/// A string representation of the value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// The type the value is
	/// </summary>
	public JsonLiteralType ValueType { get; set; }

	/// <summary>
	/// Returns Value cast as the appropriate type.
	/// </summary>
	/// <returns></returns>
	public object? Get() => ValueType switch
	{
		JsonLiteralType.String => Value,
		JsonLiteralType.Number => Convert.ToDouble(Value),
		JsonLiteralType.Boolean => Value?.ToLower() == "true",
		_ => null,
	};

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	public string ToJson() => ValueType switch
	{
		JsonLiteralType.String => $"\"{Value?.ToJsonString()}\"",
		JsonLiteralType.Null => "null",
		_ => Value ?? string.Empty,
	};

	/// <summary>
	/// Returns the Value property
	/// </summary>
	/// <returns>this.Value</returns>
	public override string ToString() =>
		Value ?? string.Empty;
}
