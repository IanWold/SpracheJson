using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace SpracheJSON
{
    public static class JSONParser
    {
        public static JSONObject ParseDocument(string toParse)
        {
            return (JSONObject)JObject.Parse(toParse);
        }

        private static readonly Parser<JSONLiteral> JNull =
            from str in Parse.IgnoreCase("null")
            select new JSONLiteral(null, LiteralType.Null);

        private static readonly Parser<JSONLiteral> JBoolean =
            from str in Parse.IgnoreCase("true").Or(Parse.IgnoreCase("false"))
            select new JSONLiteral(GetString(str), LiteralType.Boolean);

        private static readonly Parser<string> JExp =
            from e in Parse.IgnoreCase("e")
            from sign in Parse.String("+")
                         .Or(Parse.String("-"))
                         .Optional()
            from digits in Parse.Digit.Many()
            select GetString(e) + ((sign.IsDefined) ? GetString(sign.Get()) : "") + GetString(digits);

        private static readonly Parser<string> JFrac =
            from dot in Parse.String(".")
            from digits in Parse.Digit.Many()
            select GetString(dot) + GetString(digits);

        private static readonly Parser<string> JInt =
            from minus in Parse.String("-").Optional()
            from digits in Parse.Digit.Many()
            select minus.IsDefined ? GetString(minus.Get()) + GetString(digits) : GetString(digits);

        private static readonly Parser<JSONLiteral> JNumber =
            from integer in JInt
            from frac in JFrac.Optional()
            from exp in JExp.Optional()
            select new JSONLiteral(GetNumber(integer, frac, exp), LiteralType.Number);

        private static string GetNumber(string integer, IOption<string> frac, IOption<string> exp)
        {
            if (frac.IsDefined) integer += frac.Get();
            if (exp.IsDefined) integer += exp.Get();

            return integer;
        }

        private static string GetString(IEnumerable<char> val)
        {
            string toReturn = "";
            foreach (var c in val) toReturn += c.ToString();
            return toReturn;
        }

        private static readonly Parser<char> ControlChar =
            from first in Parse.Char('\\')
            from next in Parse.Char('\"')
                         .Or(Parse.Char('\\'))
                         .Or(Parse.Char('/'))
                         .Or(Parse.Char('b'))
                         .Or(Parse.Char('f'))
                         .Or(Parse.Char('n'))
                         .Or(Parse.Char('r'))
                         .Or(Parse.Char('t'))
            select ((next == 't') ? '\t' :
                    (next == 'r') ? '\r' :
                    (next == 'n') ? '\n' :
                    (next == 'f') ? '\f' :
                    (next == 'b') ? '\b' :
                    next );

        private static readonly Parser<char> JChar = Parse.AnyChar.Except(Parse.Char('"').Or(Parse.Char('\\'))).Or(ControlChar);

        private static readonly Parser<JSONLiteral> JString =
            from first in Parse.Char('"')
            from chars in JChar.Many()
            from last in Parse.Char('"')
            select new JSONLiteral(GetString(chars), LiteralType.String);

        private static readonly Parser<JSONLiteral> JLiteral =
            JString
            .XOr(JNumber)
            .XOr(JBoolean)
            .XOr(JNull);

        private static readonly Parser<JSONValue> JValue =
            Parse.Ref(() => JObject)
            .Or(Parse.Ref(() => JArray))
            .Or(JLiteral);

        private static readonly Parser<IEnumerable<JSONValue>> JElements = JValue.DelimitedBy(Parse.Char(',').Token());

        private static readonly Parser<JSONValue> JArray =
            from first in Parse.Char('[').Token()
            from elements in JElements.Optional()
            from last in Parse.Char(']').Token()
            select new JSONArray(elements.IsDefined ? elements.Get() : null);

        private static readonly Parser<JSONPair> JPair =
            from name in JString
            from colon in Parse.Char(':').Token()
            from val in JValue
            select new JSONPair(name.Value, val);

        private static readonly Parser<IEnumerable<JSONPair>> JMembers = JPair.DelimitedBy(Parse.Char(',').Token());

        private static readonly Parser<JSONValue> JObject =
            from first in Parse.Char('{').Token()
            from members in JMembers.Optional()
            from last in Parse.Char('}').Token()
            select new JSONObject(members.IsDefined ? members.Get() : null);
    }
}
