// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.matchtimeout(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System
open System.Text.RegularExpressions

let domain = AppDomain.CurrentDomain
// Set a timeout interval of 2 seconds.
domain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(2.))
let timeout = domain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT")
printfn "Default regex match timeout: %O"
                    (if (isNull timeout) then ("<null>" :> obj) else timeout)

let rgx = new Regex("[aeiouy]")
printfn "Regular expression pattern: %s" (rgx.ToString())
printfn "Timeout interval for this regex: %f seconds" rgx.MatchTimeout.TotalSeconds