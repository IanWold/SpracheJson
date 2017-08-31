# SpracheJSON [![Travis](https://img.shields.io/travis/IanWold/SpracheJSON.svg?style=flat-square)](https://travis-ci.org/IanWold/SpracheJSON)

SpracheJSON is a JSON parser for C# written with the awesome [Sprache](https://github.com/sprache/Sprache) framework. I built this in much the same vain as [SpracheDown](https://github.com/IanWold/SpracheDown), more as a side project than anything. However, SpracheJSON is a **fully-functioning** JSON parser (as far as I'm aware), and as such you can use it for any of your JSON parsing needs.

SpracheJSON can parse JSON from a string or from a file. It parses the JSON into a very simple syntax tree. Imagine you have the following JSON file in "MyFile.json":

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
var Parsed = JSON.Parse(reader.ReadToEnd());
```

From here, you have a `JSONObject` called `Parsed` that you can work with. Getting values from the syntax tree is pretty easy:

```c#
Console.WriteLine(Parsed["Field1"]);
//Writes 'Hello, World!'

var name = Parsed["Field2"][1]["name"]; //Gets a JSONLiteral object for the name
var age = Parsed["Field2"][1]["age"]; //Gets a JSONLiteral object for the age
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

SpraceJSON can then map our parsed JSON document onto an array of `Person` objects:

```c#
var people = JSON.Map<Person[]>(Parsed["Field2"]);
```

Any JSONValue object can write itself back into JSON, so you only need the following to write `Parsed` back to MyFile.json:

```c#
var writer = new StreamWriter("MyFile.json");
writer.Write(Parsed.ToJSON());
```

SpracheJSON also allows you to serialize any object into JSON. Suppose we wanted to write `people` into its own file:

```c#
var writer2 = new StreamWriter("MyOtherFile.json");
writer2.Write(JSON.Write(people));
```

It should be noted that the object mapping shown above is kinda beta right now (it's in need of error handling). It's being tested and worked on, but if you want to contribute, you definitely should!

# Contributing

Comments/Issues/Pull Requests are all very welcome any time!
