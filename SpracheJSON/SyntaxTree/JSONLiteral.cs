using System;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a literal value in JSON:
    /// A string, number, boolean, or null value
    /// </summary>
    public class JSONLiteral : IJSONValue
    {
        /// <summary>
        /// A string representation of the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The type the value is
        /// </summary>
        public LiteralType ValueType { get; set; }

		public JSONLiteral(LiteralType type)
		{
			ValueType = type;
		}

        public JSONLiteral(string value, LiteralType type)
        {
            Value = value;
            ValueType = type;
        }

        /// <summary>
        /// Returns Value cast as the appropriate type.
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            switch (ValueType)
            {
                case LiteralType.String:
                    return Value;

                case LiteralType.Number:
                    return Convert.ToDouble(Value);

                case LiteralType.Boolean:
                    return (Value.ToLower() == "true") ? true : false;

                default:
                    return null;
            }
		}

		public IJSONValue this[string key]
		{
			get { throw new NotImplementedException("Cannot access JSONArray by string."); }
			set { throw new NotImplementedException("Cannot access JSONArray by string."); }
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

            switch (ValueType)
            {
                case LiteralType.String:
                    toReturn = "\"" + JSON.GetJSONString(Value) + "\"";
                    break;

                case LiteralType.Null:
                    toReturn = "null";
                    break;

                default:
                    toReturn = Value;
                    break;
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the Value property
        /// </summary>
        /// <returns>this.Value</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
