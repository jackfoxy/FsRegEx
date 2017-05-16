// https://msdn.microsoft.com/en-us/library/3y21t6y4(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open FsRegEx
open System.Text.RegularExpressions

let partNumbers = [| "1298-673-4192"; "A08Z-931-468A"; 
                        "_A90-123-129X"; "12345-KKA-1230"; 
                        "0919-2893-1256" |]
let rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$")

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
                    partNumber
                    (if (rgx.IsMatch(partNumber)) then "is" 
                     else "is not")


// The example displays the following output:
//       1298-673-4192 is a valid part number.
//       A08Z-931-468A is a valid part number.
//       _A90-123-129X is not a valid part number.
//       12345-KKA-1230 is not a valid part number.
//       0919-2893-1256 is not a valid part number.

let fsRegEx = new FsRegEx(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$")

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
                    partNumber
                    (if (fsRegEx.IsMatch(partNumber)) then "is" 
                     else "is not")

// https://msdn.microsoft.com/en-us/library/z1hx7a5y(v=vs.110).aspx

let partNumbers = [| "Part Number: 1298-673-4192"; "Part No: A08Z-931-468A"; 
                        "_A90-123-129X"; "123K-000-1230"; 
                        "SKU: 0919-2893-1256" |]
let rgx = new Regex(@"[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$")

for partNumber in partNumbers do
    let start = partNumber.IndexOf(':')
    if (start >= 0) then
        printfn "%s %s a valid part number."
                        partNumber
                        (if (rgx.IsMatch(partNumber, start)) then "is" else "is not")
    else
        printfn "Cannot find starting position in %s." partNumber

// The example displays the following output:
//       Part Number: 1298-673-4192 is a valid part number.
//       Part No: A08Z-931-468A is a valid part number.
//       Cannot find starting position in _A90-123-129X.
//       Cannot find starting position in 123K-000-1230.
//       SKU: 0919-2893-1256 is not a valid part number.

let fsRegEx = new FsRegEx(@"[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$")

for partNumber in partNumbers do
    let start = partNumber.IndexOf(':')
    if (start >= 0) then
        printfn "%s %s a valid part number."
                        partNumber
                        (if (fsRegEx.IsMatch(partNumber, start)) then "is" else "is not")
    else
        printfn "Cannot find starting position in %s." partNumber



// https://msdn.microsoft.com/en-us/library/sdx2bds0(v=vs.110).aspx

let partNumbers = [| "1298-673-4192"; "A08Z-931-468A"; 
                        "_A90-123-129X"; "12345-KKA-1230"; 
                        "0919-2893-1256" |]
let pattern = @"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$"

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
                    partNumber
                    (if (Regex.IsMatch(partNumber, pattern)) then
                        "is"
                     else "is not")

// The example displays the following output:
//       1298-673-4192 is a valid part number.
//       A08Z-931-468A is a valid part number.
//       _A90-123-129X is not a valid part number.
//       12345-KKA-1230 is not a valid part number.
//       0919-2893-1256 is not a valid part number.

// https://msdn.microsoft.com/en-us/library/ktzf2d23(v=vs.110).aspx

let pattern = @"^[A-Z0-9]\d{2}[A-Z0-9](-\d{3}){2}[A-Z0-9]$"

for partNumber in partNumbers do
    printfn "%s %s a valid part number."
                    partNumber
                    (if (Regex.IsMatch(partNumber, pattern, RegexOptions.IgnoreCase)) then
                        "is"
                     else "is not")

// The example displays the following output:
//       1298-673-4192 is a valid part number.
//       A08Z-931-468a is a valid part number.
//       _A90-123-129X is not a valid part number.
//       12345-KKA-1230 is not a valid part number.
//       0919-2893-1256 is not a valid part number

// https://msdn.microsoft.com/en-us/library/hh160210(v=vs.110).aspx

for partNumber in partNumbers do
    try 
        printfn "%s %s a valid part number."
                        partNumber
                        (if (Regex.IsMatch(partNumber, pattern, RegexOptions.IgnoreCase, (System.TimeSpan.FromMilliseconds(500.)))) then 
                            "is"
                         else "is not")
    with :? RegexMatchTimeoutException as e ->

        printfn "Timeout after %i seconds matching %s."
            e.MatchTimeout.Seconds e.Input

// The example displays the following output:
//       1298-673-4192 is a valid part number.
//       A08Z-931-468a is a valid part number.
//       _A90-123-129X is not a valid part number.
//       12345-KKA-1230 is not a valid part number.
//       0919-2893-1256 is not a valid part number.

for partNumber in partNumbers do
    try 
        printfn "%s %s a valid part number."
                        partNumber
                        (if (Regex.IsMatch(partNumber, pattern, RegexOptions.IgnoreCase, (System.TimeSpan.FromMilliseconds(500.)))) then 
                            "is"
                         else "is not")
    with :? RegexMatchTimeoutException as e ->

        printfn "Timeout after %i seconds matching %s."
            e.MatchTimeout.Seconds e.Input