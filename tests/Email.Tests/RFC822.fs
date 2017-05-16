namespace Email.Tests

open Expecto

//to do: @ is present, not start or end
        //      no illegal characters
        // https://tools.ietf.org/html/rfc2822
        // https://tools.ietf.org/html/rfc2822#section-3.2.4
        // https://tools.ietf.org/html/rfc2822#section-3.4.1
        // http://www.ex-parrot.com/pdw/Mail-RFC822-Address.html
        //
        // http://emailregex.com/
        // ( see also RFC 5322 )
        //  [a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?

        // http://stackoverflow.com/questions/297420/list-of-email-addresses-that-can-be-used-to-test-a-javascript-validation-script

//        let emailValid = new VerbEx("""[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?""")
//        match emailValid.IsMatch email with
//        | true -> Success ()
//        | false -> 
//            (caller, sprintf "%s is not a valid email address" email)
//            |> Failure

/// RFC822 tests from http://code.iamcal.com/php/rfc822/tests/
module RFC822 =

    [<Literal>]
    let Valid = true

    [<Literal>]
    let Invalid = false

    let test1 (f : string -> bool) =
        testCase "test1" <| fun () -> 
            Expect.isTrue ((f @"first.last@iana.org") = Valid) "Expcect Valid"

    let test2 (f : string -> bool) =
        testCase "test2" <| fun () -> 
            Expect.isTrue ((f @"1234567890123456789012345678901234567890123456789012345678901234@iana.org") = Valid) "Expcect Valid"

    let test3 (f : string -> bool) =
        testCase "test3" <| fun () -> 
            Expect.isTrue ((f @"first.last@sub.do,com") = Invalid) "Expcect Invalid"

    let test4 (f : string -> bool) =
    //"first\"last"@iana.org
        testCase "test4" <| fun () -> 
            Expect.isTrue ((f "\"first\\\"last\"@iana.org") = Valid) "Expcect Valid"

    let test5 (f : string -> bool) =
        testCase "test5" <| fun () -> 
            Expect.isTrue ((f @"first\@last@iana.org") = Invalid) "Expcect Invalid"

    let test6 (f : string -> bool) =
    //"first@last"@iana.org
        testCase "test6" <| fun () -> 
            Expect.isTrue ((f "\"first@last\"@iana.org") = Valid) "Expcect Valid"

    let test7 (f : string -> bool) =
    //"first\\last"@iana.org
        testCase "test7" <| fun () -> 
            Expect.isTrue ((f "\"first\\\\last\"@iana.org") = Valid) "Expcect Valid"

    let test8 (f : string -> bool) =
        testCase "test8" <| fun () -> 
            Expect.isTrue ((f @"x@x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x2") 
                = Valid) "Expcect Valid"

    let test9 (f : string -> bool) =
        testCase "test9" <| fun () -> 
            Expect.isTrue ((f @"1234567890123456789012345678901234567890123456789012345678@12345678901234567890123456789012345678901234567890123456789.12345678901234567890123456789012345678901234567890123456789.123456789012345678901234567890123456789012345678901234567890123.iana.org") 
                = Valid) "Expcect Valid"

    let test10 (f : string -> bool) =
        testCase "test10" <| fun () -> 
            Expect.isTrue ((f @"first.last@[12.34.56.78]") = Valid) "Expcect Valid"

    let test11 (f : string -> bool) =
        testCase "test11" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::12.34.56.78]") = Valid) "Expcect Valid"

    let test12 (f : string -> bool) =
        testCase "test12" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333::4444:12.34.56.78]") = Valid) "Expcect Valid"

    let test13 (f : string -> bool) =
        testCase "test13" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:12.34.56.78]") = Valid) "Expcect Valid"

    let test14 (f : string -> bool) =
        testCase "test14" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::1111:2222:3333:4444:5555:6666]") = Valid) "Expcect Valid"

    let test15 (f : string -> bool) =
        testCase "test15" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333::4444:5555:6666]") = Valid) "Expcect Valid"

    let test16 (f : string -> bool) =
        testCase "test16" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666::]") = Valid) "Expcect Valid"

    let test17 (f : string -> bool) =
        testCase "test17" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:7777:8888]") = Valid) "Expcect Valid"

    let test18 (f : string -> bool) =
        testCase "test18" <| fun () -> 
            Expect.isTrue ((f @"first.last@x23456789012345678901234567890123456789012345678901234567890123.iana.org") 
                = Valid) "Expcect Valid"

    let test19 (f : string -> bool) =
        testCase "test19" <| fun () -> 
            Expect.isTrue ((f @"first.last@3com.com") = Valid) "Expcect Valid"

    let test20 (f : string -> bool) =
        testCase "test20" <| fun () -> 
            Expect.isTrue ((f @"first.last@123.iana.org") = Valid) "Expcect Valid"

    let test21 (f : string -> bool) =
        testCase "test21" <| fun () -> 
            Expect.isTrue ((f @"123456789012345678901234567890123456789012345678901234567890@12345678901234567890123456789012345678901234567890123456789.12345678901234567890123456789012345678901234567890123456789.12345678901234567890123456789012345678901234567890123456789.12345.iana.org") 
                = Invalid) "Expcect Invalid"

    let test22 (f : string -> bool) =
        testCase "test22" <| fun () -> 
            Expect.isTrue ((f @"first.last") = Invalid) "Expcect Invalid"

    let test23 (f : string -> bool) =
        testCase "test23" <| fun () -> 
            Expect.isTrue ((f @"12345678901234567890123456789012345678901234567890123456789012345@iana.org") = Invalid) "Expcect Invalid"

    let test24 (f : string -> bool) =
        testCase "test24" <| fun () -> 
            Expect.isTrue ((f @".first.last@iana.org") = Invalid) "Expcect Invalid"

    let test25 (f : string -> bool) =
        testCase "test25" <| fun () -> 
            Expect.isTrue ((f @"first.last.@iana.org") = Invalid) "Expcect Invalid"

    let test26 (f : string -> bool) =
        testCase "test26" <| fun () -> 
            Expect.isTrue ((f @"first..last@iana.org") = Invalid) "Expcect Invalid"

    let test27 (f : string -> bool) =
    //"first"last"@iana.org
        testCase "test27" <| fun () -> 
            Expect.isTrue ((f "\"first\"last\"@iana.org") = Invalid) "Expcect Invalid"

    let test28 (f : string -> bool) =
    //"first\last"@iana.org
        testCase "test28" <| fun () -> 
            Expect.isTrue ((f "\"first\\last\"@iana.org") = Valid) "Expcect Valid"

    let test29 (f : string -> bool) =
    //"""@iana.org
        testCase "test29" <| fun () -> 
            Expect.isTrue ((f "\"\"\"@iana.org") = Invalid) "Expcect Invalid"

    let test30 (f : string -> bool) =
    //"\"@iana.org
        testCase "test30" <| fun () -> 
            Expect.isTrue ((f "\"\\\"@iana.org") = Invalid) "Expcect Invalid"

    let test31 (f : string -> bool) =
    //""@iana.org
        testCase "test31" <| fun () -> 
            Expect.isTrue ((f "\"\"@iana.org") = Invalid) "Expcect Invalid"

    let test32 (f : string -> bool) =
        testCase "test32" <| fun () -> 
            Expect.isTrue ((f @"first\\@last@iana.org") = Invalid) "Expcect Invalid"

    let test33 (f : string -> bool) =
        testCase "test33" <| fun () -> 
            Expect.isTrue ((f @"first.last@") = Invalid) "Expcect Invalid"

    let test34 (f : string -> bool) =
        testCase "test34" <| fun () -> 
            Expect.isTrue ((f @"x@x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456789.x23456") = Invalid) "Expcect Invalid"

    let test35 (f : string -> bool) =
        testCase "test35" <| fun () -> 
            Expect.isTrue ((f @"first.last@[.12.34.56.78]") = Invalid) "Expcect Invalid"

    let test36 (f : string -> bool) =
        testCase "test36" <| fun () -> 
            Expect.isTrue ((f @"first.last@[12.34.56.789]") = Invalid) "Expcect Invalid"

    let test37 (f : string -> bool) =
        testCase "test37" <| fun () -> 
            Expect.isTrue ((f @"first.last@[::12.34.56.78]") = Invalid) "Expcect Invalid"

    let test38 (f : string -> bool) =
        testCase "test37" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv5:::12.34.56.78]") = Invalid) "Expcect Invalid"

    let test39 (f : string -> bool) =
        testCase "test38" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333::4444:5555:12.34.56.78]") = Valid) "Expcect Valid"

    let test40 (f : string -> bool) =
        testCase "test40" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:12.34.56.78]") = Invalid) "Expcect Invalid"

    let test41 (f : string -> bool) =
        testCase "test41" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:7777:12.34.56.78]") = Invalid) "Expcect Invalid"

    let test42 (f : string -> bool) =
        testCase "test42" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:7777]") = Invalid) "Expcect Invalid"

    let test43 (f : string -> bool) =
        testCase "test43" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:7777:8888:9999]") = Invalid) "Expcect Invalid"

    let test44 (f : string -> bool) =
        testCase "test44" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222::3333::4444:5555:6666]") = Invalid) "Expcect Invalid"

    let test45 (f : string -> bool) =
        testCase "test45" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333::4444:5555:6666:7777]") = Valid) "Expcect Valid"

    let test46 (f : string -> bool) =
        testCase "test46" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:333x::4444:5555]") = Invalid) "Expcect Invalid"

    let test47 (f : string -> bool) =
        testCase "test47" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:33333::4444:5555]") = Invalid) "Expcect Invalid"

    let test48 (f : string -> bool) =
        testCase "test48" <| fun () -> 
            Expect.isTrue ((f @"first.last@example.123") = Valid) "Expcect Valid"

    let test49 (f : string -> bool) =
        testCase "test49" <| fun () -> 
            Expect.isTrue ((f @"first.last@com") = Valid) "Expcect Valid"

    let test50 (f : string -> bool) =
        testCase "test50" <| fun () -> 
            Expect.isTrue ((f @"first.last@-xample.com") = Invalid) "Expcect Invalid"

    let test51 (f : string -> bool) =
        testCase "test51" <| fun () -> 
            Expect.isTrue ((f @"first.last@exampl-.com") = Invalid) "Expcect Invalid"

    let test52 (f : string -> bool) =
        testCase "test52" <| fun () -> 
            Expect.isTrue ((f @"first.last@x234567890123456789012345678901234567890123456789012345678901234.iana.org") 
                = Invalid) "Expcect Invalid"

    let test53 (f : string -> bool) =
    //"Abc\@def"@iana.org
        testCase "test53" <| fun () -> 
            Expect.isTrue ((f "\"Abc\\@def\"@iana.org") = Valid) "Expcect Valid"

    let test54 (f : string -> bool) =
    //"Fred\ Bloggs"@iana.org
        testCase "test54" <| fun () -> 
            Expect.isTrue ((f "\"Fred\\ Bloggs\"@iana.org") = Valid) "Expcect Valid"

    let test55 (f : string -> bool) =
    //"Joe.\\Blow"@iana.org
        testCase "test55" <| fun () -> 
            Expect.isTrue ((f "\"Joe.\\\\Blow\"@iana.org") = Valid) "Expcect Valid"

    let test56 (f : string -> bool) =
    //"Abc@def"@iana.org
        testCase "test56" <| fun () -> 
            Expect.isTrue ((f "\"Abc@def\"@iana.org") = Valid) "Expcect Valid"

    let test57 (f : string -> bool) =
    //"Fred Bloggs"@iana.org
        testCase "test57" <| fun () -> 
            Expect.isTrue ((f "\"Fred Bloggs\"@iana.org") = Valid) "Expcect Valid"

    let test58 (f : string -> bool) =
        testCase "test58" <| fun () -> 
            Expect.isTrue ((f @"user+mailbox@iana.org") = Valid) "Expcect Valid"

    let test59 (f : string -> bool) =
        testCase "test59" <| fun () -> 
            Expect.isTrue ((f @"customer/department=shipping@iana.org") = Valid) "Expcect Valid"

    let test60 (f : string -> bool) =
        testCase "test60" <| fun () -> 
            Expect.isTrue ((f @"$A12345@iana.org") = Valid) "Expcect Valid"

    let test61 (f : string -> bool) =
        testCase "test61" <| fun () -> 
            Expect.isTrue ((f @"!def!xyz%abc@iana.org") = Valid) "Expcect Valid"

    let test62 (f : string -> bool) =
        testCase "test62" <| fun () -> 
            Expect.isTrue ((f @"_somename@iana.org") = Valid) "Expcect Valid"

    let test63 (f : string -> bool) =
        testCase "test63" <| fun () -> 
            Expect.isTrue ((f @"dclo@us.ibm.com") = Valid) "Expcect Valid"

    let test64 (f : string -> bool) =
        testCase "test64" <| fun () -> 
            Expect.isTrue ((f @"abc\@def@iana.org") = Invalid) "Expcect Invalid"

    let test65 (f : string -> bool) =
        testCase "test65" <| fun () -> 
            Expect.isTrue ((f @"abc\\@iana.org") = Invalid) "Expcect Invalid"

    let test66 (f : string -> bool) =
        testCase "test66" <| fun () -> 
            Expect.isTrue ((f @"peter.piper@iana.org") = Valid) "Expcect Valid"

    let test67 (f : string -> bool) =
    //Doug\ \"Ace\"\ Lovell@iana.org
        testCase "test67" <| fun () -> 
            Expect.isTrue ((f "Doug\\ \\\"Ace\\\"\\ Lovell@iana.org") = Invalid) "Expcect Invalid"

    let test68 (f : string -> bool) =
    //"Doug \"Ace\" L."@iana.org
        testCase "test68" <| fun () -> 
            Expect.isTrue ((f "\"Doug \\\"Ace\\\" L.\"@iana.org") = Valid) "Expcect Valid"

    let test69 (f : string -> bool) =
        testCase "test69" <| fun () -> 
            Expect.isTrue ((f @"abc@def@iana.org") = Invalid) "Expcect Invalid"

    let test70 (f : string -> bool) =
        testCase "test70" <| fun () -> 
            Expect.isTrue ((f @"abc\\@def@iana.org") = Invalid) "Expcect Invalid"

    let test71 (f : string -> bool) =
        testCase "test71" <| fun () -> 
            Expect.isTrue ((f @"abc\@iana.org") = Invalid) "Expcect Invalid"

    let test72 (f : string -> bool) =
        testCase "test72" <| fun () -> 
            Expect.isTrue ((f @"@iana.org") = Invalid) "Expcect Invalid"

    let test73 (f : string -> bool) =
        testCase "test73" <| fun () -> 
            Expect.isTrue ((f @"doug@") = Invalid) "Expcect Invalid"

    let test74 (f : string -> bool) =
    //"qu@iana.org
        testCase "test74" <| fun () -> 
            Expect.isTrue ((f "\"qu@iana.org") = Invalid) "Expcect Invalid"

    let test75 (f : string -> bool) =
    //ote"@iana.org
        testCase "test75" <| fun () -> 
            Expect.isTrue ((f "ote\"@iana.org") = Invalid) "Expcect Invalid"

    let test76 (f : string -> bool) =
        testCase "test76" <| fun () -> 
            Expect.isTrue ((f @".dot@iana.org") = Invalid) "Expcect Invalid"

    let test77 (f : string -> bool) =
        testCase "test77" <| fun () -> 
            Expect.isTrue ((f @"dot.@iana.org") = Invalid) "Expcect Invalid"

    let test78 (f : string -> bool) =
        testCase "test78" <| fun () -> 
            Expect.isTrue ((f @"two..dot@iana.org") = Invalid) "Expcect Invalid"

    let test79 (f : string -> bool) =
    //"Doug "Ace" L."@iana.org
        testCase "test79" <| fun () -> 
            Expect.isTrue ((f "\"Doug \"Ace\" L.\"@iana.org") = Invalid) "Expcect Invalid"

    let test80 (f : string -> bool) =
    //Doug\ \"Ace\"\ L\.@iana.org
        testCase "test80" <| fun () -> 
            Expect.isTrue ((f "Doug\\ \\\"Ace\\\"\\ L\\.@iana.org") = Invalid) "Expcect Invalid"

    let test81 (f : string -> bool) =
        testCase "test81" <| fun () -> 
            Expect.isTrue ((f @"hello world@iana.org") = Invalid) "Expcect Invalid"

    let test82 (f : string -> bool) =
        testCase "test82" <| fun () -> 
            Expect.isTrue ((f @"gatsby@f.sc.ot.t.f.i.tzg.era.l.d.") = Invalid) "Expcect Invalid"

    let test83 (f : string -> bool) =
        testCase "test83" <| fun () -> 
            Expect.isTrue ((f @"test@iana.org") = Valid) "Expcect Valid"

    let test84 (f : string -> bool) =
        testCase "test84" <| fun () -> 
            Expect.isTrue ((f @"TEST@iana.org") = Valid) "Expcect Valid"

    let test85 (f : string -> bool) =
        testCase "test85" <| fun () -> 
            Expect.isTrue ((f @"1234567890@iana.org") = Valid) "Expcect Valid"

    let test86 (f : string -> bool) =
        testCase "test86" <| fun () -> 
            Expect.isTrue ((f @"test+test@iana.org") = Valid) "Expcect Valid"

    let test87 (f : string -> bool) =
        testCase "test87" <| fun () -> 
            Expect.isTrue ((f @"test-test@iana.org") = Valid) "Expcect Valid"

    let test88 (f : string -> bool) =
        testCase "test88" <| fun () -> 
            Expect.isTrue ((f @"t*est@iana.org") = Valid) "Expcect Valid"

    let test89 (f : string -> bool) =
        testCase "test89" <| fun () -> 
            Expect.isTrue ((f @"+1~1+@iana.org") = Valid) "Expcect Valid"

    let test90 (f : string -> bool) =
        testCase "test90" <| fun () -> 
            Expect.isTrue ((f @"{_test_}@iana.org") = Valid) "Expcect Valid"

    let test91 (f : string -> bool) =
    //"[[ test ]]"@iana.org
        testCase "test91" <| fun () -> 
            Expect.isTrue ((f "\"[[ test ]]\"@iana.org") = Valid) "Expcect Valid"

    let test92 (f : string -> bool) =
        testCase "test92" <| fun () -> 
            Expect.isTrue ((f @"test.test@iana.org") = Valid) "Expcect Valid"

    let test93 (f : string -> bool) =
    //"test.test"@iana.org
        testCase "test93" <| fun () -> 
            Expect.isTrue ((f "\"test.test\"@iana.org") = Valid) "Expcect Valid"

    let test94 (f : string -> bool) =
    //test."test"@iana.org
        testCase "test94" <| fun () -> 
            Expect.isTrue ((f "test.\"test\"@iana.org") = Valid) "Expcect Valid"

    let test95 (f : string -> bool) =
    //"test@test"@iana.org
        testCase "test95" <| fun () -> 
            Expect.isTrue ((f "\"test@test\"@iana.org") = Valid) "Expcect Valid"

    let test96 (f : string -> bool) =
        testCase "test96" <| fun () -> 
            Expect.isTrue ((f @"test@123.123.123.x123") = Valid) "Expcect Valid"

    let test97 (f : string -> bool) =
        testCase "test97" <| fun () -> 
            Expect.isTrue ((f @"test@123.123.123.123") = Valid) "Expcect Valid"

    let test98(f : string -> bool) =
        testCase "test98" <| fun () -> 
            Expect.isTrue ((f @"test@[123.123.123.123]") = Valid) "Expcect Valid"

    let test99(f : string -> bool) =
        testCase "test99" <| fun () -> 
            Expect.isTrue ((f @"test@example.iana.org") = Valid) "Expcect Valid"

    let test100 (f : string -> bool) =
        testCase "test100" <| fun () -> 
            Expect.isTrue ((f @"test@example.example.iana.org") = Valid) "Expcect Valid"

    let test101 (f : string -> bool) =
        testCase "test101" <| fun () -> 
            Expect.isTrue ((f @"test.iana.org") = Invalid) "Expcect Invalid"

    let test102 (f : string -> bool) =
        testCase "test102" <| fun () -> 
            Expect.isTrue ((f @"test.@iana.org") = Invalid) "Expcect Invalid"

    let test103 (f : string -> bool) =
        testCase "test103" <| fun () -> 
            Expect.isTrue ((f @"test..test@iana.org") = Invalid) "Expcect Invalid"

    let test104 (f : string -> bool) =
        testCase "test104" <| fun () -> 
            Expect.isTrue ((f @".test@iana.org") = Invalid) "Expcect Invalid"

    let test105 (f : string -> bool) =
        testCase "test105" <| fun () -> 
            Expect.isTrue ((f @"test@test@iana.org") = Invalid) "Expcect Invalid"

    let test106 (f : string -> bool) =
        testCase "test106" <| fun () -> 
            Expect.isTrue ((f @"test@@iana.org") = Invalid) "Expcect Invalid"

    let test107 (f : string -> bool) =
        testCase "test107" <| fun () -> 
            Expect.isTrue ((f @"-- test --@iana.org") = Invalid) "Expcect Invalid"

    let test108 (f : string -> bool) =
        testCase "test108" <| fun () -> 
            Expect.isTrue ((f @"[test]@iana.org") = Invalid) "Expcect Invalid"

    let test109 (f : string -> bool) =
    //"test\test"@iana.org
        testCase "test109" <| fun () -> 
            Expect.isTrue ((f "\"test\\test\"@iana.org") = Valid) "Expcect Valid"

    let test110 (f : string -> bool) =
    //"test"test"@iana.org
        testCase "test110" <| fun () -> 
            Expect.isTrue ((f "\"test\"test\"@iana.org") = Invalid) "Expcect Invalid"

    let test111 (f : string -> bool) =
        testCase "test111" <| fun () -> 
            Expect.isTrue ((f @"()[]\;:,><@iana.org") = Invalid) "Expcect Invalid"

    let test112 (f : string -> bool) =
        testCase "test112" <| fun () -> 
            Expect.isTrue ((f @"test@.") = Invalid) "Expcect Invalid"

    let test113 (f : string -> bool) =
        testCase "test113" <| fun () -> 
            Expect.isTrue ((f @"test@example.") = Invalid) "Expcect Invalid"

    let test114 (f : string -> bool) =
        testCase "test114" <| fun () -> 
            Expect.isTrue ((f @"test@.org") = Invalid) "Expcect Invalid"

    let test115 (f : string -> bool) =
        testCase "test115" <| fun () -> 
            Expect.isTrue ((f @"test@123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012.com") 
                = Invalid) "Expcect Invalid"

    let test116 (f : string -> bool) =
        testCase "test116" <| fun () -> 
            Expect.isTrue ((f @"test@example") = Valid) "Expcect Valid"

    let test117 (f : string -> bool) =
        testCase "test117" <| fun () -> 
            Expect.isTrue ((f @"test@[123.123.123.123") = Invalid) "Expcect Invalid"

    let test118(f : string -> bool) =
        testCase "test118" <| fun () -> 
            Expect.isTrue ((f @"test@123.123.123.123]") = Invalid) "Expcect Invalid"

    let test119 (f : string -> bool) =
        testCase "test119" <| fun () -> 
            Expect.isTrue ((f @"NotAnEmail") = Invalid) "Expcect Invalid"

    let test120 (f : string -> bool) =
        testCase "test120" <| fun () -> 
            Expect.isTrue ((f @"@NotAnEmail") = Invalid) "Expcect Invalid"

    let test121 (f : string -> bool) =
    //"test\\blah"@iana.org
        testCase "test121" <| fun () -> 
            Expect.isTrue ((f "\"test\\\\blah\"@iana.org") = Valid) "Expcect Valid"

    let test122 (f : string -> bool) =
    //"test\blah"@iana.org
        testCase "test122" <| fun () -> 
            Expect.isTrue ((f "\"test\\blah\"@iana.org") = Valid) "Expcect Valid"

    let test123 (f : string -> bool) =
    //"test\&#13;blah"@iana.org
        testCase "test123" <| fun () -> 
            Expect.isTrue ((f "\"test\\&#13;blah\"@iana.org") = Valid) "Expcect Valid"

    let test124 (f : string -> bool) =
    //"test&#13;blah"@iana.org
        testCase "test124" <| fun () -> 
            Expect.isTrue ((f "\"test&#13;blah\"@iana.org") = Invalid) "Expcect Invalid"

    let test125 (f : string -> bool) =
    //"test\"blah"@iana.org
        testCase "test125" <| fun () -> 
            Expect.isTrue ((f "\"test\\\"blah\"@iana.org") = Valid) "Expcect Valid"

    let test126 (f : string -> bool) =
    //"test"blah"@iana.org
        testCase "test126" <| fun () -> 
            Expect.isTrue ((f "\"test\"blah\"@iana.org") = Invalid) "Expcect Invalid"

    let test127 (f : string -> bool) =
        testCase "test127" <| fun () -> 
            Expect.isTrue ((f @"customer/department@iana.org") = Valid) "Expcect Valid"

    let test128 (f : string -> bool) =
        testCase "test128" <| fun () -> 
            Expect.isTrue ((f @"_Yosemite.Sam@iana.org") = Valid) "Expcect Valid"

    let test129 (f : string -> bool) =
        testCase "test129" <| fun () -> 
            Expect.isTrue ((f @"~@iana.org") = Valid) "Expcect Valid"

    let test130 (f : string -> bool) =
        testCase "test130" <| fun () -> 
         Expect.isTrue ((f @".wooly@iana.org") = Invalid) "Expcect Invalid"

    let test131 (f : string -> bool) =
        testCase "test131" <| fun () -> 
            Expect.isTrue ((f @"wo..oly@iana.org") = Invalid) "Expcect Invalid"

    let test132 (f : string -> bool) =
        testCase "test132" <| fun () -> 
            Expect.isTrue ((f @"pootietang.@iana.org") = Invalid) "Expcect Invalid"

    let test133 (f : string -> bool) =
        testCase "test133" <| fun () -> 
         Expect.isTrue ((f @".@iana.org") = Invalid) "Expcect Invalid"

    let test134 (f : string -> bool) =
    //"Austin@Powers"@iana.org
        testCase "test134" <| fun () -> 
            Expect.isTrue ((f "\"Austin@Powers\"@iana.org") = Valid) "Expcect Valid"

    let test135 (f : string -> bool) =
        testCase "test135" <| fun () -> 
            Expect.isTrue ((f @"Ima.Fool@iana.org") = Valid) "Expcect Valid"

