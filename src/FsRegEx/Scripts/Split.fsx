// https://msdn.microsoft.com/en-us/library/ze12yx1d(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions
open FsRegEx

let regex = new Regex("-")         // Split on hyphens.
let substrings = regex.Split("plum--pear")

for m in substrings do
    printfn "'%s'" m

// The example displays the following output:
//    'plum'
//    ''
//    'pear'  

let pattern = @"\d+"
let rgx = new Regex(pattern)
let input = "123ABCDE456FGHIJKL789MNOPQ012"

string[] result = rgx.Split(input)
for (int ctr = 0 ctr < result.Length ctr++) {
    Console.Write("'{0}'", result[ctr])
    if (ctr < result.Length - 1) 
    Console.Write(", ")
}
printfn )

// The example displays the following output:
//       '', 'ABCDE', 'FGHIJKL', 'MNOPQ', ''

Regex regex = new Regex("(-)")         // Split on hyphens.
string[] substrings = regex.Split("plum-pear")
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}
// The example displays the following output:
//    'plum'
//    '-'
//    'pear' 

string input = @"07/14/2007"   
string pattern = @"(-)|(/)"
Regex regex = new Regex(pattern)
foreach (string result in regex.Split(input)) 
{
    printfn "'{0}'", result)
}
// Under .NET 1.0 and 1.1, the method returns an array of
// 3 elements, as follows:
//    '07'
//    '14'
//    '2007'
//
// Under .NET 2.0 and later, the method returns an array of
// 5 elements, as follows:
//    '07'
//    '/'
//    '14'
//    '/'
//    '2007' 

string input = "characters"
Regex regex = new Regex("")
string[] substrings = regex.Split(input)
Console.Write("{")
for(int ctr = 0 ctr < substrings.Length ctr++)
{
    Console.Write(substrings[ctr])
    if (ctr < substrings.Length - 1)
    Console.Write(", ")
}
printfn "}")

// The example displays the following output:   
//    {, c, h, a, r, a, c, t, e, r, s, }

// https://msdn.microsoft.com/en-us/library/98awefz7(v=vs.110).aspx

string pattern = @"\d+"
Regex rgx = new Regex(pattern)
string input = "123ABCDE456FGHIJKL789MNOPQ012"

string[] result = rgx.Split(input, 3)
for (int ctr = 0 ctr < result.Length ctr++) {
    Console.Write("'{0}'", result[ctr])
    if (ctr < result.Length - 1) 
    Console.Write(", ")
}
printfn )

// The example displays the following output:
//       '', 'ABCDE', 'FGHIJKL789MNOPQ012'

string pattern = "(-)"
string input = "apple-apricot-plum-pear-banana"
Regex regex = new Regex(pattern)         // Split on hyphens.
string[] substrings = regex.Split(input, 4)
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}
// The example displays the following output:
//       'apple'
//       '-'
//       'apricot'
//       '-'
//       'plum'
//       '-'
//       'pear-banana'  

string input = @"07/14/2007"   
string pattern = @"(-)|(/)"
Regex regex = new Regex(pattern)
foreach (string result in regex.Split(input, 2)) 
{
    printfn "'{0}'", result)
}
// Under .NET 1.0 and 1.1, the method returns an array of
// 2 elements, as follows:
//    '07'
//    '14/2007'
//
// Under .NET 2.0 and later, the method returns an array of
// 3 elements, as follows:
//    '07'
//    '/'
//    '14/2007' 

// https://msdn.microsoft.com/en-us/library/8yttk7sy(v=vs.110).aspx

string input = "plum--pear"
string pattern = "-"            // Split on hyphens

string[] substrings = Regex.Split(input, pattern)
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}
// The method displays the following output:
//    'plum'
//    ''
//    'pear'   

string pattern = @"\d+"
string input = "123ABCDE456FGHIJKL789MNOPQ012"
string[] result = Regex.Split(input, pattern)
for (int ctr = 0 ctr < result.Length ctr++) {
    Console.Write("'{0}'", result[ctr])
    if (ctr < result.Length - 1) 
    Console.Write(", ")
}
printfn )

