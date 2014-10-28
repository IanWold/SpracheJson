SpracheJSON
===========

SpracheJSON is a JSON parser for C# written with the awesome [Sprache](https://github.com/sprache/Sprache) framework. I built this in much the same vain as [SpracheDown](https://github.com/IanWold/SpracheDown), more as a side project than anything. However, SpracheJSON is a **fully-functioning** JSON parser (as far as I'm aware), and as such you can use it for any of your JSON parsing needs.

SpracheJSON can parse JSON from a string or from a file. It parses the JSON into a very simple abstract syntax tree. Imagine you have the following JSON file:

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

You can parse that with the following line:

```c#
var Parsed = JSON.ParseDocument("MyFile.json");
```

From here, you have a `JSONObject` called `Parsed` that you can work with. Getting values from the syntax tree is pretty easy:

```c#
Console.WriteLine(Parsed["Field1"] as JSONLiteral);
//Writes 'Hello, World!'

var name = Parsed["Field2"][1]["name"]; //Gets a JSONLiteral object representing the name
var age = Parsed["Field2"][1]["age"]; //Gets a JSONLiteral object representing the age
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
var people = JSON.MapValue<Person[]>(Parsed["Field2"]);
```

SpracheJSON can also write the abstract syntax tree back into JSON. The method is more/less the same:

```c#
JSON.WriteDocument(Parsed, "MyFile.json");
```

In fact, every object of the syntax tree has a `ToJSON()` method you can call to get its JSON string representation:

```c#
var json = Parsed.ToJSON();
```

This outputs the same properly formatted JSON shown above.

It should be noted that the object mapping shown above is kinda beta right now (needs error handling), and non-AST objects can't be written into JSON.

Contributing
============

Comments/Issues/Pull Requests are all very welcome any time!