//following 2 are duplicates in the source
    let test136 (f : string -> bool) =
    //"Ima.Fool"@iana.org
        testCase "test136" <| fun () -> 
            Expect.isTrue ((f "\"Ima.Fool\"@iana.org") = Valid) "Expcect Valid"

    let test137 (f : string -> bool) =
    //"Ima Fool"@iana.org
        testCase "test137" <| fun () -> 
            Expect.isTrue ((f "\"Ima Fool\"@iana.org") = Valid) "Expcect Valid"

    let test138 (f : string -> bool) =
        testCase "test138" <| fun () -> 
            Expect.isTrue ((f @"Ima Fool@iana.org") = Invalid) "Expcect Invalid"

    let test139 (f : string -> bool) =
        testCase "test139" <| fun () -> 
            Expect.isTrue ((f @"phil.h\@\@ck@haacked.com") = Invalid) "Expcect Invalid"

    let test140 (f : string -> bool) =
    //"first"."last"@iana.org
        testCase "test140" <| fun () -> 
            Expect.isTrue ((f "\"first\".\"last\"@iana.org") = Valid) "Expcect Valid"

    let test141 (f : string -> bool) =
    //"first".middle."last"@iana.org
        testCase "test141" <| fun () -> 
            Expect.isTrue ((f "\"first\".middle.\"last\"@iana.org") = Valid) "Expcect Valid"

    let test142 (f : string -> bool) =
    //"first\\"last"@iana.org
        testCase "test142" <| fun () -> 
            Expect.isTrue ((f "\"first\\\\\"last\"@iana.org") = Invalid) "Expcect Invalid"

    let test143 (f : string -> bool) =
    //"first".last@iana.org
        testCase "test143" <| fun () -> 
            Expect.isTrue ((f "\"first\".last@iana.org") = Valid) "Expcect Valid"

    let test144 (f : string -> bool) =
    //first."last"@iana.org
        testCase "test144" <| fun () -> 
            Expect.isTrue ((f "first.\"last\"@iana.org") = Valid) "Expcect Valid"

    let test145 (f : string -> bool) =
    //"first"."middle"."last"@iana.org
        testCase "test145" <| fun () -> 
            Expect.isTrue ((f "\"first\".\"middle\".\"last\"@iana.org") = Valid) "Expcect Valid"

    let test146 (f : string -> bool) =
    //"first.middle"."last"@iana.org
        testCase "test146" <| fun () -> 
            Expect.isTrue ((f "\"first.middle\".\"last\"@iana.org") = Valid) "Expcect Valid"

    let test147 (f : string -> bool) =
    //"first.middle.last"@iana.org
        testCase "test147" <| fun () -> 
            Expect.isTrue ((f "\"first.middle.last\"@iana.org") = Valid) "Expcect Valid"

    let test148 (f : string -> bool) =
    //"first..last"@iana.org
        testCase "test148" <| fun () -> 
            Expect.isTrue ((f "\"first..last\"@iana.org") = Valid) "Expcect Valid"

    let test149 (f : string -> bool) =
        testCase "test149" <| fun () -> 
            Expect.isTrue ((f @"foo@[\1.2.3.4]") = Invalid) "Expcect Invalid"

    let test150 (f : string -> bool) =
    //"first\\\"last"@iana.org
        testCase "test150" <| fun () -> 
            Expect.isTrue ((f "\"first\\\\\\\"last\"@iana.org") = Valid) "Expcect Valid"

    let test151 (f : string -> bool) =
    //first."mid\dle"."last"@iana.org
        testCase "test151" <| fun () -> 
            Expect.isTrue ((f "first.\"mid\\dle\".\"last\"@iana.org") = Valid) "Expcect Valid"

    let test152 (f : string -> bool) =
        testCase "test152" <| fun () -> 
            Expect.isTrue ((f @"Test.&#13;&#10; Folding.&#13;&#10; Whitespace@iana.org") = Valid) "Expcect Valid"

    let test153 (f : string -> bool) =
    //first."".last@iana.org
        testCase "test153" <| fun () -> 
            Expect.isTrue ((f "first.\"\".last@iana.org") = Invalid) "Expcect Invalid"

    let test154 (f : string -> bool) =
        testCase "test154" <| fun () -> 
            Expect.isTrue ((f @"first\last@iana.org") = Invalid) "Expcect Invalid"

    let test155 (f : string -> bool) =
        testCase "test155" <| fun () -> 
            Expect.isTrue ((f @"Abc\@def@iana.org") = Invalid) "Expcect Invalid"

    let test156 (f : string -> bool) =
        testCase "test156" <| fun () -> 
            Expect.isTrue ((f @"Fred\ Bloggs@iana.org") = Invalid) "Expcect Invalid"

    let test157 (f : string -> bool) =
        testCase "test157" <| fun () -> 
            Expect.isTrue ((f @"Joe.\\Blow@iana.org") = Invalid) "Expcect Invalid"

    let test158 (f : string -> bool) =
        testCase "test158" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:1111:2222:3333:4444:5555:6666:12.34.567.89]") = Invalid) "Expcect Invalid"

    let test159 (f : string -> bool) =
    //"test\&#13;&#10; blah"@iana.org
        testCase "test159" <| fun () -> 
            Expect.isTrue ((f "\"test\&#13;&#10; blah\"@iana.org") = Invalid) "Expcect Invalid"

    let test160 (f : string -> bool) =
    //"test&#13;&#10; blah"@iana.org
        testCase "test160" <| fun () -> 
            Expect.isTrue ((f "\"test&#13;&#10; blah\"@iana.org") = Valid) "Expcect Valid"

    let test161 (f : string -> bool) =
        testCase "test161" <| fun () -> 
            Expect.isTrue ((f @"{^c\@**Dog^}@cartoon.com") = Invalid) "Expcect Invalid"

    let test162 (f : string -> bool) =
        testCase "test162" <| fun () -> 
            Expect.isTrue ((f @"(foo)cal(bar)@(baz)iamcal.com(quux)") = Valid) "Expcect Valid"

    let test163 (f : string -> bool) =
        testCase "test163" <| fun () -> 
            Expect.isTrue ((f @"cal@iamcal(woo).(yay)com") = Valid) "Expcect Valid"

    let test164 (f : string -> bool) =
    //"foo"(yay)@(hoopla)[1.2.3.4]
        testCase "test164" <| fun () -> 
            Expect.isTrue ((f "\"foo\"(yay)@(hoopla)[1.2.3.4]") = Invalid) "Expcect Invalid"

    let test165 (f : string -> bool) =
        testCase "test165" <| fun () -> 
            Expect.isTrue ((f @"cal(woo(yay)hoopla)@iamcal.com") = Valid) "Expcect Valid"

    let test166 (f : string -> bool) =
        testCase "test166" <| fun () -> 
            Expect.isTrue ((f @"cal(foo\@bar)@iamcal.com") = Valid) "Expcect Valid"

    let test167 (f : string -> bool) =
        testCase "test167" <| fun () -> 
            Expect.isTrue ((f @"cal(foo\)bar)@iamcal.com") = Valid) "Expcect Valid"

    let test168 (f : string -> bool) =
        testCase "test168" <| fun () -> 
            Expect.isTrue ((f @"cal(foo(bar)@iamcal.com") = Invalid) "Expcect Invalid"

    let test169 (f : string -> bool) =
        testCase "test196" <| fun () -> 
            Expect.isTrue ((f @"cal(foo)bar)@iamcal.com") = Invalid) "Expcect Invalid"

    let test170(f : string -> bool) =
        testCase "test170" <| fun () -> 
            Expect.isTrue ((f @"cal(foo\)@iamcal.com") = Invalid) "Expcect Invalid"

    let test171 (f : string -> bool) =
        testCase "test171" <| fun () -> 
            Expect.isTrue ((f @"first().last@iana.org") = Valid) "Expcect Valid"

    let test172 (f : string -> bool) =
        testCase "test172" <| fun () -> 
            Expect.isTrue ((f @"first.(&#13;&#10; middle&#13;&#10; )last@iana.org") = Valid) "Expcect Valid"

    let test173 (f : string -> bool) =
        testCase "test173" <| fun () -> 
            Expect.isTrue ((f @"first(12345678901234567890123456789012345678901234567890)last@(1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890)iana.org") 
                = Invalid) "Expcect Invalid"

    let test174 (f : string -> bool) =
    //first(Welcome to&#13;&#10; the ("wonderful" (!)) world&#13;&#10; of email)@iana.org
        testCase "test174" <| fun () -> 
            Expect.isTrue ((f "first(Welcome to&#13;&#10; the (\"wonderful\" (!)) world&#13;&#10; of email)@iana.org") 
                = Valid) "Expcect Valid"

    let test175 (f : string -> bool) =
        testCase "test175" <| fun () -> 
            Expect.isTrue ((f @"pete(his account)@silly.test(his host)") = Valid) "Expcect Valid"

    let test176 (f : string -> bool) =
        testCase "test176" <| fun () -> 
            Expect.isTrue ((f @"c@(Chris's host.)public.example") = Valid) "Expcect Valid"

    let test177(f : string -> bool) =
        testCase "test177" <| fun () -> 
            Expect.isTrue ((f @"jdoe@machine(comment).  example") = Valid) "Expcect Valid"

    let test178 (f : string -> bool) =
        testCase "test178" <| fun () -> 
            Expect.isTrue ((f @"1234   @   local(blah)  .machine .example") = Valid) "Expcect Valid"

    let test179 (f : string -> bool) =
        testCase "test179" <| fun () -> 
            Expect.isTrue ((f @"first(middle)last@iana.org") = Invalid) "Expcect Invalid"

    let test180 (f : string -> bool) =
        testCase "test180" <| fun () -> 
            Expect.isTrue ((f @"first(abc.def).last@iana.org") = Valid) "Expcect Valid"

    let test181 (f : string -> bool) =
    //first(a"bc.def).last@iana.org
        testCase "test181" <| fun () -> 
            Expect.isTrue ((f "first(a\"bc.def).last@iana.org") = Valid) "Expcect Valid"

    let test182 (f : string -> bool) =
    //first.(")middle.last(")@iana.org
        testCase "test182" <| fun () -> 
            Expect.isTrue ((f "first.(\")middle.last(\")@iana.org") = Valid) "Expcect Valid"

    let test183 (f : string -> bool) =
    //first(abc("def".ghi).mno)middle(abc("def".ghi).mno).last@(abc("def".ghi).mno)example(abc("def".ghi).mno).(abc("def".ghi).mno)com(abc("def".ghi).mno)
        testCase "test183" <| fun () -> 
            Expect.isTrue ((f "first(abc(\"def\".ghi).mno)middle(abc(\"def\".ghi).mno).last@(abc(\"def\".ghi).mno)example(abc(\"def\".ghi).mno).(abc(\"def\".ghi).mno)com(abc(\"def\".ghi).mno)") 
                = Invalid) "Expcect Invalid"

    let test184 (f : string -> bool) =
        testCase "test184" <| fun () -> 
            Expect.isTrue ((f @"first(abc\(def)@iana.org") = Valid) "Expcect Valid"

    let test185 (f : string -> bool) =
        testCase "test185" <| fun () -> 
            Expect.isTrue ((f @"first.last@x(1234567890123456789012345678901234567890123456789012345678901234567890).com") 
                = Valid) "Expcect Valid"

    let test186 (f : string -> bool) =
        testCase "test186" <| fun () -> 
            Expect.isTrue ((f @"a(a(b(c)d(e(f))g)h(i)j)@iana.org") = Valid) "Expcect Valid"

    let test187 (f : string -> bool) =
        testCase "test187" <| fun () -> 
            Expect.isTrue ((f @"a(a(b(c)d(e(f))g)(h(i)j)@iana.org") = Invalid) "Expcect Invalid"

    let test188 (f : string -> bool) =
        testCase "test188" <| fun () -> 
            Expect.isTrue ((f @"name.lastname@domain.com") = Valid) "Expcect Valid"

    let test189 (f : string -> bool) =
        testCase "test189" <| fun () -> 
            Expect.isTrue ((f @".@") = Invalid) "Expcect Invalid"

    let test190 (f : string -> bool) =
        testCase "test190" <| fun () -> 
            Expect.isTrue ((f @"a@b") = Valid) "Expcect Valid"

    let test191 (f : string -> bool) =
        testCase "test191" <| fun () -> 
            Expect.isTrue ((f @"@bar.com") = Invalid) "Expcect Invalid"

    let test192 (f : string -> bool) =
        testCase "test192" <| fun () -> 
            Expect.isTrue ((f @"@@bar.com") = Invalid) "Expcect Invalid"

    let test193 (f : string -> bool) =
        testCase "test193" <| fun () -> 
            Expect.isTrue ((f @"a@bar.com") = Valid) "Expcect Valid"

    let test194 (f : string -> bool) =
        testCase "test194" <| fun () -> 
            Expect.isTrue ((f @"aaa.com") = Invalid) "Expcect Invalid"

    let test195 (f : string -> bool) =
        testCase "test195" <| fun () -> 
            Expect.isTrue ((f @"aaa@.com") = Invalid) "Expcect Invalid"

    let test196 (f : string -> bool) =
        testCase "test196" <| fun () -> 
            Expect.isTrue ((f @"aaa@.123") = Invalid) "Expcect Invalid"

    let test197 (f : string -> bool) =
        testCase "test197" <| fun () -> 
            Expect.isTrue ((f @"aaa@[123.123.123.123]") = Valid) "Expcect Valid"

    let test198 (f : string -> bool) =
        testCase "test198" <| fun () -> 
            Expect.isTrue ((f @"aaa@[123.123.123.123]a") = Invalid) "Expcect Invalid"

    let test199 (f : string -> bool) =
        testCase "test199" <| fun () -> 
            Expect.isTrue ((f @"aaa@[123.123.123.333]") = Invalid) "Expcect Invalid"

    let test200 (f : string -> bool) =
        testCase "test200" <| fun () -> 
            Expect.isTrue ((f @"a@bar.com.") = Invalid) "Expcect Invalid"

    let test201 (f : string -> bool) =
        testCase "test201" <| fun () -> 
            Expect.isTrue ((f @"a@bar") = Valid) "Expcect Valid"

    let test202 (f : string -> bool) =
        testCase "test202" <| fun () -> 
            Expect.isTrue ((f @"a-b@bar.com") = Valid) "Expcect Valid"

    let test203 (f : string -> bool) =
        testCase "test203" <| fun () -> 
            Expect.isTrue ((f @"+@b.c") = Valid) "Expcect Valid"

    let test204 (f : string -> bool) =
        testCase "test204" <| fun () -> 
            Expect.isTrue ((f @"+@b.com") = Valid) "Expcect Valid"

    let test205 (f : string -> bool) =
        testCase "test205" <| fun () -> 
            Expect.isTrue ((f @"a@-b.com") = Invalid) "Expcect Invalid"

    let test206 (f : string -> bool) =
        testCase "test206" <| fun () -> 
            Expect.isTrue ((f @"a@b-.com") = Invalid) "Expcect Invalid"

    let test207 (f : string -> bool) =
        testCase "test207" <| fun () -> 
            Expect.isTrue ((f @"-@..com") = Invalid) "Expcect Invalid"

    let test208 (f : string -> bool) =
        testCase "test208" <| fun () -> 
            Expect.isTrue ((f @"-@a..com") = Invalid) "Expcect Invalid"

    let test209 (f : string -> bool) =
        testCase "test209" <| fun () -> 
            Expect.isTrue ((f @"a@b.co-foo.uk") = Valid) "Expcect Valid"

    let test210 (f : string -> bool) =
    //"hello my name is"@stutter.com
        testCase "test210" <| fun () -> 
            Expect.isTrue ((f "\"hello my name is\"@stutter.com") = Valid) "Expcect Valid"

    let test211 (f : string -> bool) =
    //"Test \"Fail\" Ing"@iana.org
        testCase "test211" <| fun () -> 
            Expect.isTrue ((f "\"Test \\\"Fail\\\" Ing\"@iana.org") = Valid) "Expcect Valid"

    let test212 (f : string -> bool) =
        testCase "test212" <| fun () -> 
            Expect.isTrue ((f @"valid@about.museum") = Valid) "Expcect Valid"

    let test213 (f : string -> bool) =
        testCase "test213" <| fun () -> 
            Expect.isTrue ((f @"Invalid@about.museum-") = Invalid) "Expcect Invalid"

    let test214 (f : string -> bool) =
        testCase "test214" <| fun () -> 
            Expect.isTrue ((f @"shaitan@my-domain.thisisminekthx") = Valid) "Expcect Valid"

    let test215 (f : string -> bool) =
        testCase "test215" <| fun () -> 
            Expect.isTrue ((f @"test@...........com") = Invalid) "Expcect Invalid"

    let test216 (f : string -> bool) =
        testCase "test216" <| fun () -> 
            Expect.isTrue ((f @"foobar@192.168.0.1") = Valid) "Expcect Valid"

    let test217 (f : string -> bool) =
    //"Joe\\Blow"@iana.org
        testCase "test217" <| fun () -> 
            Expect.isTrue ((f "\"Joe\\\\Blow\"@iana.org") = Valid) "Expcect Valid"

    let test218 (f : string -> bool) =
        testCase "test218" <| fun () -> 
            Expect.isTrue ((f @"Invalid \&#10; Folding \&#10; Whitespace@iana.org") = Invalid) "Expcect Invalid"

    let test219 (f : string -> bool) =
        testCase "test219" <| fun () -> 
            Expect.isTrue ((f @"HM2Kinsists@(that comments are allowed)this.is.ok") = Valid) "Expcect Valid"

    let test220 (f : string -> bool) =
        testCase "test220" <| fun () -> 
            Expect.isTrue ((f @"user%uucp!path@berkeley.edu") = Valid) "Expcect Valid"

    let test221 (f : string -> bool) =
    //"first(last)"@iana.org
        testCase "test221" <| fun () -> 
            Expect.isTrue ((f "\"first(last)\"@iana.org") = Valid) "Expcect Valid"

    let test222 (f : string -> bool) =
        testCase "test222" <| fun () -> 
            Expect.isTrue ((f @"&#13;&#10; (&#13;&#10; x &#13;&#10; ) &#13;&#10; first&#13;&#10; ( &#13;&#10; x&#13;&#10; ) &#13;&#10; .&#13;&#10; ( &#13;&#10; x) &#13;&#10; last &#13;&#10; (  x &#13;&#10; ) &#13;&#10; @iana.org") 
                = Valid) "Expcect Valid"

    let test223 (f : string -> bool) =
        testCase "test223" <| fun () -> 
            Expect.isTrue ((f @"first.last @iana.org") = Valid) "Expcect Valid"

    let test224 (f : string -> bool) =
        testCase "test224" <| fun () -> 
            Expect.isTrue ((f @"test. &#13;&#10; &#13;&#10; obs@syntax.com") = Valid) "Expcect Valid"

    let test225 (f : string -> bool) =
        testCase "test225" <| fun () -> 
            Expect.isTrue ((f @"test.&#13;&#10;&#13;&#10; obs@syntax.com") = Invalid) "Expcect Invalid"

    let test226 (f : string -> bool) =  //\u0000 
    //"Unicode NULL \␀"@char.com
        testCase "test226" <| fun () -> 
            Expect.isTrue ((f "\"Unicode NULL \\\u0000\"@char.com") = Valid) "Expcect Valid"

    let test227 (f : string -> bool) =
    //"Unicode NULL ␀"@char.com
        testCase "test227" <| fun () -> 
            Expect.isTrue ((f "\"Unicode NULL \u0000\"@char.com") = Invalid) "Expcect Invalid"

    let test228 (f : string -> bool) =
    //Unicode NULL \␀@char.com
        testCase "test228" <| fun () -> 
            Expect.isTrue ((f "Unicode NULL \\\u0000@char.com") = Invalid) "Expcect Invalid"

    let test229 (f : string -> bool) =
        testCase "test229" <| fun () -> 
            Expect.isTrue ((f @"cdburgess+!#$%&'*-/=?+_{}|~test@gmail.com") = Valid) "Expcect Valid"

    let test230 (f : string -> bool) =
        testCase "test230" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::a2:a3:a4:b1:b2:b3:b4]") = Valid) "Expcect Valid"

    let test231 (f : string -> bool) =
        testCase "test231" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:a3:a4:b1:b2:b3::]") = Valid) "Expcect Valid"

    let test232 (f : string -> bool) =
        testCase "test232" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::]") = Invalid) "Expcect Invalid"

    let test233 (f : string -> bool) =
        testCase "test233" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::]") = Valid) "Expcect Valid"

    let test234 (f : string -> bool) =
        testCase "test234" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::::]") = Invalid) "Expcect Invalid"

    let test235 (f : string -> bool) =
        testCase "test235" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::b4]") = Invalid) "Expcect Invalid"

    let test236 (f : string -> bool) =
        testCase "test236" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::b4]") = Valid) "Expcect Valid"

    let test237 (f : string -> bool) =
        testCase "test237" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::::b4]") = Invalid) "Expcect Invalid"

    let test238 (f : string -> bool) =
        testCase "test238" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::b3:b4]") = Invalid) "Expcect Invalid"

    let test239 (f : string -> bool) =
        testCase "test239" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::b3:b4]") = Valid) "Expcect Valid"

    let test240(f : string -> bool) =
        testCase "test240" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::::b3:b4]") = Invalid) "Expcect Invalid"

    let test241 (f : string -> bool) =
        testCase "test241" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::b4]") = Valid) "Expcect Valid"

    let test242 (f : string -> bool) =
        testCase "test242" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:::b4]") = Invalid) "Expcect Invalid"

    let test243 (f : string -> bool) =
        testCase "test243" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:]") = Invalid) "Expcect Invalid"

    let test244 (f : string -> bool) =
        testCase "test244" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::]") = Valid) "Expcect Valid"

    let test245 (f : string -> bool) =
        testCase "test245" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:::]") = Invalid) "Expcect Invalid"

    let test246 (f : string -> bool) =
        testCase "test246" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:]") = Invalid) "Expcect Invalid"

    let test247 (f : string -> bool) =
        testCase "test247" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2::]") = Valid) "Expcect Valid"

    let test248 (f : string -> bool) =
        testCase "test248" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:::]") = Invalid) "Expcect Invalid"

    let test249 (f : string -> bool) =
        testCase "test249" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:cdef::]") = Valid) "Expcect Valid"

    let test250 (f : string -> bool) =
        testCase "test250" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:CDEF::]") = Valid) "Expcect Valid"

    let test251 (f : string -> bool) =
        testCase "test251" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::a3:a4:b1:ffff:11.22.33.44]") = Valid) "Expcect Valid"

    let test252 (f : string -> bool) =
        testCase "test252" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:::a2:a3:a4:b1:ffff:11.22.33.44]") = Valid) "Expcect Valid"

    let test253 (f : string -> bool) =
        testCase "test253" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:a3:a4::11.22.33.44]") = Valid) "Expcect Valid"

    let test254 (f : string -> bool) =
        testCase "test254" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:a3:a4:b1::11.22.33.44]") = Valid) "Expcect Valid"

    let test255 (f : string -> bool) =
        testCase "test255" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test256 (f : string -> bool) =
        testCase "test256" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test257 (f : string -> bool) =
        testCase "test257" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:11.22.33.44]") = Invalid) "Expcect Invalid"

    let test258 (f : string -> bool) =
        testCase "test258" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::11.22.33.44]") = Valid) "Expcect Valid"

    let test259 (f : string -> bool) =
        testCase "test259" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test260 (f : string -> bool) =
        testCase "test260" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2::11.22.33.44]") = Valid) "Expcect Valid"

    let test261 (f : string -> bool) =
        testCase "test261" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test262 (f : string -> bool) =
        testCase "test262" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:cdef::11.22.33.44]") = Valid) "Expcect Valid"

    let test263 (f : string -> bool) =
        testCase "test263" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:cdef::11.22.33.xx]") = Invalid) "Expcect Invalid"

    let test264 (f : string -> bool) =
        testCase "test264" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:CDEF::11.22.33.44]") = Valid) "Expcect Valid"

    let test265 (f : string -> bool) =
        testCase "test265" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:0123:4567:89ab:CDEFF::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test266 (f : string -> bool) =
        testCase "test266" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::a4:b1::b4:11.22.33.44]") = Invalid) "Expcect Invalid"

    let test267 (f : string -> bool) =
        testCase "test267" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::11.22.33]") = Invalid) "Expcect Invalid"

    let test268 (f : string -> bool) =
        testCase "test268" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::11.22.33.44.55]") = Invalid) "Expcect Invalid"

    let test269 (f : string -> bool) =
        testCase "test269" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::b211.22.33.44]") = Invalid) "Expcect Invalid"

    let test270 (f : string -> bool) =
        testCase "test270" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::b2:11.22.33.44]") = Valid) "Expcect Valid"

    let test271 (f : string -> bool) =
        testCase "test271" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::b2::11.22.33.44]") = Invalid) "Expcect Invalid"

    let test272 (f : string -> bool) =
        testCase "test272" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1::b3:]") = Invalid) "Expcect Invalid"

    let test273 (f : string -> bool) =
        testCase "test273" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::a2::b4]") = Invalid) "Expcect Invalid"

    let test274 (f : string -> bool) =
        testCase "test274" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:a3:a4:b1:b2:b3:]") = Invalid) "Expcect Invalid"

    let test275 (f : string -> bool) =
        testCase "test275" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6::a2:a3:a4:b1:b2:b3:b4]") = Invalid) "Expcect Invalid"

    let test276 (f : string -> bool) =
        testCase "test276" <| fun () -> 
            Expect.isTrue ((f @"first.last@[IPv6:a1:a2:a3:a4::b1:b2:b3:b4]") = Invalid) "Expcect Invalid"

    let test277 (f : string -> bool) =
        testCase "test277" <| fun () -> 
            Expect.isTrue ((f @"test@test.com") = Valid) "Expcect Valid"

    let test278 (f : string -> bool) =
        testCase "test278" <| fun () -> 
            Expect.isTrue ((f @"test@example.com&#10;") = Invalid) "Expcect Invalid"

    let test279 (f : string -> bool) =
        testCase "test279" <| fun () -> 
            Expect.isTrue ((f @"test@xn--example.com") = Valid) "Expcect Valid"

    let test280 (f : string -> bool) =
        testCase "test280" <| fun () -> 
            Expect.isTrue ((f @"test@Bücher.ch") = Valid) "Expcect Valid"
