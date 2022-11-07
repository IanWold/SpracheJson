using System.Collections;
using System.Text;

namespace SpracheJson;

/// <summary>
/// Serializes objects as JSON text
/// </summary>
static class JsonSerializer
{
	/// <summary>
	/// Serializes an object as a JSON object
	/// </summary>
	/// <param name="T">The type of object to be serialized</param>
	/// <param name="toWrite">The object to be serialized</param>
	/// <returns>A string containing the serialized JSON text</returns>
	static string SerializeObject(Type T, object toWrite)
	{
		var builder = new StringBuilder();

		if (T.IsSubclassOf(typeof(IDictionary)))
		{
			//This is weird
		}
		else if (T.IsClass)
		{
			if (toWrite == null)
			{
				throw new ArgumentException(null, nameof(toWrite));
			}

			//Loop through all the properties of the type
#pragma warning disable CS8604 // Possible null reference argument; toWrite is already checked to be != null
			foreach (var property in T.GetProperties())
			{
				//Write the property and the serialization of its value in the appropriate format
				builder.Append($"\"{property.Name}\": {SerializeValue(property.PropertyType, property.GetValue(toWrite))},\r\n");
			}

			//Loop through all the fields of the type
			foreach (var field in T.GetFields())
			{
				//Write the field and the serialization of its value in the appropriate format
				builder.Append($"\"{field.Name}\": {SerializeValue(field.FieldType, field.GetValue(toWrite!))},\r\n");
			}
#pragma warning restore CS8604 // Possible null reference argument.
		}

		//Return a properly formatted JSON object
		return $"{{\r\n{builder.ToString()[0..^3].Tabify()}\r\n}}";
	}

	/// <summary>
	/// Serializes an object as a JSON array
	/// </summary>
	/// <param name="T">The type of object to be serialized</param>
	/// <param name="toWrite">The object to be serialized</param>
	/// <returns>A string containing the serialized JSON text</returns>
	static string SerializeArray(Type T, object toWrite)
	{
		var builder = new StringBuilder();

		//Loop through all the elements in the collection
		foreach (var element in (IList)toWrite)
		{
			//Write the serialized element
			builder.Append($"{SerializeValue(element.GetType(), element)},\r\n");
		}

		//Return a properly formatted JSON array
		return $"[\r\n{builder.ToString()[0..^3].Tabify()}\r\n]";
	}

	/// <summary>
	/// Serializes an object as a literal JSON value
	/// </summary>
	/// <param name="T">The type of object to be serialized</param>
	/// <param name="toWrite">The object to be serialized</param>
	/// <returns>A string containing the serialized JSON text</returns>
	static string SerializeLiteral(Type T, object toWrite)
	{
		//toWrite is a number
		if (T.IsEquivalentTo(typeof(double)))
		{
			//Convert it to a number and return it cast as a string
			return Convert.ToDouble(toWrite).ToString();
		}
		//toWrite is a bool
		else if (T.IsEquivalentTo(typeof(bool)))
		{
			//Cast it as a bool and return a string representation of the value
			return (toWrite is bool toWriteBool && toWriteBool)
				? "true"
				: "false";
		}
		//toWrite is a string
		else if (T.IsEquivalentTo(typeof(string)))
		{
			//Return the string properly formatted as a literal JSON string
			return $"\"{toWrite.ToString()!.ToJsonString()}\"";
		}
		
		throw new ArgumentException($"{T} can't be serialized as a literal JSON value.");
	}

	/// <summary>
	/// Serializes an object as the appropriate JSON type
	/// </summary>
	/// <param name="T">The type of object to be serialized</param>
	/// <param name="toWrite">The object to be serialized</param>
	/// <returns>A string containing the serialized JSON text</returns>
	public static string SerializeValue(Type T, object toWrite)
	{
		//If it's null, go ahead and return that
		if (toWrite == null)
		{
			return "null";
		}
		//Otherwise, find the right type
		else if (T.GetInterface("System.Collections.IList") != null || T.IsArray)
		{
			return SerializeArray(T, toWrite);
		}
		else if (T.IsPrimitive || T.IsEquivalentTo(typeof(string)) || T.IsEquivalentTo(typeof(bool)))
		{
			return SerializeLiteral(T, toWrite);
		}
		
		return SerializeObject(T, toWrite);
	}
}
