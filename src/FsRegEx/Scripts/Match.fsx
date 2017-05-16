// https://msdn.microsoft.com/en-us/library/twcw2f1c(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let text = "One car red car blue car"
let pat = @"(\w+)\s+(car)"

// Instantiate the regular expression object.
let r = new Regex(pat, RegexOptions.IgnoreCase)

// Match the regular expression pattern against a text string.
let mutable m = r.Match(text)
let mutable matchCount = 0
while m.Success do
    matchCount <- matchCount + 1
    printfn "Match%i" matchCount
    for (int i = 1 i <= 2 i++) 
        {
        Group g = m.Groups[i]
        printfn "Group"+i+"='" + g + "'")
        CaptureCollection cc = g.Captures
        for (int j = 0 j < cc.Count j++) 
        {
            Capture c = cc[j]
            System.printfn "Capture"+j+"='" + c + "', Position="+c.Index)
        }
        }
    m = m.NextMatch()
// This example displays the following output:
//       Match1
//       Group1='One'
//       Capture0='One', Position=0
//       Group2='car'
//       Capture0='car', Position=4
//       Match2
//       Group1='red'
//       Capture0='red', Position=8
//       Group2='car'
//       Capture0='car', Position=12
//       Match3
//       Group1='blue'
//       Capture0='blue', Position=16
//       Group2='car'
//       Capture0='car', Position=21

// https://msdn.microsoft.com/en-us/library/h09aybcd(v=vs.110).aspx
//(no code sample)

// https://msdn.microsoft.com/en-us/library/0z2heewz(v=vs.110).aspx

let input = "ablaze beagle choral dozen elementary fanatic " +
                "glaze hunger inept jazz kitchen lemon minus " +
                "night optical pizza quiz restoration stamina " +
                "train unrest vertical whiz xray yellow zealous"
let pattern = @"\b\w*z+\w*\b"

let mutable m = Regex.Match(input, pattern)

while m.Success do
    printfn "'%s' found at position %i" m.Value m.Index
    m <- m.NextMatch()
   
// The example displays the following output:
//    'ablaze' found at position 0
//    'dozen' found at position 21
//    'glaze' found at position 46
//    'jazz' found at position 65
//    'pizza' found at position 104
//    'quiz' found at position 110
//    'whiz' found at position 157
//    'zealous' found at position 174

FsRegEx.matches pattern input
|> Array.iter (fun m -> printfn "'%s' found at position %i" m.Value m.Index)

// https://msdn.microsoft.com/en-us/library/bk1x0726(v=vs.110).aspx

let pattern = @"\ba\w*\b"
let input = "An extraordinary day dawns with each new day."
let m = Regex.Match(input, pattern, RegexOptions.IgnoreCase)

if m.Success then
    printfn "Found '%s' at position %i." m.Value m.Index

// The example displays the following output:
//        Found 'An' at position 0.

let m = FsRegEx.firstMatchOpt pattern RegexOptions.IgnoreCase input

if m.Success then
    printfn "Found '%s' at position %i." m.Value m.Index

// https://msdn.microsoft.com/en-us/library/hh160204(v=vs.110).aspx
// (no code sample)