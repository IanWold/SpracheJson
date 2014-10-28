using System;
using System.Collections;

namespace SpracheJSON
{
    static class JSONSerialize
    {
        static string WriteObject(Type T, object toWrite)
        {
            var toReturn = "";

            //Loop through all the properties of the type
            foreach (var p in T.GetProperties())
            {
                toReturn += "\"" + p.Name + "\": " + WriteValue(p.PropertyType, p.GetValue(toWrite)) + ",\r\n";
            }

            //Loop through all the fields of the type
            foreach (var f in T.GetFields())
            {
                toReturn += "\"" + f.Name + "\": " + WriteValue(f.FieldType, f.GetValue(toWrite)) + ",\r\n";
            }

            return "{\r\n" + JSON.Tabify(toReturn.Substring(0, toReturn.Length - 3)) + "\r\n}";
        }

        static string WriteArray(Type T, object toWrite)
        {
            string toReturn = "";

            foreach (var element in (IList)toWrite)
            {
                toReturn += WriteValue(T.GetElementType(), element) + ",\r\n";
            }

            return "[\r\n" + JSON.Tabify(toReturn.Substring(0, toReturn.Length - 3)) + "\r\n]";
        }

        static string WriteLiteral(Type T, object toWrite)
        {
            if (T.IsEquivalentTo(typeof(double)))
            {
                return Convert.ToDouble(toWrite).ToString();
            }
            else if (T.IsEquivalentTo(typeof(bool)))
            {
                var b = toWrite as bool?;
                if (b == true) return "true";
                else return "false";
            }
            else if (T.IsEquivalentTo(typeof(string)))
            {
                return "\"" + JSON.GetJSONString(toWrite.ToString()) + "\"";
            }
            else throw new ArgumentException(T + " can't be serialized as a literal JSON value.");
        }

        public static string WriteValue(Type T, object toWrite)
        {
            if (toWrite == null) return "null";
            else if (T.IsArray || T.GetInterface("System.Collections.IList") != null) return WriteArray(T, toWrite);
            else if (T.IsPrimitive || T.IsEquivalentTo(typeof(string)) || T.IsEquivalentTo(typeof(bool))) return WriteLiteral(T, toWrite);
            else return WriteObject(T, toWrite);
        }
    }
}
