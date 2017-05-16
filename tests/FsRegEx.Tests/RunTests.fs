namespace FsRegEx.Tests

open Expecto

module RunTests =

    [<EntryPoint>]
    let main args =

        Tests.runTestsWithArgs defaultConfig args FsRegEx.testMatches |> ignore
        Tests.runTestsWithArgs defaultConfig args FsRegEx.testReplace |> ignore
        Tests.runTestsWithArgs defaultConfig args FsRegEx.testSplit |> ignore
        Tests.runTestsWithArgs defaultConfig args FsRegEx.testCapture |> ignore
        Tests.runTestsWithArgs defaultConfig args FsRegEx.testGroup |> ignore
        Tests.runTestsWithArgs defaultConfig args FsRegEx.testIsMatch |> ignore

        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testAdd |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testCapture |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testEndOfLine |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testMatch |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testMaybe |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testMisc |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testMultiple |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testOr' |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testRange |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testRegexOptions |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testRepeatPrevious |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testSomethingBut |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testSomething |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testStartOfLine |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testThen' |> ignore
        Tests.runTestsWithArgs defaultConfig args VerbalExpressions.testTutorial |> ignore

        0

