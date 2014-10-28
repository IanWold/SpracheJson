using System.Collections.Generic;
using Sprache;

namespace SpracheJSON
{
    /// <summary>
    /// Contains the entire JSON Parser
    /// </summary>
    static class JSONParser
    {
        /// <summary>
        /// Parses a literal null value
        /// </summary>
        static readonly Parser<JSONLiteral> JNull =
            from str in Parse.IgnoreCase("null")
            select new JSONLiteral(null, LiteralType.Null);

        /// <summary>
        /// Parses a literal boolean value
        /// </summary>
        static readonly Parser<JSONLiteral> JBoolean =
            from str in Parse.IgnoreCase("true").Text().Or(Parse.IgnoreCase("false").Text())
            select new JSONLiteral(str, LiteralType.Boolean);

        /// <summary>
        /// Parses the exponential part of a number
        /// </summary>
        static readonly Parser<string> JExp =
            from e in Parse.IgnoreCase("e").Text()
            from sign in Parse.String("+").Text()
                         .Or(Parse.String("-").Text())
                         .Optional()
            from digits in Parse.Digit.Many().Text()
            select e + ((sign.IsDefined) ? sign.Get() : "") + digits;

        /// <summary>
        /// Parses the decimal part of a number
        /// </summary>
        static readonly Parser<string> JFrac =
            from dot in Parse.String(".").Text()
            from digits in Parse.Digit.Many().Text()
            select dot + digits;

        /// <summary>
        /// Parses the integer part of anumber
        /// </summary>
        static readonly Parser<string> JInt =
            from minus in Parse.String("-").Text().Optional()
            from digits in Parse.Digit.Many().Text()
            select (minus.IsDefined ? minus.Get() : "") + digits;

        /// <summary>
        /// Parses a JSON number
        /// </summary>
        static readonly Parser<JSONLiteral> JNumber =
            from integer in JInt
            from frac in JFrac.Optional()
            from exp in JExp.Optional()
            select new JSONLiteral(GetNumber(integer, frac, exp), LiteralType.Number);

        /// <summary>
        /// Accepts all the parts from the JNumber parser and assembles it into one string representing the number
        /// </summary>
        /// <param name="integer">The integer part of the number</param>
        /// <param name="frac">The optional decimal part of the number</param>
        /// <param name="exp">The optional exponential part of the number</param>
        /// <returns>A string containing the JSON number</returns>
        static string GetNumber(string integer, IOption<string> frac, IOption<string> exp)
        {
            if (frac.IsDefined) integer += frac.Get();
            if (exp.IsDefined) integer += exp.Get();

            return integer;
        }

        /// <summary>
        /// Parses a control char, which is a character preceded by the escape character '\'
        /// </summary>
        static readonly Parser<char> ControlChar =
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

        /// <summary>
        /// Parses a JSON character
        /// </summary>
        static readonly Parser<char> JChar = Parse.AnyChar.Except(Parse.Char('"').Or(Parse.Char('\\'))).Or(ControlChar);

        /// <summary>
        /// Parses a JSON string
        /// </summary>
        static readonly Parser<JSONLiteral> JString =
            from first in Parse.Char('"')
            from chars in JChar.Many().Text()
            from last in Parse.Char('"')
            select new JSONLiteral(chars, LiteralType.String);

        /// <summary>
        /// Parses any literal JSON value: string, number, boolean, null
        /// </summary>
        static readonly Parser<JSONLiteral> JLiteral =
            JString
            .XOr(JNumber)
            .XOr(JBoolean)
            .XOr(JNull);

        /// <summary>
        /// Parses any JSON value
        /// </summary>
        static readonly Parser<JSONValue> JValue =
            Parse.Ref(() => JObject)
            .Or(Parse.Ref(() => JArray))
            .Or(JLiteral);

        /// <summary>
        /// Parses the elements within a JSON array
        /// </summary>
        static readonly Parser<IEnumerable<JSONValue>> JElements = JValue.DelimitedBy(Parse.Char(',').Token());

        /// <summary>
        /// Parses a JSON array
        /// </summary>
        static readonly Parser<JSONValue> JArray =
            from first in Parse.Char('[').Token()
            from elements in JElements.Optional()
            from last in Parse.Char(']').Token()
            select new JSONArray(elements.IsDefined ? elements.Get() : null);

        /// <summary>
        /// Parses a JSON pair
        /// </summary>
        static readonly Parser<KeyValuePair<string, JSONValue>> JPair =
            from name in JString
            from colon in Parse.Char(':').Token()
            from val in JValue
            select new KeyValuePair<string, JSONValue>(name.Value, val);

        /// <summary>
        /// Parses all the pairs (members) of a JSON object
        /// </summary>
        static readonly Parser<IEnumerable<KeyValuePair<string, JSONValue>>> JMembers = JPair.DelimitedBy(Parse.Char(',').Token());

        /// <summary>
        /// Parses a JSON object
        /// </summary>
        static readonly Parser<JSONValue> JObject =
            from first in Parse.Char('{').Token()
            from members in JMembers.Optional()
            from last in Parse.Char('}').Token()
            select new JSONObject(members.IsDefined ? members.Get() : null);

        public static JSONObject ParseJSON(string toParse)
        {
            return (JSONObject)JObject.Parse(toParse);
        }
    }
}
