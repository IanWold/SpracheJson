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
        public static T MapString<T>(string toMap)
        {
            return MapValue<T>(ParseString(toMap));
        }

        public static T MapDocument<T>(string doc)
        {
            return MapValue<T>(ParseDocument(doc));
        }

        public static T MapValue<T>(JSONValue toMap)
        {
            return (T)JSONMap.MapValue(typeof(T), toMap);
        }
    }
}
