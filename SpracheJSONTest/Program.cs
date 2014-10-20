using System;
using System.IO;
using SpracheJSON;

namespace SpracheJSONTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            var Parsed = JSON.ParseDocument("TestFile.json");
            timer.Stop();

            JSON.WriteDocument(Parsed, "TestOutFile.json");

            Console.Write("Parse completed in " + timer.ElapsedMilliseconds + " milliseconds. Press ENTER to open output file.");

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
