using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpracheJSON
{
    public class JSONObject : JSONValue
    {
        public IEnumerable<JSONPair> Pairs { get; set; }

        public JSONObject(IEnumerable<JSONPair> pairs)
        {
            Pairs = pairs;
        }

        public override string ToString()
        {
            var toReturn = " ( Object | Pairs : ";
            foreach (var p in Pairs) toReturn += "\r\n\t" + p.ToString();
            return toReturn + "\r\n ) ";
        }
    }
}
