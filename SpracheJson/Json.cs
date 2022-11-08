namespace SpracheJson;

/// <summary>
/// Interfaces kindly with the parser and mapper
/// </summary>
public static class JSON
{
	/// <summary>
	/// Parse a string into a JSONObject
	/// </summary>
	/// <param name="toParse">The string (document) to be parsed</param>
	/// <returns>A JSONObject representing the JSON document</returns>
	public static JsonObject Parse(string toParse) =>
		JSONParser.ParseJSON(toParse);

	/// <summary>
	/// Parses a string into a JSONObject, then maps that onto an object
	/// </summary>
	/// <typeparam name="T">The type of object to map the JSON onto</typeparam>
	/// <param name="toMap">The string containing the JSON to be mapped</param>
	/// <returns>An instance of the object containing the JSON information</returns>
	public static T Deserialize<T>(string toMap) =>
		Map<T>(Parse(toMap));

	/// <summary>
	/// Maps a IJSONValue object onto another object
	/// </summary>
	/// <typeparam name="T">The type of object to map the IJSONValue onto</typeparam>
	/// <param name="toMap">The IJSONValue to map onto the object</param>
	/// <returns>An instance of the object containing the JSON information</returns>
	public static T Map<T>(IJsonValue toMap) =>
		JsonMapper.MapValue(typeof(T), toMap) is T mappedT
			? mappedT
			: throw new ArgumentException($"Cannot map value to type {typeof(T)}.", nameof(toMap));

	/// <summary>
	/// Serialize an object into JSON
	/// </summary>
	/// <typeparam name="T">The type of object to be serialized</typeparam>
	/// <param name="toWrite">the object to be serialized</param>
	/// <returns>A string containing the serialized JSON</returns>
	public static string Serialize<T>(T toWrite) =>
		toWrite is IJsonValue jsonValueToWrite
			? jsonValueToWrite.ToJson()
			: JsonSerializer.SerializeValue(typeof(T), toWrite);
}