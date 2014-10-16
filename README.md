SpracheJSON
===========

SpracheJSON is a JSON parser for C# written with the awesome [Sprache](https://github.com/sprache/Sprache) framework. I built this in much the same vain as [SpracheDown](https://github.com/IanWold/SpracheDown), more as a side project than anything. However, SpracheJSON is a **fully-functioning** JSON parser (as far as I'm aware), and as such you can use it for any of your JSON parsing needs.

SpracheJSON works like so:

```c#
var toParse = ""; //String containing all your JSON code
var Parsed = JSONParser.ParseDocument(toParse);
```

`Parsed` is the syntax tree that SpracheJSON returns. Parsed will be of type `JSONObject`, which contains all the subvalues in the parameter `JSONObject.Pairs`.

Contributing
============

Comments/Issues/Pull Requests are all very welcome any time!
