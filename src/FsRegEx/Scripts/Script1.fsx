#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions
open FsRegEx

let emailregex = new Regex("(?<user>[^@]+)@(?<host>.+)")

let printMatch (m : Match) s =

    if m.Success  then
        m.Groups.["user"].Value
        |> printfn "User: %s"
        m.Groups.["host"].Value
        |> printfn "Host: %s"
        m.Captures
        |> printfn "%A"
    else 
        printfn "%s is not a valid email address" s

let s = "johndoe@tempuri.org"
let m = emailregex.Match(s)


let s' = "jackfoxy@gmail.com"
let m' = emailregex.Match(s')

printMatch m s

printMatch m' s'

let emailFsRegEx = new FsRegEx("(?<user>[^@]+)@(?<host>.+)")

emailFsRegEx

//@@@@@@@@@@@@@@@@@@@@@@@
let parseRegexTimeStamp (regex : Regex) source =
    try
        let m = regex.Matches source
        m.[0].Groups.[1].Value
        //|> Success
    with _ -> 
        sprintf "cannot parse timestamp from source: %s" source
       // |> Failure

//@@@@@@@@@@@@@@@@@@@@@@@

let parseTimeStamp (fsRegEx : FsRegEx) source =
    try
        let m = fsRegEx.Matches source
        m.[0].Groups().[1].Value
        //|> Success
    with _ -> 
        sprintf "cannot parse timestamp from source: %s" source
       // |> Failure

let timeStampFsRegEx = (new FsRegEx("\"timeStamp\"\s*:\s*\"(\d{4}-\d{2}-\d{2}-\d{2}-\d{2})\""))
let timeStampRegex = (new Regex("\"timeStamp\"\s*:\s*\"(\d{4}-\d{2}-\d{2}-\d{2}-\d{2})\""))

let source1 = """ "quick": false, 
        "timeStamp": "2016-07-14-08-14"
    } """

let source2 = """ "quick": false, 
        "timeStamp": "2015-06-10-11-12"
    } """

parseRegexTimeStamp timeStampRegex source1
parseRegexTimeStamp timeStampRegex source2

//bug:
parseTimeStamp timeStampFsRegEx source1
parseTimeStamp timeStampFsRegEx source2

//@@@@@@@@@@@@@@@@@@@@

let text = "One car red car blue car";
let pat = @"(\w+)\s+(car)";

let fsMatches = matches pat text

let groupNumberRegExp = @"COD(?<GroupNumber>[0-9]{3})END"
let groupNumberName = "GroupNumber"

capture groupNumberRegExp groupNumberName "COD123END"
|> printfn "%s"

let carRegExp = @"(\w+)\s+(car)"

sprintf "%s %s %s" "One car" "red car" "blue car"
|> matches carRegExp
|> Array.map (fun m -> m.Value)
|> Array.iter(fun x -> printfn "%s" x)

sprintf "%s" "COD123END"
|> capture groupNumberRegExp groupNumberName
|> printfn "%s"