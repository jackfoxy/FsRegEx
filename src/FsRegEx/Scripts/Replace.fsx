// https://msdn.microsoft.com/en-us/library/cft8645c(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions
open System

let capText (m : Match) =
    // Get the matched string.
    let x = m.Value
    // If the first char is lower case...
    if (Char.IsLower(x.[0])) then
        // Capitalize it.
        (Char.ToUpper(x.[0]).ToString()) + (x.Substring(1, x.Length - 1))
    else
        x

let text = "four score and seven years ago"

printfn "text=[%s]" text

let rx = new Regex(@"\w+")

let result = rx.Replace(text, new MatchEvaluator(capText))

printfn "result=[%s]" result

// The example displays the following output:
//       text=[four score and seven years ago]
//       result=[Four Score And Seven Years Ago]

printfn "result=[%s]"
    (text
     |> FsRegEx.replaceByMatch @"\w+" (new MatchEvaluator(capText)) )


// https://msdn.microsoft.com/en-us/library/e47f3dkc(v=vs.110).aspx

public static void Main()
{
    string input = "deceive relieve achieve belief fierce receive"
    string pattern = @"\w*(ie|ei)\w*"
    Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase)
    printfn "Original string: " + input)

    string result = rgx.Replace(input, new MatchEvaluator(Example.ReverseLetter), 
                                input.Split(' ').Length / 2)
    printfn "Returned string: " + result)
}

static string ReverseLetter(Match match)
{
    return Regex.Replace(match.Value, "([ie])([ie])", "$2$1", 
                        RegexOptions.IgnoreCase)            
}
// The example displays the following output:
//    Original string: deceive relieve achieve belief fierce receive
//    Returned string: decieve releive acheive belief fierce receive

// https://msdn.microsoft.com/en-us/library/ms149474(v=vs.110).aspx
// (no code sample)

// https://msdn.microsoft.com/en-us/library/xwewhkd1(v=vs.110).aspx

string input = "This is   text with   far  too   much   " + 
                "whitespace."
string pattern = "\\s+"
string replacement = " "
Regex rgx = new Regex(pattern)
string result = rgx.Replace(input, replacement)

printfn "Original String: {0}", input)
printfn "Replacement String: {0}", result)   

// The example displays the following output:
//       Original String: This is   text with   far  too   much   whitespace.
//       Replacement String: This is text with far too much whitespace.

// https://msdn.microsoft.com/en-us/library/haekbhys(v=vs.110).aspx

string str = "aabccdeefgghiijkklmm"
string pattern = "(\\w)\\1" 
string replacement = "$1" 
Regex rgx = new Regex(pattern)

string result = rgx.Replace(str, replacement, 5)
printfn "Original String:    '{0}'", str)
printfn "Replacement String: '{0}'", result) 

// The example displays the following output:
//       Original String:    'aabccdeefgghiijkklmm'
//       Replacement String: 'abcdefghijkklmm'

// https://msdn.microsoft.com/en-us/library/3fky5t5f(v=vs.110).aspx

string input = "Instantiating a New Type\n" +
                "Generally, there are two ways that an\n" + 
                "instance of a class or structure can\n" +
                "be instantiated. "
string pattern = "^.*$"
string replacement = "\n$&"
Regex rgx = new Regex(pattern, RegexOptions.Multiline)
string result = String.Empty 

Match match = rgx.Match(input)
// Double space all but the first line.
if (match.Success) 
    result = rgx.Replace(input, replacement, -1, match.Index + match.Length + 1)

printfn result)  

// The example displays the following output:
//       Instantiating a New Type
//       
//       Generally, there are two ways that an
//       
//       instance of a class or structure can
//       
//       be instntiated.

// https://msdn.microsoft.com/en-us/library/ht1sxswy(v=vs.110).aspx

public static void Main()
{
    string words = "letter alphabetical missing lack release " + 
                    "penchant slack acryllic laundry cease"
    string pattern = @"\w+"                            
    MatchEvaluator evaluator = new MatchEvaluator(WordScrambler)
    printfn "Original words:")
    printfn words)
    printfn )
    printfn "Scrambled words:")
    printfn Regex.Replace(words, pattern, evaluator))      
}

