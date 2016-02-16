using System;
using System.Collections;
using System.Collections.Generic;

namespace SpracheJSON
{
    /// <summary>
    /// Serializes objects as JSON text
    /// </summary>
    static class JSONSerialize
    {
        /// <summary>
        /// Serializes an object as a JSON object
        /// </summary>
        /// <param name="T">The type of object to be serialized</param>
        /// <param name="toWrite">The object to be serialized</param>
        /// <returns>A string containing the serialized JSON text</returns>
        static string WriteObject(Type T, object toWrite)
        {
            var toReturn = "";

			if (T.IsSubclassOf(typeof(IDictionary)))
			{
				//This is weird
			}
            else if (T.IsClass)
			{
				//Loop through all the properties of the type
				foreach (var p in T.GetProperties())
				{
					//Write the property and the serialization of its value in the appropriate format
					toReturn += "\"" + p.Name + "\": " + WriteValue(p.PropertyType, p.GetValue(toWrite)) + ",\r\n";
				}

				//Loop through all the fields of the type
				foreach (var f in T.GetFields())
				{
					//Write the field and the serialization of its value in the appropriate format
					toReturn += "\"" + f.Name + "\": " + WriteValue(f.FieldType, f.GetValue(toWrite)) + ",\r\n";
				}
			}

            //Return a properly formatted JSON object
            return "{\r\n" + JSON.Tabify(toReturn.Substring(0, toReturn.Length - 3)) + "\r\n}";
        }

        /// <summary>
        /// Serializes an object as a JSON array
        /// </summary>
        /// <param name="T">The type of object to be serialized</param>
        /// <param name="toWrite">The object to be serialized</param>
        /// <returns>A string containing the serialized JSON text</returns>
        static string WriteArray(Type T, object toWrite)
        {
            string toReturn = "";

            //Loop through all the elements in the collection
            foreach (var element in (IList)toWrite)
            {
				//Write the serialized element
				var e = T.GetElementType();
				var et = element.GetType();
                toReturn += WriteValue(et, element) + ",\r\n";
            }

            //Return a properly formatted JSON array
            return "[\r\n" + JSON.Tabify(toReturn.Substring(0, toReturn.Length - 3)) + "\r\n]";
        }

        /// <summary>
        /// Serializes an object as a literal JSON value
        /// </summary>
        /// <param name="T">The type of object to be serialized</param>
        /// <param name="toWrite">The object to be serialized</param>
        /// <returns>A string containing the serialized JSON text</returns>
        static string WriteLiteral(Type T, object toWrite)
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
                //Cast it as a bool
                var b = toWrite as bool?;
                //return a string representation of the value
                if (b == true) return "true";
                else return "false";
            }
            //toWrite is a string
            else if (T.IsEquivalentTo(typeof(string)))
            {
                //Return the string properly formatted as a literal JSON string
                return "\"" + JSON.GetJSONString(toWrite.ToString()) + "\"";
            }
            else throw new ArgumentException(T + " can't be serialized as a literal JSON value.");
        }

        /// <summary>
        /// Serializes an object as the appropriate JSON type
        /// </summary>
        /// <param name="T">The type of object to be serialized</param>
        /// <param name="toWrite">The object to be serialized</param>
        /// <returns>A string containing the serialized JSON text</returns>
        public static string WriteValue(Type T, object toWrite)
        {
            //If it's null, go ahead and return that
            if (toWrite == null) return "null";
            //Otherwise, find the right type
            else if (T.GetInterface("System.Collections.IList") != null || T.IsArray) return WriteArray(T, toWrite);
            else if (T.IsPrimitive || T.IsEquivalentTo(typeof(string)) || T.IsEquivalentTo(typeof(bool))) return WriteLiteral(T, toWrite);
            else return WriteObject(T, toWrite);
        }
    }
}
