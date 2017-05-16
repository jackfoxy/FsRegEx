namespace FsRegEx.Tests

open Expecto
open FsRegEx
open System.Text.RegularExpressions

module FsRegEx =

    let carText = "One car red car blue car"
    let carRegExp = @"(\w+)\s+(car)"
    let eval = new MatchEvaluator(fun _ -> "blah")

    let splitText = "123ABCDE456FGHIJKL789MNOPQ012"
    let splitRegExp = @"\d+"

    let groupNumberRegExp = @"COD(?<GroupNumber>[0-9]{3})END"
    let groupNumberName = "GroupNumber"

    let partNumberRegExp = @"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$"

    [<Tests>]
    let testMatches =

        testList "FsRegEx.Match" [
            testCase "firstMatch" <| fun () ->
                let fsMatch = firstMatch carRegExp carText
                Expect.isTrue (fsMatch.Value = "One car") "Expected True"

            testCase "matchAt" <| fun () ->
                let fsMatch = matchAt carRegExp 3 carText
                Expect.isTrue (fsMatch.Value = "red car") "Expected True"

            testCase "matchAtFor" <| fun () ->
                let fsMatch = matchAtFor carRegExp 3 15 carText
                Expect.isTrue (fsMatch.Value = "red car") "Expected True"

            testCase "matches" <| fun () ->
                let value =
                    matches carRegExp carText
                    |> Array.map (fun m -> m.Value)
                Expect.isTrue (value = [|"One car"; "red car"; "blue car"|]) "Expected True"

            testCase "matchesAt" <| fun () ->
                let value =
                    matchesAt carRegExp 3 carText
                    |> Array.map (fun m -> m.Value)
                Expect.isTrue (value = [|"red car"; "blue car"|]) "Expected True"
        ]

    [<Tests>]
    let testReplace =
        testList "FsRegEx.Replace" [
            testCase "" <| fun () ->
                let value = replace carRegExp "blah" carText
                Expect.isTrue (value = "blah blah blah") "Expected True"

            testCase "replaceByMatch" <| fun () ->
                let value = replaceByMatch carRegExp eval carText
                Expect.isTrue (value = "blah blah blah") "Expected True"

            testCase "replaceMaxTimes" <| fun () ->
                let value = replaceMaxTimes carRegExp "blah" 2 carText
                Expect.isTrue (value = "blah blah blue car") "Expected True"

            testCase "replaceByMatchMaxTimes" <| fun () ->
                let value = replaceByMatchMaxTimes carRegExp eval 2 carText
                Expect.isTrue (value = "blah blah blue car") "Expected True"

            testCase "replaceMaxTimesStartAt" <| fun () ->
                let value = replaceMaxTimesStartAt carRegExp "blah" 1 3 carText
                Expect.isTrue (value = "One car blah blue car") "Expected True"

            testCase "replaceByMatchMaxTimesStartAt" <| fun () ->
                let value = replaceByMatchMaxTimesStartAt carRegExp eval 1 3 carText
                Expect.isTrue (value = "One car blah blue car") "Expected True"
        ]

    [<Tests>]
    let testSplit =
        testList "FsRegEx.Split" [
            testCase "split" <| fun () ->
                let value = split splitRegExp splitText
                Expect.isTrue (value = [|""; "ABCDE"; "FGHIJKL"; "MNOPQ"; ""|]) "Expected True"

            testCase "splitMaxTimes" <| fun () ->
                let value = splitMaxTimes splitRegExp 3 splitText
                Expect.isTrue (value = [|""; "ABCDE"; "FGHIJKL789MNOPQ012"|]) "Expected True"

            testCase "splitMaxTimesStartAt" <| fun () ->
                let value = splitMaxTimesStartAt splitRegExp 3 5 splitText
                Expect.isTrue (value = [|"123ABCDE"; "FGHIJKL"; "MNOPQ012"|]) "Expected True"
        ]

    [<Tests>]
    let testCapture =
        testList "FsRegEx.Capture" [
            testCase "capture" <| fun () ->
                let value = capture groupNumberRegExp groupNumberName "COD123END"
                Expect.isTrue (value = "123") "Expected True"

            testCase "captureGroup" <| fun () ->
                let group = captureGroup groupNumberRegExp groupNumberName "COD123END"
                Expect.isTrue (group.Value = "123") "Expected True"
        ]

    [<Tests>]
    let testGroup =
        testList "FsRegEx.Group" [
            testCase "groupNames" <| fun () ->
                let value = groupNames groupNumberRegExp
                Expect.isTrue (value = [|"0"; groupNumberName|]) "Expected True"

            testCase "groupNumbers" <| fun () ->
                let value = groupNumbers groupNumberRegExp
                Expect.isTrue (value = [|0; 1|]) "Expected True"

            testCase "fsGroupName" <| fun () ->
                let pattern = @"\b(?<FirstWord>\w+)\s?((\w+)\s)*(?<LastWord>\w+)?(?<Punctuation>\p{Po})"
                let input = "The cow jumped over the moon."

                let m = FsRegEx.firstMatch pattern input

                let names =
                    m.Groups()
                    |> Array.map (fun x -> x.Name)
                Expect.isTrue (names = [|"0"; "1"; "2"; "FirstWord"; "LastWord"; "Punctuation"|]) "Expected True"
        ]

    [<Tests>]
    let testIsMatch =
        testList "FsRegEx.IsMatch" [
            testCase "isMatch" <| fun () ->
                Expect.isTrue (isMatch partNumberRegExp "1298-673-4192") "Expected True"

            testCase "isMatchAt" <| fun () ->
                let partNumber = "Part Number: 1298-673-4192"
                let start = partNumber.IndexOf(":")
                Expect.isTrue (isMatchAt @"[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$" start partNumber) "Expected True"
        ]
