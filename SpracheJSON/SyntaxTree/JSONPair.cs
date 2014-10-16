namespace SpracheJSON
{
    /// <summary>
    /// Represents a JSON pair:
    /// A key and a corresponding JSON value
    /// </summary>
    public class JSONPair
    {
        /// <summary>
        /// The text of the key string
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value of the pair
        /// </summary>
        public JSONValue Value { get; set; }

        public JSONPair(string key, JSONValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Outputs a text representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return " ( Pair | Key: " + Key + ", Value: " + Value + " ) ";
        }
    }
}
