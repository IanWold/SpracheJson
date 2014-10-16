using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpracheJSON
{
    public class JSONPair
    {
        public string Key { get; set; }
        public JSONValue Value { get; set; }

        public JSONPair(string key, JSONValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return " ( Pair | Key: " + Key + ", Value: " + Value + " ) ";
        }
    }
}
