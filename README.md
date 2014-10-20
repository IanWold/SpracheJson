SpracheJSON
===========

SpracheJSON is a JSON parser for C# written with the awesome [Sprache](https://github.com/sprache/Sprache) framework. I built this in much the same vain as [SpracheDown](https://github.com/IanWold/SpracheDown), more as a side project than anything. However, SpracheJSON is a **fully-functioning** JSON parser (as far as I'm aware), and as such you can use it for any of your JSON parsing needs.

SpracheJSON can parse JSON from a string or from a file. It parses the JSON into a very simple abstract syntax tree. To parse the JSON, do one of these:

```c#
var Parsed = JSON.ParseDocument("MyFile.json");
```

SpracheJSON can also write out the abstract syntax tree back into JSON. The method is more/less the same:

```c#
JSON.WriteDocument(Parsed, "MyFile.json");
```

Contributing
============

Comments/Issues/Pull Requests are all very welcome any time!
