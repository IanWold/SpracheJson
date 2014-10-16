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

            Console.Write("Parse successful. Press ENTER to open output file.");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                var writer = new StreamWriter("OutputFile.txt");
                writer.Write(Parsed.ToString());
                writer.Close();

                System.Diagnostics.Process.Start("OutputFile.txt");
            }
        }
    }
}
