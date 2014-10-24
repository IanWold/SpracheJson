using System;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a JSON value
    /// </summary>
    public class JSONValue
    {
        /// <summary>
        /// Returns a string representing the object in JSON
        /// </summary>
        /// <returns></returns>
        public virtual string ToJSON()
        {
            return null;
        }

        /// <summary>
        /// Allows key indexing without downcasting to JSONObject
        /// </summary>
        /// <param name="key">The key to index by</param>
        /// <returns>Throws an exception unless overridden by JSONObject</returns>
        public virtual JSONValue this[string key]
        {
            get { throw new Exception("Cannot index JSONValue"); }
            set { throw new Exception("Cannot index JSONValue"); }
        }

        /// <summary>
        /// Allows indexing without downcasting to JSONArray
        /// </summary>
        /// <param name="i">The integer to index by</param>
        /// <returns>Throws an exception unless overridden by JSONArray</returns>
        public virtual JSONValue this[int i]
        {
            get { throw new Exception("Cannot index JSONValue"); }
            set { throw new Exception("Cannot index JSONValue"); }
        }
    }
}
