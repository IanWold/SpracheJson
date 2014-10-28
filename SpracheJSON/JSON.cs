namespace SpracheJSON
{
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
        public static JSONObject Parse(string toParse)
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
        /// Maps a JSONValue object onto another object
        /// </summary>
        /// <typeparam name="T">The type of object to map the JSONValue onto</typeparam>
        /// <param name="toMap">The JSONValue to map onto the object</param>
        /// <returns>An instance of the object containing the JSON information</returns>
        public static T Map<T>(JSONValue toMap)
        {
            return (T)JSONMap.MapValue(typeof(T), toMap);
        }

        /// <summary>
        /// Serialize a JSONValue into JSON
        /// </summary>
        /// <param name="toWrite">The JSONValue to be serialized</param>
        /// <returns>A string containing the serialized JSON</returns>
        public static string Write(JSONValue toWrite)
        {
            return toWrite.ToJSON();
        }

        /// <summary>
        /// Serialize an object into JSON
        /// </summary>
        /// <typeparam name="T">The type of object to be serialized</typeparam>
        /// <param name="toWrite">the object to be serialized</param>
        /// <returns>A string containing the serialized JSON</returns>
        public static string Write<T>(T toWrite)
        {
            return JSONSerialize.WriteValue(typeof(T), toWrite);
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
        /// <param name="toConvert">The string to convert to valid JSON</param>
        /// <returns>A JSON string</returns>
        internal static string GetJSONString(string toConvert)
        {
            var toReturn = "";

            foreach (var s in toConvert.ToCharArray())
            {
                switch (s)
                {
                    case '/':
                        toReturn += "\\/";
                        break;

                    case '\\':
                        toReturn += "\\\\";
                        break;

                    case '\b':
                        toReturn += "\\b";
                        break;

                    case '\f':
                        toReturn += "\\f";
                        break;

                    case '\n':
                        toReturn += "\\n";
                        break;

                    case '\r':
                        toReturn += "\\r";
                        break;

                    case '\t':
                        toReturn += "\\t";
                        break;

                    case '"':
                        toReturn += "\\\"";
                        break;

                    default:
                        toReturn += s;
                        break;
                }
            }

            return toReturn;
        }
    }
}
