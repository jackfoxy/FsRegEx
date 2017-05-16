// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.groupnamefromnumber(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

open System
open System.Collections.Generic

let pattern = @"(?<city>[A-Za-z\s]+), (?<state>[A-Za-z]{2}) (?<zip>\d{5}(-\d{4})?)"
let cityLines = [|"New York, NY 10003"; "Brooklyn, NY 11238"; "Detroit, MI 48204"; 
                    "San Francisco, CA 94109"; "Seattle, WA 98109" |]
let rgx = new Regex(pattern)
let names = new List<string>()
let mutable ctr = 1
let mutable exitFlag = false

while (not exitFlag) do 
    let name = rgx.GroupNameFromNumber(ctr)
    if (not (String.IsNullOrEmpty(name))) then
        ctr <- ctr + 1
        names.Add(name)
    else
        exitFlag <- true

for cityLine in cityLines do
    let m = rgx.Match(cityLine)
    if (m.Success) then
        printfn "Zip code %s is in %s, %s."
                        m.Groups.[names.[3]].Value
                        m.Groups.[names.[1]].Value
                        m.Groups.[names.[2]].Value

// The example displays the following output:
//       Zip code 10003 is in New York, NY.
//       Zip code 11238 is in Brooklyn, NY.
//       Zip code 48204 is in Detroit, MI.
//       Zip code 94109 is in San Francisco, CA.
//       Zip code 98109 is in Seattle, WA.

let names' = FsRegEx.groupNames

cityLines
|> Array.iter ()
