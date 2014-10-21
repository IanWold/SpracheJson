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

            JSON.WriteDocument(Parsed, "OutputFile.json");

            Console.Write("Parse completed in " + timer.ElapsedMilliseconds + " milliseconds.");

            var key = Console.ReadKey();
        }
    }
}
