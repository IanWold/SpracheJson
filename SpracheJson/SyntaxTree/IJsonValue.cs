namespace SpracheJson;

/// <summary>
/// Represents a JSON value
/// </summary>
public interface IJsonValue
{
	/// <summary>
	/// Allows key indexing without downcasting to JSONObject
	/// </summary>
	/// <param name="key">The key to index by</param>
	/// <returns>Throws an exception unless overridden by JSONObject</returns>
	IJsonValue this[string key]
	{
		get => throw new NotImplementedException($"JsonLiteral and JsonArray cannot be indexed by key. Value: {ToJson()}");
		set => throw new NotImplementedException($"JsonLiteral and JsonArray cannot be indexed by key. Value: {ToJson()}");
	}

	/// <summary>
	/// Allows indexing without downcasting to JSONArray
	/// </summary>
	/// <param name="i">The integer to index by</param>
	/// <returns>Throws an exception unless overridden by JSONArray</returns>
	IJsonValue this[int i]
	{
		get => throw new NotImplementedException($"JsonLiteral cannot be indexed by position. Value: {ToJson()}");
		set => throw new NotImplementedException($"JsonLiteral cannot be indexed by position. Value: {ToJson()}");
	}

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	string ToJson();
}
