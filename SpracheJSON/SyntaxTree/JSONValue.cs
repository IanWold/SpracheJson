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

        public virtual JSONValue this[string key]
        {
            get { return null; }
            set { }
        }

        public virtual JSONValue this[int i]
        {
            get { return null; }
            set { }
        }
    }
}
