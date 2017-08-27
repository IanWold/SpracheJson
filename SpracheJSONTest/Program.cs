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
			IJSONValue Parsed;
            using (var reader = new StreamReader("TestFile.json"))
            {
                Parsed = JSON.Parse(reader.ReadToEnd());
            }

            timer.Stop();

            //Write the parsed JSON as a JSON file
            using (var writer = new StreamWriter("OutputFile.json"))
            {
                writer.Write(JSON.Write(Parsed));
                //This works the same:
                //writer.Write(Parsed.ToJSON());
            }

            Console.WriteLine("Parse completed in " + timer.ElapsedMilliseconds + " milliseconds.");

            //Refer to TestFile.json
            //Get John's name and his home phone number from the AST
            var name = Parsed["firstName"] + " " + Parsed["lastName"];
            var homePhone = Parsed["phoneNumber"][0]["number"];

            //Map his address onto our own TestAddress class
            var Address = JSON.Map<TestAddress>(Parsed["address"]);

            //Write out all his info
            Console.WriteLine("The home phone number of {0} is {1}.", name, homePhone);
            Console.WriteLine("His address is:\r\n" + Address);

            //Write the Address object to a file.
            using (var writer = new StreamWriter("OutputAddress.json"))
            {
                writer.Write(JSON.Write(Address));
            }

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

        public override string ToString()
        {
            return string.Format("{0}\r\n{1} {2} {3}",
                                 streetAddress,
                                 city,
                                 state,
                                 postalCode);
        }
    }
}
