// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.group(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let pattern = @"(\b(\w+?)[,:;]?\s?)+[?.!]"
let input = "This is one sentence. This is a second sentence."

let match' = Regex.Match(input, pattern)
printfn "Match: %s" match'.Value
let mutable groupCtr = 0

for group in match'.Groups do
    groupCtr <- groupCtr + 1
    printfn "   Group %i: '%s'" groupCtr group.Value
    let mutable captureCtr = 0
    for capture in group.Captures do
        captureCtr <- captureCtr + 1
        printfn "      Capture %i: '%s'" captureCtr capture.Value

// The example displays the following output:
//       Match: This is one sentence.
//          Group 1: 'This is one sentence.'
//             Capture 1: 'This is one sentence.'
//          Group 2: 'sentence'
//             Capture 1: 'This '
//             Capture 2: 'is '
//             Capture 3: 'one '
//             Capture 4: 'sentence'
//          Group 3: 'sentence'
//             Capture 1: 'This'
//             Capture 2: 'is'
//             Capture 3: 'one'
//             Capture 4: 'sentence'

let m' = FsRegEx.firstMatch pattern input
printfn "Match: %s" m'.Value

printfn "Named Groups:"
m'.Groups()
|> Array.iteri (fun i g -> 
    printfn "   Group %i: '%s'" i g.Value
    g.Captures()
    |> Array.iteri (fun i c ->  
        printfn "      Capture %i: '%s'" i c.Value 
        )
    )