// The example displays the following output:
//       '', 'ABCDE', 'FGHIJKL', 'MNOPQ', ''

string input = "plum-pear"
string pattern = "(-)"

string[] substrings = Regex.Split(input, pattern)    // Split on hyphens
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}
// The example displays the following output:
//    'plum'
//    '-'
//    'pear'  

string input = @"07/14/2007"   
string pattern = @"(-)|(/)"

foreach (string result in Regex.Split(input, pattern)) 
{
    printfn "'{0}'", result)
}
// In .NET 1.0 and 1.1, the method returns an array of
// 3 elements, as follows:
//    '07'
//    '14'
//    '2007'
//
// In .NET 2.0 and later, the method returns an array of
// 5 elements, as follows:
//    '07'
//    '/'
//    '14'
//    '/'
//    '2007' 

string input = "characters"
string[] substrings = Regex.Split(input, "")
Console.Write("{")
for(int ctr = 0 ctr < substrings.Length ctr++)
{
    Console.Write("'{0}'", substrings[ctr])
    if (ctr < substrings.Length - 1)
    Console.Write(", ")
}
printfn "}")

// The example produces the following output:   
//    {'', 'c', 'h', 'a', 'r', 'a', 'c', 't', 'e', 'r', 's', ''}

// https://msdn.microsoft.com/en-us/library/byy2946e(v=vs.110).aspx

string pattern = "[a-z]+"
string input = "Abc1234Def5678Ghi9012Jklm"
string[] result = Regex.Split(input, pattern, 
                            RegexOptions.IgnoreCase)
for (int ctr = 0 ctr < result.Length ctr++) {
    Console.Write("'{0}'", result[ctr])
    if (ctr < result.Length - 1) 
    Console.Write(", ")
}
printfn )

// The example displays the following output:
//       '', '1234', '5678', '9012', ''

string input = "plum-pear"
string pattern = "(-)"

string[] substrings = Regex.Split(input, pattern)    // Split on hyphens
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}
// The example displays the following output:
//    'plum'
//    '-'
//    'pear' 

string input = @"07/14/2007"   
string pattern = @"(-)|(/)"

foreach (string result in Regex.Split(input, pattern)) 
{
    printfn "'{0}'", result)
}
// In .NET 1.0 and 1.1, the method returns an array of
// 3 elements, as follows:
//    '07'
//    '14'
//    '2007'
//
// In .NET 2.0 and later, the method returns an array of
// 5 elements, as follows:
//    '07'
//    '/'
//    '14'
//    '/'
//    '2007' 

// https://msdn.microsoft.com/en-us/library/hh160264(v=vs.110).aspx

string pattern = "[a-z]+"
string input = "Abc1234Def5678Ghi9012Jklm"
string[] result = Regex.Split(input, pattern, 
                            RegexOptions.IgnoreCase,
                            TimeSpan.FromMilliseconds(500))
for (int ctr = 0 ctr < result.Length ctr++) {
    Console.Write("'{0}'", result[ctr])
    if (ctr < result.Length - 1) 
    Console.Write(", ")
}
printfn )

// The example displays the following output:
//       '', '1234', '5678', '9012', ''

string input = "plum-pear"
string pattern = "(-)"

string[] substrings = Regex.Split(input, pattern)    // Split on hyphens
foreach (string match in substrings)
{
    printfn "'{0}'", match)
}

// The example displays the following output:
//    'plum'
//    '-'
//    'pear'    

string input = @"07/14/2007"   
string pattern = @"(-)|(/)"

foreach (string result in Regex.Split(input, pattern)) 
{
    printfn "'{0}'", result)
}

// In .NET 1.0 and 1.1, the method returns an array of
// 3 elements, as follows:
//    '07'
//    '14'
//    '2007'
//
// In .NET 2.0 and later, the method returns an array of
// 5 elements, as follows:
//    '07'
//    '/'
//    '14'
//    '/'
//    '2007' 