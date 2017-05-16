// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.escape(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let pattern = Regex.Escape("[") + "(.*?)]" 
let input = "The animal [what kind?] was visible [by whom?] from the window."

let matches = Regex.Matches(input, pattern)
let mutable commentNumber = 0

printfn "%s produces the following matches:" pattern
for match' in matches do
    commentNumber <- commentNumber + 1
    printfn "   %i: %s" commentNumber match'.Value

// This example displays the following output:
//       \[(.*?)] produces the following matches:
//          1: [what kind?]
//          2: [by whom?]

printfn "%s produces the following matches:" pattern

FsRegEx.matches pattern input
|> Array.iteri (fun i x -> printfn "   %i: %s" (i + 1) x.Value)
