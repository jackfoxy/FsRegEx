(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/FsRegEx"

(**
FsRegEx
======================

The `FsRegEx` library provides composable F# functionality for nearly all the capabilites of the .NET `Regex` class, supporting uniform pipe forward `|>` composability and all `Regex` features except timeouts. Optionally you can compose F# verbal expressions in natural language. Lazy evaluation ensures natural language composition imposes no performance penalty.

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The FsRegEx library can be <a href="https://nuget.org/packages/FsRegEx">installed from NuGet</a>:
      <pre>PM> Install-Package FsRegEx</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Introduction
------------

The `FsRegEx` library brings a composable regular expression experience to F#.

 * Composable functions representing all available `Regex` functionality (except timeouts) with the target input string uniformly the last parameter to better support pipe forward `|>` composition and partial application. 
 
 * Collections returned as F# arrays rather than `Regex` special collections for better composability (but sacrificing lazy match evaluation and timeout support).

 * Short-cut functions like `capture` on group name make common multi-step processes a single function.

  * `FsRegEx` - A composable F# wrapper type over `System.Text.RegularExpressions.Regex` for natural language regular expression composition.

 * `FsMatch` - A composable F# wrapper type over `System.Text.RegularExpressions.Match`.

 * `FsGroup` - A composable F# wrapper type over `System.Text.RegularExpressions.Group`.

 * Basic functional operations on `FsRegEx` (`exists`, `fold`, `foldBack`, `iter`, and `map`), among other composable functions.

* `FsRegEx` speciall collections are returned as arrays. This does lose the advantange of lazy match evaluation the special collections provide. 
The underlying object can always be returned from the wrapper.
*)

(*** hide ***) 
#r "FsRegEx.dll"

(**
Better F# Composition
------------------------------------
*)
open FsRegEx

let carRegExp = @"(\w+)\s+(car)"

sprintf "%s %s %s" "One car" "red car" "blue car"
|> matches carRegExp
|> Array.map (fun m -> m.Value)
|> Array.iter(fun x -> printfn "%s" x)

// One car
// red car
// blue car

(**
Short-cut common procedures to a single function
------------------------------------------------
*)
let groupNumberRegExp = @"COD(?<GroupNumber>[0-9]{3})END"
let groupNumberName = "GroupNumber"

sprintf "%s" "COD123END"
|> capture groupNumberRegExp groupNumberName
|> printfn "%s"

// 123

(**
Natural Language Regular Expressions
------------------------------------

The library also provides the composable Verbal Expressions language for constructing simple regular expressions in a readable fashion.
*)
open FsRegEx
open System.Text.RegularExpressions
open VerbalExpressions

let groupName =  "GroupNumber"
 
FsRegEx()
|> add "COD"
|> beginCaptureNamed groupName
|> any "0-9"
|> repeatPrevious 3
|> endCapture
|> then' "END"
|> capture "COD123END" groupName
|> printfn "%s"

// 123

(**
A comparison of using Regex natively, and Verbal Expressions to retrieve the database parameter from a connection string:
*)

let TestConnString = 
    """<add name="MyConnString" connectionString="Server=MyServer;Database=MyDatabase;User ID=me;Password=secret;Encrypt=True;TrustServerCertificate=True;Enlist=False;" />"""

let databaseFromRegex connectionString =
        
    Regex.Match(connectionString, "[dD]atabase=(?<database>.*?);").Groups.["database"].Value

let databaseFromFsRegEx connectionString =

    FsRegEx()
    |> add "[dD]atabase=(?<database>.*?);"
    |> capture connectionString "database"

(databaseFromRegex TestConnString) = (databaseFromFsRegEx TestConnString) 
|> printfn "%b"

// true

(**
This example shows the more verbose and desctriptive Verbal Expressions:
*)

let databaseFromVerboseFsRegEx connectionString =

    FsRegEx()
    |> anyOf "dD"
    |> then' "atabase="
    |> beginCaptureNamed "database"
    |> add ".*?"
    |> endCapture
    |> then' ";"
    |> capture connectionString "database"

(databaseFromRegex TestConnString) = (databaseFromVerboseFsRegEx TestConnString) 
|> printfn "%b"

// true

(**
Documentation
-------------

FsRegEx comes with comprehensive API documentation and a tutorial. 

 * [API Reference](reference/index.html) contains documentation for all types, modules, and functions in the library. 

 * [F# Verbal Expressions Tutorial](tutorial.html) contains further explanation of the natural language DSL syntax and many more usage examples.

Versioning
-----------------------

 FsRegEx adheres to [Semantic Versioning](http://semver.org/ "Semantic Versioning"). So long as the project is pre-1.0.0 minor versions may be breaking. Once the project reaches 1.0.0 minor enhancements will be backwards-compatible.
 
Contributing and copyright
--------------------------

FsRegEx is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project, and submit pull requests, so long as pull requests:

 * include excellent unit test coverage 
 * include correct intellisense documentation
 * adhere to the concepts of composability

FsRegEx is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: http://github.com/VerbalExpressions/FSharpVerbalExpressions/tree/master/docs/content
  [gh]: http://github.com/VerbalExpressions/FSharpVerbalExpressions
  [issues]: http://github.com/VerbalExpressions/FSharpVerbalExpressions/issues
  [readme]: http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/README.md
  [license]: http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/LICENSE.txt
*)