public static string WordScrambler(Match match)
{
    int arraySize = match.Value.Length
    // Define two arrays equal to the number of letters in the match.
    double[] keys = new double[arraySize]
    char[] letters = new char[arraySize]

    // Instantiate random number generator'
    Random rnd = new Random()

    for (int ctr = 0 ctr < match.Value.Length ctr++)
    {
        // Populate the array of keys with random numbers.
        keys[ctr] = rnd.NextDouble()
        // Assign letter to array of letters.
        letters[ctr] = match.Value[ctr]
    }         
    Array.Sort(keys, letters, 0, arraySize, Comparer.Default)      
    return new String(letters)
}

// The example displays output similar to the following:
//    Original words:
//    letter alphabetical missing lack release penchant slack acryllic laundry cease
//    
//    Scrambled words:
//    elrtte iaeabatlpchl igmnssi lcka aerslee hnpatnce ksacl lialcryc dylruna ecase

// https://msdn.microsoft.com/en-us/library/ms149475(v=vs.110).aspx

public static void Main()
{
    string words = "letter alphabetical missing lack release " + 
                    "penchant slack acryllic laundry cease"
    string pattern = @"\w+  # Matches all the characters in a word."                            
    MatchEvaluator evaluator = new MatchEvaluator(WordScrambler)
    printfn "Original words:")
    printfn words)
    printfn )
    printfn "Scrambled words:")
    printfn Regex.Replace(words, pattern, evaluator, 
                                    RegexOptions.IgnorePatternWhitespace))      
}

public static string WordScrambler(Match match)
{
    int arraySize = match.Value.Length
    // Define two arrays equal to the number of letters in the match.
    double[] keys = new double[arraySize]
    char[] letters = new char[arraySize]

    // Instantiate random number generator'
    Random rnd = new Random()

    for (int ctr = 0 ctr < match.Value.Length ctr++)
    {
        // Populate the array of keys with random numbers.
        keys[ctr] = rnd.NextDouble()
        // Assign letter to array of letters.
        letters[ctr] = match.Value[ctr]
    }         
    Array.Sort(keys, letters, 0, arraySize, Comparer.Default)      
    return new String(letters)
}
// The example displays output similar to the following:
//    Original words:
//    letter alphabetical missing lack release penchant slack acryllic laundry cease
//    
//    Scrambled words:
//    etlert liahepalbcat imsgsni alkc ereelsa epcnnaht lscak cayirllc alnyurd ecsae

// https://msdn.microsoft.com/en-us/library/hh194609(v=vs.110).aspx

public static void Main()
{
    string words = "letter alphabetical missing lack release " + 
                    "penchant slack acryllic laundry cease"
    string pattern = @"\w+  # Matches all the characters in a word."                            
    MatchEvaluator evaluator = new MatchEvaluator(WordScrambler)
    printfn "Original words:")
    printfn words)
    printfn )
    try {
        printfn "Scrambled words:")
        printfn Regex.Replace(words, pattern, evaluator, 
                                        RegexOptions.IgnorePatternWhitespace,
                                        TimeSpan.FromSeconds(.25)))      
    }
    catch (RegexMatchTimeoutException) {
        printfn "Word Scramble operation timed out.")
        printfn "Returned words:")
    }
}

public static string WordScrambler(Match match)
{
    int arraySize = match.Value.Length
    // Define two arrays equal to the number of letters in the match.
    double[] keys = new double[arraySize]
    char[] letters = new char[arraySize]

    // Instantiate random number generator'
    Random rnd = new Random()

    for (int ctr = 0 ctr < match.Value.Length ctr++)
    {
        // Populate the array of keys with random numbers.
        keys[ctr] = rnd.NextDouble()
        // Assign letter to array of letters.
        letters[ctr] = match.Value[ctr]
    }         
    Array.Sort(keys, letters, 0, arraySize, Comparer.Default)      
    return new String(letters)
}
// The example displays output similar to the following:
//    Original words:
//    letter alphabetical missing lack release penchant slack acryllic laundry cease
//    
//    Scrambled words:
//    etlert liahepalbcat imsgsni alkc ereelsa epcnnaht lscak cayirllc alnyurd ecsae

// https://msdn.microsoft.com/en-us/library/e7f5w83z(v=vs.110).aspx

string input = "This is   text with   far  too   much   " + 
                "whitespace."
string pattern = "\\s+"
string replacement = " "
string result = Regex.Replace(input, pattern, replacement)

printfn "Original String: {0}", input)
printfn "Replacement String: {0}", result)  

// The example displays the following output:
//       Original String: This is   text with   far  too   much   whitespace.
//       Replacement String: This is text with far too much whitespace.

