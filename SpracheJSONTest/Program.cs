using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpracheJSON;

namespace SpracheJSONTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader("TestFile.json");
            var toParse = reader.ReadToEnd();
            reader.Close();

            var Parsed = JSONParser.ParseDocument(toParse);

            Console.WriteLine(Parsed);
            Console.ReadLine();
        }
    }
}
