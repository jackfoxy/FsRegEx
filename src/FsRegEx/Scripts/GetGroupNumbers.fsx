// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.getgroupnumbers(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions
open System

let pattern = @"\b((?<word>\w+)\s*)+(?<end>[.?!])"
let input = "This is a sentence. This is a second sentence."

let rgx = new Regex(pattern)
let groupNumbers = rgx.GetGroupNumbers()

let m = rgx.Match(input)

// does GroupNameFromNumber & inverse need to be option type?
if m.Success then
    printfn "Match: %s" m.Value
    for groupNumber in groupNumbers do
        let name = rgx.GroupNameFromNumber(groupNumber)

        let isNumber, _ = Int32.TryParse(name)

        printfn "   Group %i%s: '%s'"
            groupNumber
            (if (String.IsNullOrEmpty(name) |> not) 
                && (not isNumber) then
                " (" + name + ")"
                else String.Empty)
            m.Groups.[groupNumber].Value
else ()

// The example displays the following output:
//       Match: This is a sentence.
//          Group 0: 'This is a sentence.'
//          Group 1: 'sentence'
//          Group 2 (word): 'sentence'
//          Group 3 (end): '.'

let groupNumbers' = FsRegEx.groupNumbers pattern
let groupNames = FsRegEx.groupNames pattern

let m' = FsRegEx.firstMatch pattern input

Array.zip groupNumbers' groupNames
|> Array.iter (fun (n, name) ->
    let isNumber, _ = Int32.TryParse(name)
    printfn "   Group %i%s: '%s'" 
        n 
        (if (not isNumber) then
            " (" + name + ")"
         else String.Empty)
        (m'.Groups()).[n].Value
    )