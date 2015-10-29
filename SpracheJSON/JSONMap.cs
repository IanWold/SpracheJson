using System;
using System.Collections;

namespace SpracheJSON
{
    /// <summary>
    /// Maps JSONValue objects onto other objects
    /// </summary>
    static class JSONMap
    {
        /// <summary>
        /// Maps a JSONObject onto an object which can be cast as an appropriate type
        /// </summary>
        /// <param name="T">The type to which the returned object will be cast</param>
        /// <param name="toMap">The JSONObject to map onto the object</param>
        /// <returns>An object of type T containing the JSON information</returns>
        static object MapObject(Type T, JSONObject toMap)
        {
            //Create an instance of the object
            var toReturn = Activator.CreateInstance(T);

            if (T.IsSubclassOf(typeof(IDictionary)))
            {
                //How do I get this into a dictionary?
            }
            else if (T.IsClass)
            {
                //Loop through all the properties of the type
                foreach (var p in T.GetProperties())
                {
                    //If the JSONObject contains information for that property,
                    //set the value of the return object's property to that information.
                    if (toMap.Pairs.ContainsKey(p.Name))
                    {
                        p.SetValue(toReturn, MapValue(p.PropertyType, toMap[p.Name]));
                    }
                }

                //Loop through all the fields of the type
                foreach (var f in T.GetFields())
                {
                    //If the JSONObject contains information for that field,
                    //set the value of the return object's field to that information.
                    if (toMap.Pairs.ContainsKey(f.Name))
                    {
                        f.SetValue(toReturn, MapValue(f.FieldType, toMap[f.Name]));
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Maps a JSONArray onto an object which can be cast as an appropriate type
        /// </summary>
        /// <param name="T">The type to which the returned object will be cast</param>
        /// <param name="toMap">The JSONArray to map onto the object</param>
        /// <returns>An object of type T containing the JSON information</returns>
        static object MapArray(Type T, JSONArray toMap)
        {
            //If the type isn't an array or IList then the JSONArray can't be mapped onto it
            if (!T.IsArray || T.GetInterface("System.Collections.IList") == null)
            {
                throw new ArgumentException(T + " can't map JSONArray.");
            }
            else
            {
                //If T is an array, create a new ArrayList, otherwise create a new IList of type T
                var toReturnList = (T.IsArray) ? new ArrayList() : (IList)Activator.CreateInstance(T);

                //Loop through all the elements of the array, and populate toReturnList with the mapped values of those elements
                foreach (var e in toMap.Elements)
                {
                    toReturnList.Add(MapValue(T.GetElementType(), e));
                }

                //If T is an array, we need to cast the list as an array, otherwise we can return the list
                if (T.IsArray)
                {
                    //Create an instance of an array of the appropriate type
                    var c = toReturnList.Count;
                    var toReturn = Array.CreateInstance(T.GetElementType(), c);

                    //Loop through toReturnList and add each element to the array
                    for (int i = 0; i < c; i++)
                    {
                        toReturn.SetValue(toReturnList[i], i);
                    }

                    return toReturn;
                }
                else return toReturnList;
            }
        }

        /// <summary>
        /// Maps a JSONLiteral onto an object which can be cast as an appropriate type
        /// </summary>
        /// <param name="T">The type to which the returned object will be cast</param>
        /// <param name="toMap">The JSONLiteral to map onto the object</param>
        /// <returns>An object of type T containing the JSON information</returns>
        static object MapLiteral(Type T, JSONLiteral toMap)
        {
            //If the literal is a null, return null IF T is nullable, otherwise throw an exception
            if (toMap.ValueType == LiteralType.Null)
            {
                if (T.IsClass || Nullable.GetUnderlyingType(T) != null) return null;
                else throw new ArgumentException(T + " can't be null.");
            }
            //If toMap isn't a literal, toMap.Get() return an object cast as the appropriate type.
            else return toMap.Get();
        }

        /// <summary>
        /// Maps a JSONValue onto an object which can be cast as an appropriate type
        /// </summary>
        /// <param name="T">The type to which the returned object will be cast</param>
        /// <param name="toMap">The JSONValue to map onto the object</param>
        /// <returns>An object of type T containing the JSON information</returns>
        public static object MapValue(Type T, JSONValue toMap)
        {
            if (toMap is JSONObject) return MapObject(T, (JSONObject)toMap);
            else if (toMap is JSONArray) return MapArray(T, (JSONArray)toMap);
            else if (toMap is JSONLiteral) return MapLiteral(T, (JSONLiteral)toMap);
            else throw new ArgumentException("Cannot map vanilla JSONValue.");
        }
    }
}
