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
        public IEnumerable<JSONPair> Pairs { get; set; }

        public JSONObject(IEnumerable<JSONPair> pairs)
        {
            Pairs = pairs;
        }

        /// <summary>
        /// Outputs a text representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var toReturn = " ( Object | Pairs : ";
            foreach (var p in Pairs) toReturn += "\r\n\t" + p.ToString();
            return toReturn + "\r\n ) ";
        }
    }
}
