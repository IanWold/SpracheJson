using System.IO;

namespace SpracheJSON
{
    public static class JSON
    {
        /// <summary>
        /// Parse a string into a JSONObject
        /// </summary>
        /// <param name="toParse">The string (document) to be parsed</param>
        /// <returns>A JSONObject representing the JSON document</returns>
        public static JSONObject ParseString(string toParse)
        {
            return JSONParser.ParseJSON(toParse);
        }

        /// <summary>
        /// Read a file, then parse its contents into a JSONObject
        /// </summary>
        /// <param name="doc">The location of the file to parse</param>
        /// <returns>A JSON object representing the JSON document</returns>
        public static JSONObject ParseDocument(string doc)
        {
            using (var reader = new StreamReader(doc))
                return ParseString(reader.ReadToEnd());
        }

        /// <summary>
        /// Write a JSONValue into JSON, then write than onto a file
        /// </summary>
        /// <param name="toWrite">The JSONValue to be written</param>
        /// <param name="doc">The location of the file to be written to</param>
        public static void WriteDocument(JSONValue toWrite, string doc)
        {
            using (var writer = new StreamWriter(doc))
                writer.Write(toWrite.ToJSON());
        }

        /// <summary>
        /// Parses a string into a JSONObject, then maps that onto an object
        /// </summary>
        /// <typeparam name="T">The type of object to map the JSON onto</typeparam>
        /// <param name="toMap">The string containing the JSON to be mapped</param>
        /// <returns>An instance of the object containing the JSON information</returns>
        public static T MapString<T>(string toMap)
        {
            return MapValue<T>(ParseString(toMap));
        }

        /// <summary>
        /// Parses a document into a JSONObject, then maps that onto an object
        /// </summary>
        /// <typeparam name="T">The type of object to map the JSON onto</typeparam>
        /// <param name="doc">The location of the document containnig the JSON to be mapped</param>
        /// <returns>An instance of the object containing the JSON information</returns>
        public static T MapDocument<T>(string doc)
        {
            return MapValue<T>(ParseDocument(doc));
        }

        /// <summary>
        /// Maps a JSONValue object onto another object
        /// </summary>
        /// <typeparam name="T">The type of object to map the JSONValue onto</typeparam>
        /// <param name="toMap">The JSONValue to map onto the object</param>
        /// <returns>An instance of the object containing the JSON information</returns>
        public static T MapValue<T>(JSONValue toMap)
        {
            return (T)JSONMap.MapValue(typeof(T), toMap);
        }
    }
}
