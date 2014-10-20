using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
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
            return (JSONObject)JSONParser.JObject.Parse(toParse);
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
        /// Write a JSONValue into JSON
        /// </summary>
        /// <param name="toWrite">The JSONValue to be written</param>
        /// <returns>A string containing the JSON text</returns>
        public static string WriteString(JSONValue toWrite)
        {
            var toReturn = "";

            if (toWrite is JSONObject)
            {
                toReturn += "{\r\n";
                foreach (var p in (toWrite as JSONObject).Pairs) toReturn += "\"" + p.Key + "\"" + ": " + WriteString(p.Value) + ",\r\n";
                toReturn = toReturn.Substring(0, toReturn.Length - 3);
                toReturn += "\r\n}";
            }
            else if (toWrite is JSONArray)
            {
                toReturn += "[\r\n";
                foreach (var e in (toWrite as JSONArray).Elements) toReturn += WriteString(e) + ",\r\n";
                toReturn = toReturn.Substring(0, toReturn.Length - 3);
                toReturn += "\r\n]";
            }
            else if (toWrite is JSONLiteral)
            {
                switch ((toWrite as JSONLiteral).Type)
                {
                    case LiteralType.String:
                        foreach (var s in (toWrite as JSONLiteral).Value.ToCharArray())
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

                        toReturn = "\"" + toReturn + "\"";
                        break;

                    case LiteralType.Null:
                        toReturn = "null";
                        break;

                    default:
                        toReturn = (toWrite as JSONLiteral).Value;
                        break;
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Write a JSONValue into JSON, then write than onto a file
        /// </summary>
        /// <param name="toWrite">The JSONValue to be written</param>
        /// <param name="doc">The location of the file to be written to</param>
        public static void WriteDocument(JSONValue toWrite, string doc)
        {
            using (var writer = new StreamWriter(doc))
                writer.Write(WriteString(toWrite));
        }
    }
}
