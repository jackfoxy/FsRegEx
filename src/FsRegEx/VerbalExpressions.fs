module VerbalExpressions

open FsRegEx
open System.Text.RegularExpressions

let firstMatch input (fsRegEx : FsRegEx) = 
    fsRegEx .Match input

let matchAt input startAt (fsRegEx : FsRegEx) = 
    fsRegEx .Match (input, startAt)

let matches input (fsRegEx : FsRegEx) = 
    fsRegEx .Matches input

let matchesAt input startAt (fsRegEx : FsRegEx) = 
    fsRegEx .Matches (input, startAt)

let matchAtFor input startAt length (fsRegEx : FsRegEx) = 
    fsRegEx .Match (input, startAt, length)

let replace input (replacement : string) (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, replacement)

let replaceByMatch input (evalutor : MatchEvaluator) (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, evalutor)

let replaceMaxTimes input (replacement : string) count (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, replacement, count )

let replaceByMatchMaxTimes input (evalutor : MatchEvaluator) count (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, evalutor, count)

let replaceMaxTimesStartAt input (replacement : string) count startAt (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, replacement, count, startAt)

let replaceByMatchMaxTimesStartAt input (evalutor : MatchEvaluator) count startAt (fsRegEx : FsRegEx) =
    fsRegEx .Replace(input, evalutor, count, startAt)

let split input (fsRegEx : FsRegEx) =
    fsRegEx .Split input

let splitMaxTimes input count (fsRegEx : FsRegEx) =
    fsRegEx .Split (input, count)

let splitMaxTimesStartAt input count startAt (fsRegEx : FsRegEx) =
    fsRegEx .Split (input, count, startAt)

let resetRegexOptions regexOptions (fsRegEx : FsRegEx) =
    fsRegEx .Clone regexOptions

let capture input groupName (fsRegEx : FsRegEx) =
    fsRegEx .Capture input groupName

let groupNames (fsRegEx : FsRegEx) =
    fsRegEx .GroupNames()

let groupNumbers (fsRegEx : FsRegEx) =
    fsRegEx .GroupNumbers()

let isMatch value (fsRegEx : FsRegEx) =
    fsRegEx .IsMatch value

let isMatchAt value startAt (fsRegEx : FsRegEx) =
    fsRegEx .IsMatch (value, startAt)
     
let toString (fsRegEx : FsRegEx) =
    fsRegEx .ToString()

let startOfLine (fsRegEx : FsRegEx) =
    let v = fsRegEx .Clone None
    if v.Prefixes.StartsWith("^") then v
    else 
        v.Prefixes <- "^" + v.Prefixes
        v

let endOfLine (fsRegEx : FsRegEx) =
    let v = fsRegEx .Clone None
    if v.Suffixes.EndsWith("$") then v
    else 
        v.Suffixes <- v.Suffixes + "$"
        v

let add value (fsRegEx : FsRegEx) =
    let v = fsRegEx .Clone None
    v.Source <- v.Source + value
    v

let internalAdd (fsRegEx : FsRegEx) value =
    let v = fsRegEx .Clone None
    v.Source <- v.Source + value
    v

let then' value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf "(%s)"
    |> internalAdd fsRegEx 

let find value (fsRegEx : FsRegEx) = then' value fsRegEx 

let maybe value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf "(%s)?"
    |> internalAdd fsRegEx 

