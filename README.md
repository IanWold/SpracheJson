# SpracheJson

SpracheJson is a JSON parser for C# written with the awesome [Sprache](https://github.com/sprache/Sprache) framework. I built this in much the same vain as [SpracheDown](https://github.com/IanWold/SpracheDown), more as a side project than anything. However, SpracheJson is a **fully-functioning** JSON parser (as far as I'm aware), and as such you can use it for any of your JSON parsing needs.

# Documentation

I have a full, MSDN-style documentation of the library on the [GitHub wiki](https://github.com/IanWold/SpracheJson/wiki) for this project; all public members of the library are documented there! In future I hope to get some examples and tutorials there.

## Quick Tutorial

SpracheJson can parse JSON from a string or from a file. It parses the JSON into a very simple syntax tree. Imagine you have the following JSON file in "MyFile.json":

```json
{
	"Field1": "Hello, world!",
	"Field2": [
		{
			"Name": "John",
			"Age": 23
		},
		{
			"Name": "Bob",
			"Age": 46
		}
	]
}
```

To parse that into JSON, you can read it and parse it like so:

```c#
var reader = new StreamReader("MyFile.json");
var parsed = Json.Parse(reader.ReadToEnd());
```

From here, you have a `JsonObject` called `Parsed` that you can work with. Getting values from the syntax tree is pretty easy:

```c#
Console.WriteLine(parsed["Field1"]);
//Writes 'Hello, World!'

var name = parsed["Field2"][1]["name"]; //Gets a JsonLiteral object for the name
var age = parsed["Field2"][1]["age"]; //Gets a JsonLiteral object for the age
Console.WriteLine("The age of {0} is {1}", name, age);
//Writes 'The age of Bob is 46'
```

Now, let's say that we've got the following class `Person`:

```c#
public class Person
{
	public string Name { get; set; }
	public int Age { get; set; }
}
```

SpracheJson can then map our parsed JSON document onto an array of `Person` objects:

```c#
var people = Json.Map<Person[]>(parsed["Field2"]);
```

Any JsonValue object can write itself back into JSON, so you only need the following to write `Parsed` back to MyFile.json:

```c#
var writer = new StreamWriter("MyFile.json");
writer.Write(parsed.ToJson());
```

SpracheJson also allows you to serialize any object into JSON. Suppose we wanted to write `people` into its own file:

```c#
var writer2 = new StreamWriter("MyOtherFile.json");
writer2.Write(Json.Serialize(people));
```

It should be noted that the object mapping shown above is kinda beta right now (it's in need of error handling). It's being tested and worked on, but if you want to contribute, you definitely should!

# Contributing

Comments/Issues/Pull Requests are all very welcome any time!
