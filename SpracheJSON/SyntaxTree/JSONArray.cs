using System.Collections.Generic;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a JSON array:
    /// A collection of unnamed (anonymous) JSON values
    /// </summary>
    public class JSONArray : JSONValue
    {
        /// <summary>
        /// All the JSON values
        /// </summary>
        public IEnumerable<JSONValue> Elements { get; set; }

        public JSONArray(IEnumerable<JSONValue> elements)
        {
            Elements = elements;
        }

        /// <summary>
        /// Returns a string representing the object in JSON
        /// </summary>
        /// <returns></returns>
        public override string ToJSON()
        {
            var toReturn = "";
            foreach (var e in Elements) toReturn +=  e.ToJSON() + ",\r\n";
            toReturn = Tabify(toReturn.Substring(0, toReturn.Length - 3));
            return "[\r\n" + toReturn + "\r\n]";
        }

        /// <summary>
        /// Inserts a tab character after each newline to ease formatting
        /// </summary>
        /// <param name="toTab">The string to be tabbed</param>
        /// <returns></returns>
        string Tabify(string toTab)
        {
            var lines = toTab.Split('\n');
            var toReturn = "";
            foreach (var l in lines) toReturn += "\t" + l + "\n";
            return toReturn.Substring(0, toReturn.Length - 1);
        }
    }
}
