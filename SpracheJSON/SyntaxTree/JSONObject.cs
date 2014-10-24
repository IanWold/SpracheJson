using System.Collections.Generic;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a JSON object:
    /// A collection of JSON pairs
    /// </summary>
    public class JSONObject : JSONValue
    {
        /// <summary>
        /// All the JSON pair objects
        /// </summary>
        public Dictionary<string, JSONValue> Pairs { get; set; }

        public JSONObject(IEnumerable<KeyValuePair<string, JSONValue>> pairs)
        {
            Pairs = new Dictionary<string, JSONValue>();
            foreach (var p in pairs) Pairs.Add(p.Key, p.Value);
        }

        /// <summary>
        /// Makes Pairs directly accessable
        /// </summary>
        /// <param name="key">The key of the JSONValue</param>
        /// <returns>The JSONValue at that key</returns>
        public override JSONValue this[string key]
        {
            get { return Pairs[key]; }
            set { Pairs[key] = value; }
        }

        /// <summary>
        /// Returns a string representing the object in JSON
        /// </summary>
        /// <returns></returns>
        public override string ToJSON()
        {
            var toReturn = "";
            foreach (var p in Pairs) toReturn += "\"" + p.Key + "\"" + ": " + p.Value.ToJSON() + ",\r\n";
            toReturn = Tabify(toReturn.Substring(0, toReturn.Length - 3));
            return "{\r\n" + toReturn + "\r\n}";
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
