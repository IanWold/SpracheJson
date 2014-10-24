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

            Console.WriteLine("Parse completed in " + timer.ElapsedMilliseconds + " milliseconds.");

            var name = Parsed["firstName"] + " " + Parsed["lastName"];
            var homePhone = Parsed["phoneNumber"][0]["number"];

            Console.Write("The home phone number of {0} is {1}.", name, homePhone);

            var key = Console.ReadKey();
        }
    }
}
