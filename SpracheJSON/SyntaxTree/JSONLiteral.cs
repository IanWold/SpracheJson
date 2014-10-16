using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpracheJSON
{
    public class JSONLiteral : JSONValue
    {
        public string Value { get; set; }

        public LiteralType Type { get; set; }

        public JSONLiteral(string value, LiteralType type)
        {
            Value = value;
            Type = type;
        }

        public override string ToString()
        {
            return " ( Literal | Value: " + ((Value != null) ? Value : "<null>") + ", " +
                "Type: " + ((Type == LiteralType.String)    ? "String"  :
                            (Type == LiteralType.Number)    ? "Number"  :
                            (Type == LiteralType.Null)      ? "Null"    :
                            (Type == LiteralType.Boolean)   ? "Boolean" : "");
        }
    }

    public enum LiteralType
    {
        String,
        Number,
        Boolean,
        Null
    }
}
