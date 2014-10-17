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
        public IDictionary<string, JSONValue> Pairs { get; set; }

        public JSONObject(IEnumerable<KeyValuePair<string, JSONValue>> pairs)
        {
            Pairs = new Dictionary<string, JSONValue>();

            foreach (var p in pairs)
            {
                Pairs.Add(p);
            }
        }

        /// <summary>
        /// Outputs a text representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var toReturn = "( Object | Pairs : ";
            foreach (var p in Pairs) toReturn += "\r\n" + Tabify(PairString(p));
            return toReturn + "\r\n)";
        }

        string PairString<T,U>(KeyValuePair<T,U> pair)
        {
            return "( Pair | Key: " + pair.Key.ToString() + ", Value: " + pair.Value.ToString() + " )";
        }

        string Tabify(string toTab)
        {
            var lines = toTab.Split('\n');
            var toReturn = "";
            foreach (var l in lines) toReturn += "\t" + l + "\n";
            return toReturn.Substring(0, toReturn.Length - 1);
        }
    }
}
