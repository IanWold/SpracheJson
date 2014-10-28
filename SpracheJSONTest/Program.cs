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

            var Address = JSON.MapValue<TestAddress>(Parsed["address"]);

            Console.WriteLine("The home phone number of {0} is {1}.", name, homePhone);
            Console.WriteLine("His address is:");
            Console.WriteLine("\t{0}", Address.streetAddress);
            Console.WriteLine("\t{0}, {1} {2}", Address.city, Address.state, Address.postalCode);

            var key = Console.ReadKey();
        }
    }
    public class TestAddress
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }
}
