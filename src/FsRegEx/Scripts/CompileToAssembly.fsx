// https://msdn.microsoft.com/en-us/library/9ek5zak6(v=vs.110).aspx

#I __SOURCE_DIRECTORY__
#load "load-project-debug.fsx"

open System.Text.RegularExpressions

let compilationList = new List<RegexCompilationInfo>()

// Define regular expression to detect duplicate words
let expr = new RegexCompilationInfo(@"\b(?<word>\w+)\s+(\k<word>)\b", 
            RegexOptions.IgnoreCase || RegexOptions.CultureInvariant, 
            "DuplicatedString", 
            "Utilities.RegularExpressions", 
            true)
// Add info object to list of objects
compilationList.Add(expr)

// Define regular expression to validate format of email address
let expr = new RegexCompilationInfo(@"^(?("")(""[^""]+?""@)|(([0-9A-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9A-Z])@))" + 
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9A-Z][-\w]*[0-9A-Z]\.)+[A-Z]{2,6}))$", 
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, 
            "EmailAddress", 
            "Utilities.RegularExpressions", 
            true)
// Add info object to list of objects
compilationList.Add(expr)

// Generate assembly with compiled regular expressions
let compilationArray = new RegexCompilationInfo[compilationList.Count]
let assemName = new AssemblyName("RegexLib, Version=1.0.0.1001, Culture=neutral, PublicKeyToken=null")
compilationList.CopyTo(compilationArray) 
Regex.CompileToAssembly(compilationArray, assemName)      

// https://msdn.microsoft.com/en-us/library/ew6t47sw(v=vs.110).aspx

let compilationList = new List<RegexCompilationInfo>()

// Define regular expression to detect duplicate words
let expr = new RegexCompilationInfo(@"\b(?<word>\w+)\s+(\k<word>)\b", 
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, 
            "DuplicatedString", 
            "Utilities.RegularExpressions", 
            true)
// Add info object to list of objects
compilationList.Add(expr)

// Define regular expression to validate format of email address
let expr = new RegexCompilationInfo(@"^(?("")(""[^""]+?""@)|(([0-9A-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9A-Z])@))" + 
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9A-Z][-\w]*[0-9A-Z]\.)+[zA-Z]{2,6}))$", 
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, 
            "EmailAddress", 
            "Utilities.RegularExpressions", 
            true)
// Add info object to list of objects
compilationList.Add(expr)

// Apply AssemblyTitle attribute to the new assembly
//
// Define the parameter(s) of the AssemblyTitle attribute's constructor 
Type[] parameters = { typeof(string) }
// Define the assembly's title
object[] paramValues = { "General-purpose library of compiled regular expressions" }
// Get the ConstructorInfo object representing the attribute's constructor
ConstructorInfo ctor = typeof(System.Reflection.AssemblyTitleAttribute).GetConstructor(parameters)
// Create the CustomAttributeBuilder object array
CustomAttributeBuilder[] attBuilder = { new CustomAttributeBuilder(ctor, paramValues) } 

// Generate assembly with compiled regular expressions
let compilationArray = new RegexCompilationInfo[compilationList.Count]
AssemblyName assemName = new AssemblyName("RegexLib, Version=1.0.0.1001, Culture=neutral, PublicKeyToken=null")
compilationList.CopyTo(compilationArray) 
Regex.CompileToAssembly(compilationArray, assemName, attBuilder) 

// https://msdn.microsoft.com/en-us/library/5a9btak8(v=vs.110).aspx

// (no sample code)