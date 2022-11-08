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
	IJsonValue this[string key] { get; set; }

	/// <summary>
	/// Allows indexing without downcasting to JSONArray
	/// </summary>
	/// <param name="i">The integer to index by</param>
	/// <returns>Throws an exception unless overridden by JSONArray</returns>
	IJsonValue this[int i] { get; set; }

	/// <summary>
	/// Returns a string representing the object in JSON
	/// </summary>
	/// <returns></returns>
	string ToJson();
}
