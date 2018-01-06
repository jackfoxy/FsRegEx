(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/FsRegEx/net47"

(**
Matching
========

IsMatch
-------

The `System.Text.RegularExpressions.Regex` Class provides several overloads of `isMatch`. The same overloads are available in the `FsRegEx` class and as individually 
named functions, including overloads and functions using `RegexOptions`. `Timeout` is not supported at this time.
*)

(*** hide ***) 
#r "FsRegEx.dll"

(**
### is match on first occurence ###
*)
open FsRegEx
module F = FsRegEx

let partNumbers = [| "1298-673-4192"; "A08Z-931-468A"; 
                    "_A90-123-129X"; "12345-KKA-1230"; 
                    "0919-2893-1256" |]

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
        partNumber
        (if (F.isMatch @"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$" partNumber ) then "is" 
            else "is not")

let fsRegEx = new FsRegEx(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$")

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
        partNumber
        (if (fsRegEx.IsMatch(partNumber)) then "is" 
            else "is not")


// 1298-673-4192 is a valid part number.
// A08Z-931-468A is a valid part number.
// _A90-123-129X is not a valid part number.
// 12345-KKA-1230 is not a valid part number.
// 0919-2893-1256 is not a valid part number.
(**
### is match starting at position ###
*)
let labeledPartNumbers = [| "Part Number: 1298-673-4192"; "Part No: A08Z-931-468A"; 
                        "_A90-123-129X"; "123K-000-1230"; 
                        "SKU: 0919-2893-1256" |]

for partNumber in labeledPartNumbers do
    let start = partNumber.IndexOf(':')
    if (start >= 0) then
        printfn "%s %s a valid part number."
            partNumber
            (if (F.isMatchAt @"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$" start partNumber) then "is" else "is not")
    else
        printfn "Cannot find starting position in %s." partNumber

for partNumber in labeledPartNumbers do
    let start = partNumber.IndexOf(':')
    if (start >= 0) then
        printfn "%s %s a valid part number."
            partNumber
            (if (fsRegEx.IsMatch(partNumber, start)) then "is" else "is not")
    else
        printfn "Cannot find starting position in %s." partNumber

// Part Number: 1298-673-4192 is a valid part number.
// Part No: A08Z-931-468A is a valid part number.
// Cannot find starting position in _A90-123-129X.
// Cannot find starting position in 123K-000-1230.
// SKU: 0919-2893-1256 is not a valid part number.
(**
FsMatch
-------

`FsMatch` provides the functionality in the `System.Text.RegularExpressions.Match` Class, but returning arrays of objects instead of special collections.

### iterate over matches ###
*)
"Who writes these notes?"
|> FsRegEx.matches @"\b\w+es\b"
|> Array.iter (fun m -> printfn "Found '%s' at position %i" m.Value m.Index)

// Found 'writes' at position 4
// Found 'notes' at position 17
(**
### first match and iteration ###
*)
let regExpr = @"\b\w+es\b"
let sentence = "Who writes these notes and uses our paper?"

let m = FsRegEx.firstMatch regExpr sentence

if m.Success then
    printfn "Found first 'es' in '%s' at position %i" m.Value m.Index
    sentence
    |> FsRegEx.matchesAt regExpr (m.Index + m.Length)
    |> Array.iter (fun m -> printfn "Also found '%s' at position %i" m.Value m.Index)

// Found first 'es' in 'writes' at position 4
// Also found 'notes' at position 17
// Also found 'uses' at position 27