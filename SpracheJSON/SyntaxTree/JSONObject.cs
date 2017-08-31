using System;
using System.Collections.Generic;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a JSON object:
    /// A collection of JSON pairs
    /// </summary>
    public class JSONObject : IJSONValue
    {
        /// <summary>
        /// All the JSON pair objects
        /// </summary>
        public Dictionary<string, IJSONValue> Pairs { get; set; }

		public JSONObject()
		{
			Pairs = new Dictionary<string, IJSONValue>();
		}

        public JSONObject(IEnumerable<KeyValuePair<string, IJSONValue>> pairs)
        {
            Pairs = new Dictionary<string, IJSONValue>();
            if (pairs != null) foreach (var p in pairs) Pairs.Add(p.Key, p.Value);
        }

        /// <summary>
        /// Makes Pairs directly accessable
        /// </summary>
        /// <param name="key">The key of the IJSONValue</param>
        /// <returns>The IJSONValue at that key</returns>
        public IJSONValue this[string key]
        {
            get
            {
                if (Pairs.ContainsKey(key)) return Pairs[key];
                else throw new ArgumentException("Key not found: " + key);
            }
            set
            {
                if (Pairs.ContainsKey(key)) Pairs[key] = value;
                else throw new ArgumentException("Key not found: " + key);
            }
		}

		public IJSONValue this[int i]
		{
			get { throw new NotImplementedException("Cannot access JSONArray by string."); }
			set { throw new NotImplementedException("Cannot access JSONArray by string."); }
		}

		/// <summary>
		/// Returns a string representing the object in JSON
		/// </summary>
		/// <returns></returns>
		public string ToJSON()
        {
            var toReturn = "";
            foreach (var p in Pairs) toReturn += "\"" + p.Key + "\"" + ": " + p.Value.ToJSON() + ",\r\n";
            toReturn = JSON.Tabify(toReturn.Substring(0, toReturn.Length - 3));
            return "{\r\n" + toReturn + "\r\n}";
        }
    }
}
