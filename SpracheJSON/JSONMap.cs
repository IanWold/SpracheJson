using System;
using System.Collections;

namespace SpracheJSON
{
    static class JSONMap
    {
        static object MapObject(Type T, JSONObject toMap)
        {
            var toReturn = Activator.CreateInstance(T);

            foreach (var p in T.GetProperties())
            {
                if (toMap.Pairs.ContainsKey(p.Name))
                {
                    p.SetValue(toReturn, MapValue(p.PropertyType, toMap[p.Name]));
                }
            }

            foreach (var f in T.GetFields())
            {
                if (toMap.Pairs.ContainsKey(f.Name))
                {
                    f.SetValue(toReturn, MapValue(f.FieldType, toMap[f.Name]));
                }
            }

            return toReturn;
        }

        static object MapArray(Type T, JSONArray toMap)
        {
            if (!T.IsArray || T.GetInterface("System.Collections.IList") == null)
            {
                throw new ArgumentException(T + " can't map JSONArray.");
            }
            else
            {
                IList toReturnList = (T.IsArray) ? new ArrayList() : (IList)Activator.CreateInstance(T);

                foreach (var e in toMap.Elements)
                {
                    toReturnList.Add(MapValue(T.GetElementType(), e));
                }

                if (T.IsArray)
                {
                    var c = toReturnList.Count;
                    var toReturn = Array.CreateInstance(T.GetElementType(), c);

                    for (int i = 0; i < c; i++)
                    {
                        toReturn.SetValue(toReturnList[i], i);
                    }

                    return toReturn;
                }
                else return toReturnList;
            }
        }

        static object MapLiteral(Type T, JSONLiteral toMap)
        {
            if (toMap.ValueType == LiteralType.Null)
            {
                if (T.IsClass || Nullable.GetUnderlyingType(T) != null) return null;
                else throw new ArgumentException(T + " can't be null.");
            }
            else return toMap.Get();
        }

        public static object MapValue(Type T, JSONValue toMap)
        {
            if (toMap is JSONObject) return MapObject(T, (JSONObject)toMap);
            else if (toMap is JSONArray) return MapArray(T, (JSONArray)toMap);
            else if (toMap is JSONLiteral) return MapLiteral(T, (JSONLiteral)toMap);
            else throw new ArgumentException("Cannot map vanilla JSONValue.");
        }
    }
}
