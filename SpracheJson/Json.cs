using SpracheJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
	public static JsonObject Parse(string toParse)
	{
		return JSONParser.ParseJSON(toParse);
	}

	/// <summary>
	/// Parses a string into a JSONObject, then maps that onto an object
	/// </summary>
	/// <typeparam name="T">The type of object to map the JSON onto</typeparam>
	/// <param name="toMap">The string containing the JSON to be mapped</param>
	/// <returns>An instance of the object containing the JSON information</returns>
	public static T Map<T>(string toMap)
	{
		return Map<T>(Parse(toMap));
	}

	/// <summary>
	/// Maps a IJSONValue object onto another object
	/// </summary>
	/// <typeparam name="T">The type of object to map the IJSONValue onto</typeparam>
	/// <param name="toMap">The IJSONValue to map onto the object</param>
	/// <returns>An instance of the object containing the JSON information</returns>
	public static T Map<T>(IJsonValue toMap)
	{
		return (T)JsonMapper.MapValue(typeof(T), toMap);
	}

	/// <summary>
	/// Serialize a IJSONValue into JSON
	/// </summary>
	/// <param name="toWrite">The IJSONValue to be serialized</param>
	/// <returns>A string containing the serialized JSON</returns>
	public static string Write(IJsonValue toWrite)
	{
		return toWrite.ToJson();
	}

	/// <summary>
	/// Serialize an object into JSON
	/// </summary>
	/// <typeparam name="T">The type of object to be serialized</typeparam>
	/// <param name="toWrite">the object to be serialized</param>
	/// <returns>A string containing the serialized JSON</returns>
	public static string Write<T>(T toWrite)
	{
		return JsonSerializer.WriteValue(typeof(T), toWrite);
	}

	/// <summary>
	/// Inserts a tab character after each newline to ease formatting
	/// </summary>
	/// <param name="toTab">The string to be tabbed</param>
	/// <returns></returns>
	internal static string Tabify(string toTab)
	{
		var lines = toTab.Split('\n');
		var toReturn = "";
		foreach (var l in lines) toReturn += "\t" + l + "\n";
		return toReturn.Substring(0, toReturn.Length - 1);
	}

	/// <summary>
	/// Returns a string in JSON format (with all the appropriate escape characters placed)
	/// </summary>
	/// <param name="toGet">The string to convert to valid JSON</param>
	/// <returns>A JSON string</returns>
	internal static string GetJSONString(string toGet) => string.Concat(toGet.ToCharArray().Select(s => s switch
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
}