let anything (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  "(.*)"

let anythingBut value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf "([^%s]*)"
    |> internalAdd fsRegEx 

let something (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  "(.+)"

let somethingBut value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf "([^%s]+)"
    |> internalAdd fsRegEx 

let lineBreak (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"(\n|(\r\n))"

let br (fsRegEx : FsRegEx) = lineBreak fsRegEx 

let tab (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\t"

let whiteSpace (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\s"

let nonWhiteSpace (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\S"

let word (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\w+"

let wordCharacter (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\w"

let nonWordCharacter (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\W"

let digit (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\d"

let nonDigit (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  @"\D"

let backReference ordinal (fsRegEx : FsRegEx) =
    if ordinal < 1 then invalidArg "ordinal" "must be greater than 0" 

    sprintf @"\%i" ordinal
    |> internalAdd fsRegEx  

let namedBackReference name (fsRegEx : FsRegEx) =
    sprintf @"\k<%s>" name
    |> internalAdd fsRegEx 

let anyOf value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf "[%s]"
    |> internalAdd fsRegEx 

let any value (fsRegEx : FsRegEx) = anyOf value fsRegEx 

let range (collection : seq<obj>) (fsRegEx : FsRegEx) =

    let elem =
        collection
        |> Seq.fold (fun s t -> 
            let x = t.ToString()
            if x.Length = 0 then s
            else Regex.Escape(x)::s) []
        |> List.rev

    let divisor =
        if elem.Length % 2 = 0 then
            elem.Length / 2
        else
            elem.Length / 2 + 1
                
    List.splitInto divisor elem
    |> List.fold (fun (s : string) t -> 
        if t.Length = 1 then
            if s.Length = 0 then
                invalidArg "range" "one-element range"
            else
                s + "|" + t.Head 
        else
            s + ("[" + t.Head + "-" + t.Tail.Head + "]") ) ""
    |> internalAdd fsRegEx 

let multiple value (fsRegEx : FsRegEx) =
    Regex.Escape value
    |> sprintf @"(%s)+"
    |> internalAdd fsRegEx 

let multipleFsRegEx (sourceFsRegEx : FsRegEx) (fsRegEx : FsRegEx) =
    sourceFsRegEx.ToString()
    |> sprintf @"(%s)+"
    |> internalAdd fsRegEx 

let or' value (fsRegEx : FsRegEx) =
    let v = fsRegEx .Clone None
    if v.Source.EndsWith("(") || v.Source.EndsWith(">") then //begin capture
        v.Source <- v.Source + value
    else
        if v.Prefixes.Length > 0 || v.Source.Length > 0 then
            v.Source <- v.Source + "|" + value
        else
            v.Source <- value
    v

let fsRegExOrFsRegEx regexOptions (fsRegEx : FsRegEx) (fsRegEx2 : FsRegEx) =
    let v = fsRegEx .Clone (Some regexOptions)
    v.Prefixes <- v.Prefixes + "("
    v.Source <- v.Source + ")|(" + fsRegEx2.ToString()
    v.Suffixes <- ")" + v.Suffixes
    v

let beginCapture (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  "("

let beginCaptureNamed groupName (fsRegEx : FsRegEx) =
    Regex.Escape groupName
    |> sprintf "(?<%s>"
    |> internalAdd fsRegEx 

let endCapture (fsRegEx : FsRegEx) =
    internalAdd fsRegEx  ")"

let repeatPrevious n (fsRegEx : FsRegEx) =

    if n < 1 then
        invalidArg "repeatPrevious" "must be greater than 0"

    sprintf "{%i}" n
    |> internalAdd fsRegEx 

let repeatBetweenPrevious n m (fsRegEx : FsRegEx) =

    if n < 1 then
        invalidArg "repeatBetweenPrevious" "must be greater than 0"

    if n >= m then
        invalidArg "repeatBetweenPrevious" "m must be greater than n"

    sprintf "{%i,%i}" n m
    |> internalAdd fsRegEx 

let unicode  nnnn (fsRegEx : FsRegEx) =

    sprintf "\u%s" nnnn
    |> internalAdd fsRegEx 

let unicodeCategory (category : Unicode.UnicodeGeneralCategory) (fsRegEx : FsRegEx) =
        
    category.ToString()
    |> sprintf "\p{%s}" 
    |> internalAdd fsRegEx 

let notUnicodeCategory (category : Unicode.UnicodeGeneralCategory) (fsRegEx : FsRegEx) =

    category.ToString()
    |> sprintf "\P{%s}" 
    |> internalAdd fsRegEx 

let namedBlock (name : Unicode.SupportedNamedBlock) (fsRegEx : FsRegEx) =

    name.ToString()
    |> sprintf "\p{%s}" 
    |> internalAdd fsRegEx 

let notNamedBlock (name : Unicode.SupportedNamedBlock) (fsRegEx : FsRegEx) =

    name.ToString()
    |> sprintf "\P{%s}" 
    |> internalAdd fsRegEx 

let iter (f : (FsRegEx -> unit)) (fsRegEx : FsRegEx)  : unit = f fsRegEx 

let map (f : (FsRegEx -> FsRegEx)) fsRegEx  = f fsRegEx 

let fold (f : ('State -> FsRegEx -> 'State)) state fsRegEx  = f state fsRegEx 

let foldBack (f : (FsRegEx -> 'State -> 'State)) fsRegEx  state = f fsRegEx  state

let exists (f : (FsRegEx -> bool)) fsRegEx  = f fsRegEx 