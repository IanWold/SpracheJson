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
        /// Outputs a text representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var toReturn = " ( Array | Elements : ";
            foreach (var e in Elements) toReturn += "\r\n\t" + e.ToString();
            return toReturn + "\r\n ) ";
        }
    }
}
