// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.getgroupnames(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let ShowMatches (r : Regex)  (m : Match) =
    let names = r.GetGroupNames()

    printfn "Named Groups:"
    
    for name in names do
         let grp = m.Groups.[name]
         printfn "   %s: '%s'" name grp.Value
    ()

let pattern = @"\b(?<FirstWord>\w+)\s?((\w+)\s)*(?<LastWord>\w+)?(?<Punctuation>\p{Po})"
let input = "The cow jumped over the moon."
let rgx = new Regex(pattern)
let m = rgx.Match(input)
if m.Success then
    ShowMatches rgx m
else ()

//       Named Groups:
//          0: 'The cow jumped over the moon.'
//          1: 'the '
//          2: 'the'
//          FirstWord: 'The'
//          LastWord: 'moon'
//          Punctuation: '.'

let pattern' = @"\b(?<FirstWord>\w+)\s?((\w+)\s)*(?<LastWord>\w+)?(?<Punctuation>\p{Po})"
let input' = "The cow jumped over the moon."

let m' = FsRegEx.firstMatch pattern' input'

printfn "Named Groups:"

m'.Groups()
|> Array.iter (fun g -> printfn "   %s: '%s'" g.Name g.Value)