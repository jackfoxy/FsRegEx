// https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.groupcollection(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let pattern = @"\b(\w+?)([\u00AE\u2122])"
let input = "Microsoft® Office Professional Edition combines several office " +
                "productivity products, including Word, Excel®, Access®, Outlook®, " +
                "PowerPoint®, and several others. Some guidelines for creating " +
                "corporate documents using these productivity tools are available " +
                "from the documents created using Silverlight™ on the corporate " +
                "intranet site."

let matches = Regex.Matches(input, pattern)
for match' in matches do
    let groups = match'.Groups
    printfn "%s: %s" groups.[2].Value groups.[1].Value
                           
printfn ""
printfn "Found %i trademarks or registered trademarks." matches.Count

// ®: Microsoft
// ®: Excel
// ®: Access
// ®: Outlook
// ®: PowerPoint
// ™: Silverlight
