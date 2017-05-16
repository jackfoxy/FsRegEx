namespace FsRegEx.Tests

open Expecto
open FsRegEx
open VerbalExpressions
open System
open System.Text.RegularExpressions

module VerbalExpressions =

    [<Tests>]
    let testAdd =
        testList "VerbalExpressions.Add " [
            testCase "special characters unescaped" <| fun () ->
                let value =
                    FsRegEx(RegexOptions.Multiline)
                    |> add "Line1\*+?"
                    |> isMatch "Line1\*+?Line2"
                Expect.isFalse value "Expected False"

            testCase "special characters escaped" <| fun () ->
                let value =
                    FsRegEx(RegexOptions.Multiline)
                    |> add "Line1\*\+\?"
                    |> isMatch "Line1*+?Line2"
                Expect.isTrue value "Expected True"

            testCase "Does Not Match GoogleCom With Escaped Dot" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Multiline)
                    |> add "\.com"
                    |> isMatch "http://www.googlecom/"
                Expect.isFalse value "Expected False"

            testCase "Does Not Match GoogleCom With Dot" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Multiline)
                    |> add ".com"
                    |> isMatch "http://www.googlecom/"
                Expect.isTrue value "Expected True"
        ]

    [<Tests>]
    let testCapture =
        let duplicateIdentifier = 
            FsRegEx()
            |> beginCapture
            |> word 
            |> endCapture
            |> whiteSpace
            |> beginCapture
            |> backReference 1
            |> endCapture

        let groupName =  "GroupNumber"

        let v =
            FsRegEx()
            |> add "COD"
            |> beginCaptureNamed groupName
            |> any "0-9"
            |> repeatPrevious 3
            |> endCapture
            |> add "END"

        testList "VerbalExpressions.Capture" [
            testCase "begin and end" <| fun () ->
                let value = 
                    FsRegEx()
                    |> beginCapture
                    |> add "com" 
                    |> or' "org"
                    |> endCapture
                    |> isMatch "((com)|(org))"
                Expect.isTrue value "Expected True"

            testCase "Duplicates Identifier string value" <| fun () ->
                let value = 
                    duplicateIdentifier
                    |> toString 
                Expect.isTrue (value = @"(\w+)\s(\1)") "Expected True"

            testCase "Duplicates Identifier Does Match" <| fun () ->
                Expect.isTrue (duplicateIdentifier.IsMatch "He said that that was the the correct answer." ) "Expected True"

            testCase "Duplicates Identifier Does Not Match" <| fun () ->
                Expect.isFalse (duplicateIdentifier.IsMatch "He said that was the correct answer." ) "Expected False"

            testCase "Group Names" <| fun () ->
                let value = 
                    duplicateIdentifier
                    |> groupNames
                Expect.isTrue (value = [|"0"; "1"; "2"|]) "Expected True"

            testCase "Group Numbers" <| fun () ->
                let value = 
                    duplicateIdentifier
                    |> groupNumbers
                Expect.isTrue (value = [|0; 1; 2|]) "Expected True"

            testCase "Group Name ToString" <| fun () ->
                let value = 
                    v
                    |> toString
                Expect.isTrue (value = @"COD(?<GroupNumber>[0-9]{3})END") "Expected True"

            testCase "Group Name capture" <| fun () ->
                let value = 
                    v
                    |> capture "COD123END" groupName
                Expect.isTrue (value = "123") "Expected True"

            testCase "Group Names" <| fun () ->
                let value = 
                    v
                    |> groupNames
                Expect.isTrue (value = [|"0"; "GroupNumber"|]) "Expected True"

            testCase "Nmaed Backreference" <| fun () ->
                let groupName = "char"
                let value = 
                    FsRegEx()
                    |> add @"(?<char>\w)"
                    |> namedBackReference "char"
                    |> capture "trellis llama webbing dresser swagger" groupName
                Expect.isTrue (value = "l") "Expected True"

            testCase "Match has 1 capture" <| fun () ->
                let value = 
                    FsRegEx()
                    |> word
                    |> matches "three words here"
                Expect.isTrue (value.[0].Captures().Length = 1) "Expected True"

            testCase "Match has 1 group" <| fun () ->
                let value = 
                    FsRegEx()
                    |> word
                    |> matches "three words here"
                Expect.isTrue (value.[0].Groups().Length = 1) "Expected True"

            testCase "Group captures" <| fun () ->
                let testString = "This is a sentence. This is another sentence."
                let m = 
                    FsRegEx()
                    |> add @"\b"
                    |> beginCapture
                    |> word
                    |> whiteSpace
                    |> add "*"
                    |> endCapture
                    |> add @"+\."
                    |> matches testString
                let g =
                    m.[0].Groups().[1]

                let testStringBegins = 
                    ("", g.Captures())
                    ||> Array.fold (fun s t ->
                        if s.Length = 0 then t.Value
                        else (s + t.Value)) 

                Expect.isTrue (testString.StartsWith testStringBegins) "Expected True"
        ]

    [<Tests>]
    let testEndOfLine =
        testList "VerbalExpressions.EndOfLine" [
            testCase "Match Dot Com In End" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add ".com" 
                    |> endOfLine
                    |> isMatch "www.google.com"
                Expect.isTrue value "Expected True"

            testCase "Does Not Match Forward Slash In End" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add ".com" 
                    |> endOfLine
                    |> isMatch "www.google.com/"
                Expect.isFalse value "Expected False"
        ]

    [<Tests>]
    let testMatch =
        testList "VerbalExpressions.Match" [
            testCase "Returns Expected Number Of Matches" <| fun () ->
                let value = 
                    FsRegEx()
                    |> word
                    |> matches "three words here"
                Expect.isTrue (value.Length = 3) "Expected True"

            testCase "No Matches returned" <| fun () ->
                let value = 
                    FsRegEx()
                    |> word
                    |> matches "$#% )(*! {}"
                Expect.isTrue (value.Length = 0) "Expected True"
        ]

    [<Tests>]
    let testMaybe =
        let maybeSSL =
            FsRegEx()
            |> find "http"
            |> maybe "s"
            |> anythingBut " "
            |> then' "://"

        testList "VerbalExpressions.Maybe" [
            testCase "https present" <| fun () ->
                let value = 
                    maybeSSL
                    |> isMatch "https://www.google.com"
                Expect.isTrue value "Expected True"

            testCase "http present" <| fun () ->
                let value = 
                    maybeSSL
                    |> isMatch "http://www.google.com"
                Expect.isTrue value "Expected True"
        ]

    [<Tests>]
    let testMisc =
        testList "VerbalExpressions.Misc" [
            testCase "valid url" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> then' "http"
                    |> maybe "s"
                    |> then' "://"
                    |> maybe "www."
                    |> anythingBut " "
                    |> endOfLine
                    |> isMatch "https://www.google.com"
                Expect.isTrue value "Expected True"

            testCase "does match anything" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> anything
                    |> isMatch "'!@#$%¨&*()__+{}'"
                Expect.isTrue value "Expected True"

            testCase "lineBreak" <| fun () ->
                let value = 
                    FsRegEx()
                    |> lineBreak
                    |> isMatch (sprintf "testing with %s line break" Environment.NewLine)
                Expect.isTrue value "Expected True"
                
            testCase "br" <| fun () ->
                let value = 
                    FsRegEx()
                    |> br
                    |> isMatch (sprintf "testing with %s line break" Environment.NewLine)
                Expect.isTrue value "Expected True"

            testCase "tab" <| fun () ->
                let value = 
                    FsRegEx()
                    |> tab
                    |> isMatch (sprintf "testing with %s tab" "\t")
                Expect.isTrue value "Expected True"
        ]

    [<Tests>]
    let testMultiple =
        let v =
            FsRegEx()
            |> add "y"
            |> multiple "aho"
            |> add "u"

        testList "VerbalExpressions.Multiple" [
            testCase "Match One Or Multiple Values Given ToString" <| fun () ->
                let value = 
                    v
                    |> toString
                Expect.isTrue (value = "y(aho)+u") "Expected True"

            testCase "Match One Or Multiple Values Given" <| fun () ->
                let value = 
                    v
                    |> isMatch "testesting 123 yahoahoahou another test"
                Expect.isTrue value "Expected True"
        ]

    [<Tests>]
    let testOr' =
        let v =
            FsRegEx()
            |> add "com"
            |> or' "org"

        let vAll =
            FsRegEx()
            |> or' "com"
            |> or' "org"

        testList "VerbalExpressions.Or'" [
            testCase "Matches Com And Org ToString" <| fun () ->
                let value = 
                    v
                    |> toString
                Expect.isTrue (value = "com|org") "Expected True"

            testCase "Matches Com And Org" <| fun () ->
                let value1 = 
                    v
                    |> isMatch "org"

                let value2 =
                    v
                    |> isMatch "com"
                Expect.isTrue (value1 && value2) "Expected True"

            testCase "Matches Com And Org all or ToString" <| fun () ->
                let value = 
                    vAll
                    |> toString
                Expect.isTrue (value = "com|org") "Expected True"

            testCase "Matches Com And Org all or" <| fun () ->
                let value1 = 
                    vAll
                    |> isMatch "org"
                let value2 = 
                    vAll
                    |> isMatch "com"
                Expect.isTrue (value1 && value2) "Expected True"

            testCase "fsRegEx  email or url" <| fun () ->
                let value = 
                    CommonFsRegEx.Email
                    |> fsRegExOrFsRegEx RegexOptions.None CommonFsRegEx.Url
                Expect.isTrue ((value.IsMatch "test@github.com") && (value.IsMatch "http://www.google.com")) "Expected True"

            testCase "fsRegEx  or regular expression" <| fun () ->
                let value = 
                    CommonFsRegEx.Email
                    |> or' "org"
                Expect.isTrue ((value.IsMatch "test@github.com") && (value.IsMatch "org")) "Expected True"
        ]

    [<Tests>]
    let testRange =
        let vOdd =
            FsRegEx()
            |> range [1; 6; 7]

        let vEven =
            FsRegEx()
            |> range [|"1"; "6"; "a"; "c"|]

        testList "VerbalExpressions.Range" [
            testCase "Odd Number Of Items Append LastElement With Or Clause toString" <| fun () ->
                let value = 
                    vOdd
                    |> toString
                Expect.isTrue (value = "[1-6]|7") "Expected True"

            testCase "Odd Number Of Items Append LastElement With Or Clause" <| fun () ->
                let value = 
                    vOdd
                    |> isMatch "abcd7sdadqascdaswde"
                Expect.isTrue value "Expected True"

            testCase "Even Number Of Items toString" <| fun () ->
                let value = 
                    vEven
                    |> toString
                Expect.isTrue (value = "[1-6][a-c]") "Expected True"

            testCase "Even Number Of Items isMatch" <| fun () ->
                let value = 
                    vEven
                    |> isMatch "2b"
                Expect.isTrue value "Expected True"
    
            testCase "Has Only One Value  Throw Exception" <| fun () ->
                let value = 
                    try
                        FsRegEx() |> range [1] |> ignore
                        ""
                    with e ->
                        e.Message 
                Expect.isTrue (value = "one-element range\r\nParameter name: range") "Expected True"
        ]

    [<Tests>]
    let testRegexOptions =
        let testSingleLineUpperCase v =
            v
            |> add "First string"
            |> anything
            |> add "Second string"
            |> isMatch ("FIRST STRING" + Environment.NewLine + "SECOND STRING")

        let vSingleLine = FsRegEx(RegexOptions.Singleline)
        let vSingleLineIgnoreCase = new FsRegEx(RegexOptions.Singleline ||| RegexOptions.IgnoreCase)

        testList "VerbalExpressions.RegexOptions" [
            testCase "Multiline" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Multiline)
                    |> add "Line1"
                    |> endOfLine
                    |> isMatch "Line1\nLine2"
                Expect.isTrue value "Expected True"

            testCase "Multiline None" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "Line1"
                    |> endOfLine
                    |> isMatch @"Line1\nLine2"
                Expect.isFalse value "Expected False"

            testCase "IgnoreCase" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.IgnoreCase)
                    |> add "teststring"
                    |> endOfLine
                    |> isMatch "TESTSTRING"
                Expect.isTrue value "Expected True"

            testCase "IgnoreCase None" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "teststring"
                    |> endOfLine
                    |> isMatch "TESTSTRING"
                Expect.isFalse value "Expected False"

            testCase "Singleline" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Singleline)
                    |> add "First string"
                    |> anything
                    |> add "Second string"
                    |> isMatch ("First string" + Environment.NewLine + "Second string")
                Expect.isTrue value "Expected True"

            testCase "Singleline None" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "First string"
                    |> anything
                    |> add "Second string"
                    |> isMatch ("First string" + Environment.NewLine + "Second string")
                Expect.isFalse value "Expected False"

            testCase "Singleline IgnoreCase" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Singleline ||| RegexOptions.IgnoreCase)
                    |> testSingleLineUpperCase
                Expect.isTrue value "Expected True"

            testCase "Singleline IgnoreCase None" <| fun () ->
                let value = 
                    FsRegEx(RegexOptions.Singleline)
                    |> testSingleLineUpperCase
                Expect.isFalse value "Expected False"

            testCase "resetRegexOptions single line" <| fun () ->
                let value = 
                    vSingleLine
                    |> testSingleLineUpperCase
                Expect.isFalse value "Expected False"

            testCase "resetRegexOptions single line ignore case" <| fun () ->
                let value = 
                    (Some (RegexOptions.Singleline ||| RegexOptions.IgnoreCase), vSingleLine)
                    ||> resetRegexOptions 
                    |> testSingleLineUpperCase
                Expect.isTrue value "Expected True"

            testCase "identify options ignore case" <| fun () ->
                let value = 
                    vSingleLineIgnoreCase.RegexOptions.HasFlag(RegexOptions.IgnoreCase) 
                Expect.isTrue value "Expected True"

            testCase "identify options explicit capture" <| fun () ->
                let value = 
                    vSingleLineIgnoreCase.RegexOptions.HasFlag(RegexOptions.ExplicitCapture) 
                Expect.isFalse value "Expected False"
        ]

    [<Tests>]
    let testRepeatPrevious =
        testList "VerbalExpressions.RepeatPrevious" [
            testCase "toString" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "A"
                    |> repeatPrevious 3
                    |> toString
                Expect.isTrue (value = "A{3}") "Expected True"

            testCase "between 2 and 4" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "A"
                    |> repeatBetweenPrevious 3 4
                    |> toString
                Expect.isTrue (value = "A{3,4}") "Expected True"
        ]

    [<Tests>]
    let testSomethingBut =
        testList "VerbalExpressions.SomethingBut" [
            testCase "all characters in test" <| fun () ->
                let value = 
                    FsRegEx()
                    |> somethingBut "Test"
                    |> isMatch "Test"
                Expect.isFalse value "Expected False"

            testCase "a characters out of test" <| fun () ->
                let value = 
                    FsRegEx()
                    |> somethingBut "Test"
                    |> isMatch "TestA"
                Expect.isTrue value "Expected True"
        ]

    [<Tests>]
    let testSomething =
        testList "VerbalExpressions.Something" [
            testCase "something there" <| fun () ->
                let value = 
                    FsRegEx()
                    |> something
                    |> isMatch "Test string should not be empty"
                Expect.isTrue value "Expected True"

            testCase "nothing there" <| fun () ->
                let value = 
                    FsRegEx()
                    |> something
                    |> isMatch ""
                Expect.isFalse value "Expected False"
        ]

    [<Tests>]
    let testStartOfLine =
        testList "VerbalExpressions.StartOfLine" [
            testCase "line start" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> toString 
                Expect.isTrue (value = "^") "Expected True"

            testCase "Prefix At Beginning Of The Expression" <| fun () ->
                let value = 
                    FsRegEx()
                    |> add "test"
                    |> add "ing"
                    |> startOfLine
                    |> isMatch "testing1234"
                Expect.isTrue value "Expected True"

            testCase "match correct at start" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> then' "http"
                    |> maybe "www"
                    |> startOfLine
                    |> isMatch "http://xxx.test.xxx"
                Expect.isTrue value "Expected True"

            testCase "no match not at start" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> then' "http"
                    |> maybe "www"
                    |> startOfLine
                    |> isMatch "www.test.com"
                Expect.isFalse value "Expected False"
        ]

    [<Tests>]
    let testThen' =
        testList "VerbalExpressions.Then'" [
            testCase "Does Match" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> something
                    |> then' "github.com"
                    |> isMatch "test@github.com"
                Expect.isTrue value "Expected True"

            testCase "Does Not Match" <| fun () ->
                let value = 
                    FsRegEx()
                    |> startOfLine
                    |> then' "github.com"
                    |> isMatch "test@"
                Expect.isFalse value "Expected False"

            testCase "special characters" <| fun () ->
                let value = 
                    FsRegEx()
                    |> then' "*+?"
                    |> isMatch "Line1*+?Line2"
                Expect.isTrue value "Expected True"

            testCase "Does Not Match GoogleCom Without Dot" <| fun () ->
                let value = 
                    FsRegEx()
                    |> then' ".com"
                    |> isMatch "http://www.googlecom/"
                Expect.isFalse value "Expected False"
        ]

    [<Tests>]
    let testTutorial =
        testList "VerbalExpressions.Tutorial" [
            testCase "Tutorial 1" <| fun () ->
                let v =
                    CommonFsRegEx.Email
                    |> fsRegExOrFsRegEx RegexOptions.None CommonFsRegEx.Url

                let foundEmail =
                    v
                    |> VerbalExpressions.isMatch "test@github.com"

                let foundUrl =
                    v
                    |> VerbalExpressions.isMatch "http://www.google.com"
                Expect.isTrue (foundEmail && foundUrl) "Expected True"

            testCase "Tutorial 2" <| fun () ->
                let foundFromGithub =
                    FsRegEx()
                    |> startOfLine
                    |> something
                    |> then' "github.com"
                    |> endOfLine
                    |> isMatch "test@github.com"
                Expect.isTrue foundFromGithub "Expected True"

            testCase "Tutorial 3" <| fun () ->
                let foundSomethingSpecial =
                    FsRegEx()
                    |> startOfLine
                    |> something
                    |> then' "*+?"
                    |> anything
                    |> isMatch "blah blah blah*+?yackety yack"
                Expect.isTrue foundSomethingSpecial "Expected True"

            testCase "Tutorial 4" <| fun () ->
                let foundSpecialInMultiline =
                    FsRegEx()
                    |> add @"phrase1\*\+\?"
                    |> anything
                    |> isMatch @"phrase1*+?RestOfLine\n"
                Expect.isTrue foundSpecialInMultiline "Expected True"

            testCase "Tutorial 5" <| fun () ->
                let n =
                    FsRegEx()
                    |> word
                    |> matches "three words here"
                Expect.isTrue (n.Length = 3) "Expected True"
   
            testCase "Tutorial 6" <| fun () ->
                let betterFormat =
                    FsRegEx()
                    |> add "\s+"
                    |> or' "whitespace"
                    |> replace "This     is   text with   far  too   much   whitespace" " "
                Expect.isTrue (betterFormat = "This is text with far too much  ") "Expected True"

            testCase "Tutorial 7" <| fun () ->
                let groupName =  "GroupNumber"
 
                let value = 
                    FsRegEx()
                    |> add "COD"
                    |> beginCaptureNamed groupName
                    |> any "0-9"
                    |> repeatPrevious 3
                    |> endCapture
                    |> then' "END"
                    |> capture "COD123END" groupName
                Expect.isTrue (value = "123") "Expected True"

            testCase "Tutorial 8" <| fun () ->
                let value = 
                    FsRegEx()
                    |> beginCaptureNamed "upper"
                    |> unicodeCategory  Unicode.UnicodeGeneralCategory.LetterUppercase
                    |> add "+"
                    |> endCapture
                    |> capture "some mixed case WORDS" "upper" 
                Expect.isTrue (value = "WORDS") "Expected True"
        ]
