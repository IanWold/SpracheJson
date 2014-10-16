using System;
using System.IO;
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
