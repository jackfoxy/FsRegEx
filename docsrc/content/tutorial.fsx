(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/FsRegEx"
open FsRegEx

(**
Tutorial: F# Verbal Expressions
=================================

The `VerbalExpressions` module includes the `FsRegEx` type which wraps the familiar .NET `RegEx` in a type with useful functional members. 
Multiple constructors start with a regular expression in the constructor.
*)

let fsRegEx = new FsRegEx(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")

(**
This the module is an experimental DSL that allows you to compose regular expressions in natural language 
using the immutable `FsRegEx` type. The remainder of this tutorial is concerned with the experimental DSL which is, frankly, not that practical. 

For practical examples of using the core `FsRegEx` module for composability, see the following examples:

* [Matching](matching.html)

* [Matching Groups](group.html)

* [Replace and Split](replacesplit.html)

* [Capture](capture.html)


Verbal Expressions DSL
----------------------

You can compose values of the FsRegEx type with the |> operator, including creating a new regular expression by logical or on 2 existing FsRegExs. 

*)
#r "FsRegEx.dll"
open FsRegEx
open System.Text.RegularExpressions
open VerbalExpressions

let v =
    CommonFsRegEx.Email
    |> fsRegExOrFsRegEx RegexOptions.None CommonFsRegEx.Url

let foundEmail =
    v
    |> isMatch "test@github.com"

let foundUrl =
    v
    |> isMatch "http://www.google.com"

printfn "%b" foundEmail
printfn "%b" foundUrl

// true
// true

(**
Natural language composition consists of building up a new FsRegEx from an old by functions which append special characters, groups, modifiers, and other attributes of the regular expression language.

function : 'T -> FsRegEx -> FsRegEx

See the API documentation for all the regular expression functions available.
*)

open VerbalExpressions

let foundFromGithub =
    FsRegEx()
    |> startOfLine
    |> something
    |> then' "github.com"
    |> endOfLine
    |> isMatch "test@github.com"

printfn "%b" foundFromGithub

// true

(**
You do not have to worry about escaping special characters in your regular expression.
*)

let foundSomethingSpecial =
    FsRegEx()
    |> startOfLine
    |> something
    |> then' "*+?"
    |> anything
    |> isMatch "blah blah blah*+?yackety yack"

printfn "%b" foundSomethingSpecial

// true

(**
Sometimes you may need more power than the natural language provides, or you just need to include a snippet of native regular expression. The add function lets you do that.
*)

let foundSpecialInMultiline =
    FsRegEx()
    |> add @"phrase1\*\+\?"
    |> anything
    |> isMatch @"phrase1*+?RestOfLine\n"
    
printfn "%b" foundSpecialInMultiline

// true

(**
FsRegExs posses all the power of the .Net RegEx class in a composable form.
*)

let n =
    FsRegEx()
    |> word
    |> matches "three words here"

printfn "%i" n.Length

// 3

let betterFormat =
    FsRegEx()
    |> add "\s+"
    |> or' "whitespace"
    |> replace "This     is   text with   far  too   much   whitespace" " "

printfn "%s" betterFormat

// This is text with far too much  

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
FsRegEx comes with first class support for unicode, including unicode general categories and .Net extension blocks.
*)

FsRegEx()
|> beginCaptureNamed "upper"
|> unicodeCategory Unicode.UnicodeGeneralCategory.LetterUppercase
|> add "+"
|> endCapture
|> capture "some mixed case WORDS" "upper"
|> printfn "%s"

// WORDS