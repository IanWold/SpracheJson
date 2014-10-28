using System;
using System.IO;
using SpracheJSON;

namespace SpracheJSONTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a timer to test the running time
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            //Parse the JSON
            var Parsed = JSON.ParseDocument("TestFile.json");
            timer.Stop();

            //Write the parsed JSON as a JSON file
            JSON.WriteDocument(Parsed, "OutputFile.json");

            Console.WriteLine("Parse completed in " + timer.ElapsedMilliseconds + " milliseconds.");

            //Refer to TestFile.json
            //Get John's name and his home phone number from the AST
            var name = Parsed["firstName"] + " " + Parsed["lastName"];
            var homePhone = Parsed["phoneNumber"][0]["number"];

            //Map his address onto our own TestAddress class
            var Address = JSON.MapValue<TestAddress>(Parsed["address"]);

            //Write out all his info
            Console.WriteLine("The home phone number of {0} is {1}.", name, homePhone);
            Console.WriteLine("His address is:");
            Console.WriteLine("\t{0}", Address.streetAddress);
            Console.WriteLine("\t{0}, {1} {2}", Address.city, Address.state, Address.postalCode);

            var key = Console.ReadKey();
        }
    }

    /// <summary>
    /// A class which mirrors the address information in TestFile.json
    /// </summary>
    public class TestAddress
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }
}
