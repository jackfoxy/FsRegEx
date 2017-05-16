// https://msdn.microsoft.com/en-us/library/e7sf90t3(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System
open System.Text.RegularExpressions

let pattern = @"\b\w+es\b"
let rgx = new Regex(pattern)
let sentence = "Who writes these notes?"

for m in rgx.Matches(sentence) do
    printfn "Found '%s' at position %i" m.Value m.Index

// The example displays the following output:
//       Found 'writes' at position 4
//       Found 'notes' at position 17

sentence
|> FsRegEx.matches pattern
|> Array.iter (fun m -> printfn "Found '%s' at position %i" m.Value m.Index)


// https://msdn.microsoft.com/en-us/library/z9ekkwhs(v=vs.110).aspx

let pattern = @"\b\w+es\b"
let rgx = new Regex(pattern)
let sentence = "Who writes these notes and uses our paper?"

// Get the first match.
let m = rgx.Match(sentence)
if m.Success then
    printfn "Found first 'es' in '%s' at position %i" m.Value m.Index
    // Get any additional matches.
    for m in (rgx.Matches(sentence, m.Index + m.Length)) do
        printfn "Also found '%s' at position %i" m.Value m.Index
 
// The example displays the following output:
//       Found first 'es' in 'writes' at position 4
//       Also found 'notes' at position 17
//       Also found 'uses' at position 27

let m = FsRegEx.firstMatch pattern sentence

if m.Success then
    printfn "Found first 'es' in '%s' at position %i" m.Value m.Index
    sentence
    |> FsRegEx.matchesAt pattern (m.Index + m.Length)
    |> Array.iter (fun m -> printfn "Also found '%s' at position %i" m.Value m.Index)

// https://msdn.microsoft.com/en-us/library/b9712a7w(v=vs.110).aspx

let pattern = @"\b\w+es\b"
let sentence = "Who writes these notes?"

for m in (Regex.Matches(sentence, pattern)) do
    printfn "Found '%s' at position %i" m.Value m.Index

// The example displays the following output:
//       Found 'writes' at position 4
//       Found 'notes' at position 17

// https://msdn.microsoft.com/en-us/library/b49yw9s8(v=vs.110).aspx

let pattern = @"\b\w+es\b"
let sentence = "NOTES: Any notes or comments are optional."

// Call Matches method without specifying any options.
for m in (Regex.Matches(sentence, pattern)) do
    printfn "Found '%s' at position %i" m.Value m.Index
printfn ""

// Call Matches method for case-insensitive matching.
for m in (Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase)) do
    printfn "Found '%s' at position %i" m.Value m.Index

// The example displays the following output:
//       Found 'notes' at position 11
//       
//       Found 'NOTES' at position 0
//       Found 'notes' at position 11

// https://msdn.microsoft.com/en-us/library/hh160201(v=vs.110).aspx

let pattern = @"\b\w+es\b"
let sentence = "NOTES: Any notes or comments are optional."

// Call Matches method without specifying any options.
try 
    for m in (Regex.Matches(sentence, pattern,
                                        RegexOptions.None,
                                        TimeSpan.FromSeconds(1.0))) do
        printfn "Found '%s' at position %i" m.Value m.Index
with
    | :? RegexMatchTimeoutException as e ->
        ()
    // Do Nothing: Assume that timeout represents no match.
printfn ""

// Call Matches method for case-insensitive matching.
try 
    for match' in (Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase)) do
        printfn "Found '%s' at position %i" match'.Value match'.Index
with
    | :? RegexMatchTimeoutException as e ->
        ()
    // Do Nothing: Assume that timeout represents no match.

// The example displays the following output:
//       Found 'notes' at position 11
//       
//       Found 'NOTES' at position 0
//       Found 'notes' at position 11