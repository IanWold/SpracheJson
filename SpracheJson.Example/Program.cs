using SpracheJson;
using SpracheJson.Example;
using static SpracheJson.Json;

//Create a timer to test the running time
var timer = new System.Diagnostics.Stopwatch();
timer.Start();

//Parse the JSON
IJsonValue Parsed;
using (var reader = new StreamReader("TestFile.json"))
{
	Parsed = Parse(reader.ReadToEnd());
}

timer.Stop();

//Write the parsed JSON as a JSON file
using (var writer = new StreamWriter("OutputFile.json"))
{
	writer.Write(Serialize(Parsed));
	//This works the same:
	//writer.Write(Parsed.ToJson());
}

Console.WriteLine($"Parse completed in {timer.ElapsedMilliseconds} milliseconds.");

//Refer to TestFile.json
//Get John's name and his home phone number from the AST
var name = $"{Parsed["FirstName"]} {Parsed["LastName"]}";
var homePhone = Parsed["PhoneNumber"][0]["Number"];

//Map his address onto our own TestAddress class
var Address = Map<Address>(Parsed["Address"]);

//Write out all his info
Console.WriteLine($"The home phone number of {name} is {homePhone}.");
Console.WriteLine($"His address is:\r\n{Address}");

//Write the Address object to a file.
using (var writer = new StreamWriter("OutputAddress.json"))
{
	writer.Write(Serialize(Address));
}

var _ = Console.ReadKey();