// Get drives available on local computer and form into a single character expression.
string[] drives = Environment.GetLogicalDrives()
string driveNames = String.Empty
foreach (string drive in drives)
    driveNames += drive.Substring(0,1)
// Create regular expression pattern dynamically based on local machine information.
string pattern = @"\\\\(?i:" + Environment.MachineName + @")(?:\.\w+)*\\((?i:[" + driveNames + @"]))\$"

string replacement = "$1:"
string[] uncPaths = { @"\\MyMachine.domain1.mycompany.com\C$\ThingsToDo.txt", 
                    @"\\MyMachine\c$\ThingsToDo.txt", 
                    @"\\MyMachine\d$\documents\mydocument.docx" } 

foreach (string uncPath in uncPaths)
{
    printfn "Input string: " + uncPath)
    printfn "Returned string: " + Regex.Replace(uncPath, pattern, replacement))
    printfn )
}
// The example displays the following output if run on a machine whose name is
// MyMachine:
//    Input string: \\MyMachine.domain1.mycompany.com\C$\ThingsToTo.txt
//    Returned string: C:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\c$\ThingsToDo.txt
//    Returned string: c:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\d$\documents\mydocument.docx
//    Returned string: d:\documents\mydocument.docx

// https://msdn.microsoft.com/en-us/library/taz3ak2f(v=vs.110).aspx

// Get drives available on local computer and form into a single character expression.
string[] drives = Environment.GetLogicalDrives()
string driveNames = String.Empty
foreach (string drive in drives)
    driveNames += drive.Substring(0,1)
// Create regular expression pattern dynamically based on local machine information.
string pattern = @"\\\\" + Environment.MachineName + @"(?:\.\w+)*\\([" + driveNames + @"])\$"

string replacement = "$1:"
string[] uncPaths = { @"\\MyMachine.domain1.mycompany.com\C$\ThingsToDo.txt", 
                    @"\\MyMachine\c$\ThingsToDo.txt", 
                    @"\\MyMachine\d$\documents\mydocument.docx" } 

foreach (string uncPath in uncPaths)
{
    printfn "Input string: " + uncPath)
    printfn "Returned string: " + Regex.Replace(uncPath, pattern, replacement, RegexOptions.IgnoreCase))
    printfn )
}
// The example displays the following output if run on a machine whose name is
// MyMachine:
//    Input string: \\MyMachine.domain1.mycompany.com\C$\ThingsToTo.txt
//    Returned string: C:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\c$\ThingsToDo.txt
//    Returned string: c:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\d$\documents\mydocument.docx
//    Returned string: d:\documents\mydocument.docx

// https://msdn.microsoft.com/en-us/library/hh194617(v=vs.110).aspx

// Get drives available on local computer and form into a single character expression.
string[] drives = Environment.GetLogicalDrives()
string driveNames = String.Empty
foreach (string drive in drives)
    driveNames += drive.Substring(0,1)
// Create regular expression pattern dynamically based on local machine information.
string pattern = @"\\\\" + Environment.MachineName + @"(?:\.\w+)*\\([" + driveNames + @"])\$"

string replacement = "$1:"
string[] uncPaths = { @"\\MyMachine.domain1.mycompany.com\C$\ThingsToDo.txt", 
                    @"\\MyMachine\c$\ThingsToDo.txt", 
                    @"\\MyMachine\d$\documents\mydocument.docx" } 

foreach (string uncPath in uncPaths)
{
    printfn "Input string: " + uncPath)
    string localPath = null
    try {
    localPath = Regex.Replace(uncPath, pattern, replacement, 
                                RegexOptions.IgnoreCase,
                                TimeSpan.FromSeconds(0.5))
    printfn "Returned string: " + localPath)
    }
    catch (RegexMatchTimeoutException) {
    printfn "The replace operation timed out.")
    printfn "Returned string: " + localPath)
    if (uncPath.Equals(localPath)) 
        printfn "Equal to original path.")
    else
        printfn "Original string: " + uncPath)
    }
    printfn )
}
// The example displays the following output if run on a machine whose name is
// MyMachine:
//    Input string: \\MyMachine.domain1.mycompany.com\C$\ThingsToTo.txt
//    Returned string: C:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\c$\ThingsToDo.txt
//    Returned string: c:\ThingsToDo.txt
//    
//    Input string: \\MyMachine\d$\documents\mydocument.docx
//    Returned string: d:\documents\mydocument.docx

