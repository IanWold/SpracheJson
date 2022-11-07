using System.Collections;

namespace SpracheJson;

/// <summary>
/// Maps IJSONValue objects onto other objects
/// </summary>
static class JsonMapper
{
	/// <summary>
	/// Maps a JSONObject onto an object which can be cast as an appropriate type
	/// </summary>
	/// <param name="T">The type to which the returned object will be cast</param>
	/// <param name="toMap">The JSONObject to map onto the object</param>
	/// <returns>An object of type T containing the JSON information</returns>
	static object? MapObject(Type T, JsonObject toMap)
	{
		if (T.IsSubclassOf(typeof(IDictionary)))
		{
			//Create an instance of the object as an IDictionary
			var toReturn = (IDictionary)Activator.CreateInstance(T)!;

			//Treat the object like a dictionary, and add each element to it as a key, value pair.
			foreach (var p in toMap.Pairs)
			{
				toReturn.Add(p.Key, MapValue(T.GenericTypeArguments[1], p.Value));
			}

			return toReturn;
		}
		else if (T.IsClass)
		{
			//Create an instance of the object
			var toReturn = Activator.CreateInstance(T)!;

			//Loop through all the properties of the type
			foreach (var property in T.GetProperties().Where(p => toMap.Pairs.ContainsKey(p.Name)))
			{
				//Set the value of the return object's property to that information
				property.SetValue(toReturn, MapValue(property.PropertyType, toMap[property.Name]));
			}

			//Loop through all the fields of the type
			foreach (var field in T.GetFields().Where(f => toMap.Pairs.ContainsKey(f.Name)))
			{
				//Set the value of the return object's field to that information.
				field.SetValue(toReturn, MapValue(field.FieldType, toMap[field.Name]));
			}

			return toReturn;
		}

		return null;
	}

	/// <summary>
	/// Maps a JSONArray onto an object which can be cast as an appropriate type
	/// </summary>
	/// <param name="T">The type to which the returned object will be cast</param>
	/// <param name="toMap">The JSONArray to map onto the object</param>
	/// <returns>An object of type T containing the JSON information</returns>
	static object MapArray(Type T, JsonArray toMap)
	{
		//If the type isn't an array or IList then the JSONArray can't be mapped onto it
		if (!T.IsArray || T.GetInterface("System.Collections.IList") == null)
		{
			throw new ArgumentException($"{T} can't map JsonArray.");
		}

		//If T is an array, create a new ArrayList, otherwise create a new IList of type T
		var toReturnList =
			T.IsArray
			? new ArrayList()
			: (IList)Activator.CreateInstance(T)!;

		//Loop through all the elements of the array, and populate toReturnList with the mapped values of those elements
		foreach (var e in toMap.Elements)
		{
			toReturnList.Add(MapValue(T.GetElementType()!, e));
		}

		//If T is an array, we need to cast the list as an array
		if (T.IsArray)
		{
			//Create an instance of an array of the appropriate type
			var c = toReturnList.Count;
			var toReturn = Array.CreateInstance(T.GetElementType()!, c);

			//Loop through toReturnList and add each element to the array
			for (int i = 0; i < c; i++)
			{
				toReturn.SetValue(toReturnList[i], i);
			}

			return toReturn;
		}

		return toReturnList;
	}

	/// <summary>
	/// Maps a JSONLiteral onto an object which can be cast as an appropriate type
	/// </summary>
	/// <param name="T">The type to which the returned object will be cast</param>
	/// <param name="toMap">The JSONLiteral to map onto the object</param>
	/// <returns>An object of type T containing the JSON information</returns>
	static object? MapLiteral(Type T, JsonLiteral toMap)
	{
		//If toMap isn't a literal, toMap.Get() return an object cast as the appropriate type.
		if (toMap.ValueType != JsonLiteralType.Null)
		{
			return toMap.Get();
		}

		//If the literal is a null, return null IF T is nullable, otherwise throw an exception
		return T.IsClass || Nullable.GetUnderlyingType(T) != null
			? null
			: throw new ArgumentException($"{T} can't be null.");
	}

	/// <summary>
	/// Maps a IJSONValue onto an object which can be cast as an appropriate type
	/// </summary>
	/// <param name="T">The type to which the returned object will be cast</param>
	/// <param name="toMap">The IJSONValue to map onto the object</param>
	/// <returns>An object of type T containing the JSON information</returns>
	public static object? MapValue(Type T, IJsonValue toMap) => toMap switch
	{
		JsonObject jObject => MapObject(T, jObject),
		JsonArray jArray => MapArray(T, jArray),
		JsonLiteral jLiteral => MapLiteral(T, jLiteral),
		_ => throw new ArgumentException("Cannot map vanilla IJSONValue.")
	};
}
