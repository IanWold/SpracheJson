using System;

namespace SpracheJSON
{
    /// <summary>
    /// Represents a literal value in JSON:
    /// A string, number, boolean, or null value
    /// </summary>
    public class JSONLiteral : JSONValue
    {
        /// <summary>
        /// A string representation of the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The type the value is
        /// </summary>
        public LiteralType Type { get; set; }

        public JSONLiteral(string value, LiteralType type)
        {
            Value = value;
            Type = type;
        }

        public object Get()
        {
            switch (Type)
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

        /// <summary>
        /// Outputs a text representation of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "( Literal | Value: " + ((Value != null) ? Value : "<null>") + ", " +
                "Type: " + ((Type == LiteralType.String)    ? "String"  :
                            (Type == LiteralType.Number)    ? "Number"  :
                            (Type == LiteralType.Boolean)   ? "Boolean" : "Null")
                + " )";
        }
    }

    /// <summary>
    /// A list of the types a JSON literal value can be
    /// </summary>
    public enum LiteralType
    {
        String,
        Number,
        Boolean,
        Null
    }
}
