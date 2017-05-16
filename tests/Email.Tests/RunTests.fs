namespace Email.Tests

open Expecto

module RunTests =

    [<EntryPoint>]
    let main args =
        
        Tests.runTestsWithArgs defaultConfig args SimpleEmail.testSimpleEmail |> ignore
        Tests.runTestsWithArgs defaultConfig args ModerateEmail.testModerateEmail |> ignore
        Tests.runTestsWithArgs defaultConfig args ComplexEmail.testComplexEmail |> ignore
        Tests.runTestsWithArgs defaultConfig args OneAmp.testOneAmp |> ignore
        Tests.runTestsWithArgs defaultConfig args Ultimate.testUltimate |> ignore

        0 
