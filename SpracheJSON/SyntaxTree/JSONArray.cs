using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpracheJSON
{
    public class JSONArray : JSONValue
    {
        public IEnumerable<JSONValue> Elements { get; set; }

        public JSONArray(IEnumerable<JSONValue> elements)
        {
            Elements = elements;
        }

        public override string ToString()
        {
            var toReturn = " ( Array | Elements : ";
            foreach (var e in Elements) toReturn += "\r\n\t" + e.ToString();
            return toReturn + "\r\n ) ";
        }
    }
}
