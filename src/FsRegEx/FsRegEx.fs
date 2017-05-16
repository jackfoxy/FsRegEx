module FsRegEx

open System
open System.Text.RegularExpressions
open System.Threading

module Unicode =
    ///Enum Unicode defined general categories 
    type UnicodeGeneralCategory =
        ///Letter, Uppercase; Lu
        | LetterUppercase
        ///Letter, Lowercase; Ll
        | LetterLowercase
        ///Letter, Titlecase; Lt
        | LetterTitlecase
        ///Letter, Modifier; Lm
        | LetterModifier
        ///Letter, Other; Lo
        | LetterOther
        ///All letter characters. This includes the LetterUppercase, LetterLowercase, LetterTitlecase, LetterModifier, and LetterOther characters; L
        | Letter
        ///Mark, Nonspacing; Mn
        | MarkNonspacing
        ///Mark, Spacing Combining; Mc
        | MarkSpacingCombining
        ///Mark, Enclosing; Me
        | MarkEnclosing
        ///All diacritic marks. This includes the MarkNonspacing, MarkSpacingCombining, and MarkEnclosing categories; M
        | Mark
        ///Number, Decimal Digit; Nd
        | NumberDecimalDigit
        ///Number, Letter; Nl
        | NumberLetter
        ///Number, Other; No
        | NumberOther
        ///All numbers. This includes the Nd, Nl, and No categories; N
        | NumberALL
        ///Punctuation, Connector; Pc
        | PunctuationConnector
        ///Punctuation, Dash; Pd
        | PunctuationDash
        ///Punctuation, Open; Ps
        | PunctuationOpen
        ///Punctuation, Close; Pe
        | PunctuationClose
        ///Punctuation, Initial quote (may behave like Ps or Pe depending on usage); Pi
        | PunctuationInitial
        ///Punctuation, Final quote (may behave like Ps or Pe depending on usage); Pf
        | PunctuationFinal
        ///Punctuation, Other; Po
        | PunctuationOther
        ///All punctuation characters. This includes the PunctuationConnector, PunctuationDash, PunctuationOpen, PunctuationClose, PunctuationInitial, PunctuationFinal, and PunctuationOther categories; P
        | Punctuation
        ///Symbol, Math; Sm
        | SymbolMath
        ///Symbol, Currency; Sc
        | SymbolCurrency
        ///Symbol, Modifier; Sk
        | SymbolModifier
        ///Symbol, Other; So
        | SymbolOther
        ///All symbols. This includes the SymbolMath, SymbolCurrency, SymbolModifier, and SymbolOther categories; S
        | Symbol
        ///Separator, Space; Zs
        | SeparatorSpace
        ///Separator, Line; Zl
        | SeparatorLine
        ///Separator, Paragraph; Zp
        | SeparatorParagraph
        ///All separator characters. This includes the SeparatorSpace, SeparatorLine, and SeparatorLine categories; Z
        | Separator
        ///Other, Control; Cc
        | OtherControl
        ///Other, Format; Cf
        | OtherFormat
        ///Other, Surrogate; Cs
        | OtherSurrogate
        ///Other, Private Use; Co
        | OtherPrivateUse
        ///Other, Not Assigned (no characters have this property); Cn
        | OtherNotAssigned
        ///All control characters. This includes the OtherControl, OtherFormat, OtherSurrogate, OtherPrivateUse, and OtherNotAssigned categories; C
        | ControlAll
        override __.ToString() =
            match __ with
            | LetterUppercase -> "Lu"
            | LetterLowercase -> "Ll"
            | LetterTitlecase -> "Lt"
            | LetterModifier -> "Lm"
            | LetterOther -> "Lo"
            | Letter -> "L"
            | MarkNonspacing -> "Mn"
            | MarkSpacingCombining -> "Mc"
            | MarkEnclosing -> "Me"
            | Mark -> "M"
            | NumberDecimalDigit -> "Nd"
            | NumberLetter -> "Nl"
            | NumberOther -> "No"
            | NumberALL -> "N"
            | PunctuationConnector -> "Pc"
            | PunctuationDash -> "Pd"
            | PunctuationOpen -> "Ps"
            | PunctuationClose -> "Pe"
            | PunctuationInitial -> "Pi"
            | PunctuationFinal -> "Pf"
            | PunctuationOther -> "Po"
            | Punctuation -> "P"
            | SymbolMath -> "Sm"
            | SymbolCurrency -> "Sc"
            | SymbolModifier -> "Sk"
            | SymbolOther -> "So"
            | Symbol -> "S"
            | SeparatorSpace -> "Zs"
            | SeparatorLine -> "Zl"
            | SeparatorParagraph -> "Zp"
            | Separator -> "Z"
            | OtherControl -> "Cc"
            | OtherFormat -> "Cf"
            | OtherSurrogate -> "Cs"
            | OtherPrivateUse -> "Co"
            | OtherNotAssigned -> "Cn"
            | ControlAll -> "C"

    ///Enum .NET Framework named Unicode blocks 
    type SupportedNamedBlock =
        ///0000 - 007F
        | IsBasicLatin
        ///0080 - 00FF
        | IsLatin1Supplement
        ///0100 - 017F
        | IsLatinExtendedA
        ///0180 - 024F
        | IsLatinExtendedB
        ///0250 - 02AF
        | IsIPAExtensions
        ///02B0 - 02FF
        | IsSpacingModifierLetters
        ///0300 - 036F
        | IsCombiningDiacriticalMarks
        ///0370 - 03FF
        | IsGreek
        ///0370 - 03FF
        | IsGreekandCoptic
        ///0400 - 04FF
        | IsCyrillic
        ///0500 - 052F
        | IsCyrillicSupplement
        ///0530 - 058F
        | IsArmenian
        ///0590 - 05FF
        | IsHebrew
        ///0600 - 06FF
        | IsArabic
        ///0700 - 074F
        | IsSyriac
        ///0780 - 07BF
        | IsThaana
        ///0900 - 097F
        | IsDevanagari
        ///0980 - 09FF
        | IsBengali
        ///0A00 - 0A7F
        | IsGurmukhi
        ///0A80 - 0AFF
        | IsGujarati
        ///0B00 - 0B7F
        | IsOriya
        ///0B80 - 0BFF
        | IsTamil
        ///0C00 - 0C7F
        | IsTelugu
        ///0C80 - 0CFF
        | IsKannada
        ///0D00 - 0D7F
        | IsMalayalam
        ///0D80 - 0DFF
        | IsSinhala
        ///0E00 - 0E7F
        | IsThai
        ///0E80 - 0EFF
        | IsLao
        ///0F00 - 0FFF
        | IsTibetan
        ///1000 - 109F
        | IsMyanmar
        ///10A0 - 10FF
        | IsGeorgian
        ///1100 - 11FF
        | IsHangulJamo
        ///1200 - 137F
        | IsEthiopic
        ///13A0 - 13FF
        | IsCherokee
        ///1400 - 167F
        | IsUnifiedCanadianAboriginalSyllabics
        ///1680 - 169F
        | IsOgham
        ///16A0 - 16FF
        | IsRunic
        ///1700 - 171F
        | IsTagalog
        ///1720 - 173F
        | IsHanunoo
        ///1740 - 175F
        | IsBuhid
        ///1760 - 177F
        | IsTagbanwa
        ///1780 - 17FF
        | IsKhmer
        ///1800 - 18AF
        | IsMongolian
        ///1900 - 194F
        | IsLimbu
        ///1950 - 197F
        | IsTaiLe
        ///19E0 - 19FF
        | IsKhmerSymbols
        ///1D00 - 1D7F
        | IsPhoneticExtensions
        ///1E00 - 1EFF
        | IsLatinExtendedAdditional
        ///1F00 - 1FFF
        | IsGreekExtended
        ///2000 - 206F
        | IsGeneralPunctuation
        ///2070 - 209F
        | IsSuperscriptsandSubscripts
        ///20A0 - 20CF
        | IsCurrencySymbols
        ///20D0 - 20FF
        | IsCombiningDiacriticalMarksforSymbols
        ///20D0 - 20FF
        | IsCombiningMarksforSymbols
        ///2100 - 214F
        | IsLetterlikeSymbols
        ///2150 - 218F
        | IsNumberForms
        ///2190 - 21FF
        | IsArrows
        ///2200 - 22FF
        | IsMathematicalOperators
        ///2300 - 23FF
        | IsMiscellaneousTechnical
        ///2400 - 243F
        | IsControlPictures
        ///2440 - 245F
        | IsOpticalCharacterRecognition
        ///2460 - 24FF
        | IsEnclosedAlphanumerics
        ///2500 - 257F
        | IsBoxDrawing
        ///2580 - 259F
        | IsBlockElements
        ///25A0 - 25FF
        | IsGeometricShapes
        ///2600 - 26FF
        | IsMiscellaneousSymbols
        ///2700 - 27BF
        | IsDingbats
        ///27C0 - 27EF
        | IsMiscellaneousMathematicalSymbolsA
        ///27F0 - 27FF
        | IsSupplementalArrowsA
        ///2800 - 28FF
        | IsBraillePatterns
        ///2900 - 297F
        | IsSupplementalArrowsB
        ///2980 - 29FF
        | IsMiscellaneousMathematicalSymbolsB
        ///2A00 - 2AFF
        | IsSupplementalMathematicalOperators
        ///2B00 - 2BFF
        | IsMiscellaneousSymbolsandArrows
        ///2E80 - 2EFF
        | IsCJKRadicalsSupplement
        ///2F00 - 2FDF
        | IsKangxiRadicals
        ///2FF0 - 2FFF
        | IsIdeographicDescriptionCharacters
        ///3000 - 303F
        | IsCJKSymbolsandPunctuation
        ///3040 - 309F
        | IsHiragana
        ///30A0 - 30FF
        | IsKatakana
        ///3100 - 312F
        | IsBopomofo
        ///3130 - 318F
        | IsHangulCompatibilityJamo
        ///3190 - 319F
        | IsKanbun
        ///31A0 - 31BF
        | IsBopomofoExtended
        ///31F0 - 31FF
        | IsKatakanaPhoneticExtensions
        ///3200 - 32FF
        | IsEnclosedCJKLettersandMonths
        ///3300 - 33FF
        | IsCJKCompatibility
        ///3400 - 4DBF
        | IsCJKUnifiedIdeographsExtensionA
        ///4DC0 - 4DFF
        | IsYijingHexagramSymbols
        ///4E00 - 9FFF
        | IsCJKUnifiedIdeographs
        ///A000 - A48F
        | IsYiSyllables
        ///A490 - A4CF
        | IsYiRadicals
        ///AC00 - D7AF
        | IsHangulSyllables
        ///D800 - DB7F
        | IsHighSurrogates
        ///DB80 - DBFF
        | IsHighPrivateUseSurrogates
        ///DC00 - DFFF
        | IsLowSurrogates
        ///E000 - F8FF
        | IsPrivateUse
        ///F900 - FAFF
        | IsCJKCompatibilityIdeographs 
        ///FB00 - FB4F
        | IsAlphabeticPresentationForms 
        ///FB50 - FDFF
        | IsArabicPresentationFormsA 
        ///FE00 - FE0F
        | IsVariationSelectors 
        ///FE20 - FE2F
        | IsCombiningHalfMarks 
        ///FE30 - FE4F
        | IsCJKCompatibilityForms 
        ///FE50 - FE6F
        | IsSmallFormVariants 
        ///FE70 - FEFF
        | IsArabicPresentationFormsB 
        ///FF00 - FFEF
        | IsHalfwidthandFullwidthForms 
        ///FFF0 - FFFF
        | IsSpecials
        override __.ToString() =
            match __ with
            | IsBasicLatin -> "IsBasicLatin"
            | IsLatin1Supplement -> "IsLatin-1Supplement"
            | IsLatinExtendedA -> "IsLatinExtended-A"
            | IsLatinExtendedB -> "IsLatinExtended-B"
            | IsIPAExtensions -> "IsIPAExtensions"
            | IsSpacingModifierLetters -> "IsSpacingModifierLetters"
            | IsCombiningDiacriticalMarks -> "IsCombiningDiacriticalMarks"
            | IsGreek -> "IsGreek"
            | IsGreekandCoptic -> "IsGreekandCoptic"
            | IsCyrillic -> "IsCyrillic"
            | IsCyrillicSupplement -> "IsCyrillicSupplement"
            | IsArmenian -> "IsArmenian"
            | IsHebrew -> "IsHebrew"
            | IsArabic -> "IsArabic"
            | IsSyriac -> "IsSyriac"
            | IsThaana -> "IsThaana"
            | IsDevanagari -> "IsDevanagari"
            | IsBengali -> "IsBengali"
            | IsGurmukhi -> "IsGurmukhi"
            | IsGujarati -> "IsGujarati"
            | IsOriya -> "IsOriya"
            | IsTamil -> "IsTamil"
            | IsTelugu -> "IsTelugu"
            | IsKannada -> "IsKannada"
            | IsMalayalam -> "IsMalayalam"
            | IsSinhala -> "IsSinhala"
            | IsThai -> "IsThai"
            | IsLao -> "IsLao"
            | IsTibetan -> "IsTibetan"
            | IsMyanmar -> "IsMyanmar"
            | IsGeorgian -> "IsGeorgian"
            | IsHangulJamo -> "IsHangulJamo"
            | IsEthiopic -> "IsEthiopic"
            | IsCherokee -> "IsCherokee"
            | IsUnifiedCanadianAboriginalSyllabics -> "IsUnifiedCanadianAboriginalSyllabics"
            | IsOgham -> "IsOgham"
            | IsRunic -> "IsRunic"
            | IsTagalog -> "IsTagalog"
            | IsHanunoo -> "IsHanunoo"
            | IsBuhid -> "IsBuhid"
            | IsTagbanwa -> "IsTagbanwa"
            | IsKhmer -> "IsKhmer"
            | IsMongolian -> "IsMongolian"
            | IsLimbu -> "IsLimbu"
            | IsTaiLe -> "IsTaiLe"
            | IsKhmerSymbols -> "IsKhmerSymbols"
            | IsPhoneticExtensions -> "IsPhoneticExtensions"
            | IsLatinExtendedAdditional -> "IsLatinExtendedAdditional"
            | IsGreekExtended -> "IsGreekExtended"
            | IsGeneralPunctuation -> "IsGeneralPunctuation"
            | IsSuperscriptsandSubscripts -> "IsSuperscriptsandSubscripts"
            | IsCurrencySymbols -> "IsCurrencySymbols"
            | IsCombiningDiacriticalMarksforSymbols -> "IsCombiningDiacriticalMarksforSymbols"
            | IsCombiningMarksforSymbols -> "IsCombiningMarksforSymbols"
            | IsLetterlikeSymbols -> "IsLetterlikeSymbols"
            | IsNumberForms -> "IsNumberForms"
            | IsArrows -> "IsArrows"
            | IsMathematicalOperators -> "IsMathematicalOperators"
            | IsMiscellaneousTechnical -> "IsMiscellaneousTechnical"
            | IsControlPictures -> "IsControlPictures"
            | IsOpticalCharacterRecognition -> "IsOpticalCharacterRecognition"
            | IsEnclosedAlphanumerics -> "IsEnclosedAlphanumerics"
            | IsBoxDrawing -> "IsBoxDrawing"
            | IsBlockElements -> "IsBlockElements"
            | IsGeometricShapes -> "IsGeometricShapes"
            | IsMiscellaneousSymbols -> "IsMiscellaneousSymbols"
            | IsDingbats -> "IsDingbats"
            | IsMiscellaneousMathematicalSymbolsA -> "IsMiscellaneousMathematicalSymbols-A"
            | IsSupplementalArrowsA -> "IsSupplementalArrows-A"
            | IsBraillePatterns -> "IsBraillePatterns"
            | IsSupplementalArrowsB -> "IsSupplementalArrows-B"
            | IsMiscellaneousMathematicalSymbolsB -> "IsMiscellaneousMathematicalSymbols-B"
            | IsSupplementalMathematicalOperators -> "IsSupplementalMathematicalOperators"
            | IsMiscellaneousSymbolsandArrows -> "IsMiscellaneousSymbolsandArrows"
            | IsCJKRadicalsSupplement -> "IsCJKRadicalsSupplement"
            | IsKangxiRadicals -> "IsKangxiRadicals"
            | IsIdeographicDescriptionCharacters -> "IsIdeographicDescriptionCharacters"
            | IsCJKSymbolsandPunctuation -> "IsCJKSymbolsandPunctuation"
            | IsHiragana -> "IsHiragana"
            | IsKatakana -> "IsKatakana"
            | IsBopomofo -> "IsBopomofo"
            | IsHangulCompatibilityJamo -> "IsHangulCompatibilityJamo"
            | IsKanbun -> "IsKanbun"
            | IsBopomofoExtended -> "IsBopomofoExtended"
            | IsKatakanaPhoneticExtensions -> "IsKatakanaPhoneticExtensions"
            | IsEnclosedCJKLettersandMonths -> "IsEnclosedCJKLettersandMonths"
            | IsCJKCompatibility -> "IsCJKCompatibility"
            | IsCJKUnifiedIdeographsExtensionA -> "IsCJKUnifiedIdeographsExtensionA"
            | IsYijingHexagramSymbols -> "IsYijingHexagramSymbols"
            | IsCJKUnifiedIdeographs -> "IsCJKUnifiedIdeographs"
            | IsYiSyllables -> "IsYiSyllables"
            | IsYiRadicals -> "IsYiRadicals"
            | IsHangulSyllables -> "IsHangulSyllables"
            | IsHighSurrogates -> "IsHighSurrogates"
            | IsHighPrivateUseSurrogates -> "IsHighPrivateUseSurrogates"
            | IsLowSurrogates -> "IsLowSurrogates"
            | IsPrivateUse -> "IsPrivateUse"
            | IsCJKCompatibilityIdeographs  -> "IsCJKCompatibilityIdeographs "
            | IsAlphabeticPresentationForms  -> "IsAlphabeticPresentationForms "
            | IsArabicPresentationFormsA  -> "IsArabicPresentationForms-A "
            | IsVariationSelectors  -> "IsVariationSelectors "
            | IsCombiningHalfMarks  -> "IsCombiningHalfMarks "
            | IsCJKCompatibilityForms  -> "IsCJKCompatibilityForms "
            | IsSmallFormVariants  -> "IsSmallFormVariants "
            | IsArabicPresentationFormsB  -> "IsArabicPresentationForms-B "
            | IsHalfwidthandFullwidthForms  -> "IsHalfwidthandFullwidthForms "
            | IsSpecials -> "IsSpecials"

type FsGroup (name : string, group : Group) =
    let mutable captures : Capture [] option = None
    override __.Equals(yobj) = 

        match yobj with
        | :? FsGroup as y -> (group = y.Group)
        | _ -> false
    override __.GetHashCode() = group.GetHashCode()
    override __.ToString() = group.ToString()
    member __.Captures() =
        match captures with
        | Some x -> x
        | None ->
            let a : Capture[] = Array.zeroCreate group.Captures.Count 
            group.Captures.CopyTo(a, 0)
            captures <- Some a
            a
    member __.Group = group
    member __.Index =
        group.Index
    member __.Length =
        group.Length
    member ___.Name = name
    member __.Success =
        group.Success
    member __.Value =
        group.Value

type FsMatch (regex : Regex, fsMatch : Match) =
    override __.Equals(yobj) = 

        match yobj with
        | :? FsMatch as y -> (fsMatch = y.Match)
        | _ -> false
    override __.GetHashCode() = fsMatch.GetHashCode()
    override __.ToString() = fsMatch.ToString()
    member __.Captures() =

        let a : Capture[] = Array.zeroCreate fsMatch.Captures.Count 
        fsMatch.Captures.CopyTo(a, 0)
        a
    member __.Groups() =
        let a : Group[] = Array.zeroCreate fsMatch.Groups.Count 
        fsMatch.Groups.CopyTo(a, 0)
        a
        |> Array.mapi (fun i t -> FsGroup(regex.GroupNameFromNumber(i), t)) 
    member __.Index =
        fsMatch.Index
    member __.Length =
        fsMatch.Length
    member __.Match = fsMatch
    member __.Result replacement =
        fsMatch.Result replacement
    member __.Success =
        fsMatch.Success
    member __.Value =
        fsMatch.Value

module internal Common =

    let arrayFromMatches regex (c : MatchCollection) =
        let a : Match[] = Array.zeroCreate c.Count
        c.CopyTo(a, 0)
        a
        |> Array.map (fun t -> new FsMatch(regex, t))

type FsRegEx(regularExpression : string, regexOptions : RegexOptions, matchTimeout : TimeSpan) =
    let mutable _matchTimeout = matchTimeout
    let mutable _regexOptions : RegexOptions = regexOptions
    let mutable _prefixes = ""
    let mutable _source = regularExpression
    let mutable _suffixes = ""
    let mutable _regex : Regex option = None

    override __.Equals(yobj) = 
        match yobj with
        | :? FsRegEx as y -> (__.GetHashCode() = y.GetHashCode())
        | _ -> false

    override __.GetHashCode() = 
        _regexOptions.GetHashCode().ToString() + __.ToString()
        |> hash

    override __.ToString() = _prefixes + _source + _suffixes

    new () =
        FsRegEx("", RegexOptions.None, Timeout.InfiniteTimeSpan)

    new (regexOptions : RegexOptions) =
        FsRegEx("", regexOptions, Timeout.InfiniteTimeSpan)

    new (regularExpression : string) =
        FsRegEx(regularExpression, RegexOptions.None, Timeout.InfiniteTimeSpan)

    new (regularExpression : string, regexOptions : RegexOptions) =
        FsRegEx(regularExpression, regexOptions, Timeout.InfiniteTimeSpan)

    member __.Regex =
        match _regex with
        | Some x -> x
        | None ->
            let regex = new Regex(__.ToString(), _regexOptions, _matchTimeout)

            _regex <- Some regex
            regex

    member __.Capture input (groupName : string) =
        let match' = __.Regex.Match input
        match'.Groups.[groupName].Value

    member __.Clone (newRegexOptions : RegexOptions option) =
        let v =
            match newRegexOptions with
            | Some x -> new FsRegEx(x)
            | None -> new FsRegEx(_regexOptions)
             
        v.Prefixes <- _prefixes
        v.Source <- _source
        v.Suffixes <- _suffixes
        v

    member __.GroupNameFromNumber n =
        let name =
            __.Regex.GroupNameFromNumber n

        if String.IsNullOrEmpty name then None
        else Some name

    member __.GroupNames() =
        __.Regex.GetGroupNames()

    member __.GroupNumberFromName groupName =
        let n =
            __.Regex.GroupNumberFromName groupName
        if n = -1 then None
        else Some n

    member __.GroupNumbers() =
        __.Regex.GetGroupNumbers()

    member __.IsMatch input =
        __.Regex.IsMatch input

    member __.IsMatch (input, startAt) =
        __.Regex.IsMatch(input, startAt)

    member __.Match input =
        FsMatch (__.Regex, __.Regex.Match input)

    member __.Match (input, startAt) =
        FsMatch (__.Regex, __.Regex.Match(input, startAt))

    member __.Match (input, startAt, length) =
        FsMatch (__.Regex, __.Regex.Match(input, startAt, length))

    member __.Matches input =
        (__.Regex, __.Regex.Matches input)
        ||> Common.arrayFromMatches

    member __.Matches (input, startAt) =
        (__.Regex, __.Regex.Matches(input, startAt))
        ||> Common.arrayFromMatches

    member __.MatchTimeout =
        __.Regex.MatchTimeout

    member __.Prefixes
        with get() = _prefixes
        and set(value) = _prefixes <- value

    member __.RegexOptions = _regexOptions

    member __.Replace (input, (replacement : string)) =
        __.Regex.Replace(input, replacement)

    member __.Replace (input, (evalutor : MatchEvaluator)) =
        __.Regex.Replace(input, evalutor)

    member __.Replace (input, (replacement : string), count) =
        __.Regex.Replace(input, replacement, count)

    member __.Replace (input, (evalutor : MatchEvaluator), count) =
        __.Regex.Replace(input, evalutor, count)

    member __.Replace (input, (replacement : string), count, startAt) =
        __.Regex.Replace(input, replacement, count, startAt)

    member __.Replace (input, (evalutor : MatchEvaluator), count, startAt) =
        __.Regex.Replace(input, evalutor, count, startAt)

    member __.RightToLeft =
        __.Regex.RightToLeft
            
    member __.Source
        with get() = _source
        and set(value) = _source <- value

    member __.Suffixes
        with get() = _suffixes
        and set(value) = _suffixes <- value

    member  __.Split input =
        __.Regex.Split input

    member  __.Split (input, count) =
        __.Regex.Split(input, count)

    member  __.Split (input, count, startAt) =
        __.Regex.Split(input, count, startAt)

let firstMatch regularExpression input = 
    let regex = new Regex(regularExpression)
    FsMatch (regex, Regex.Match(input, regularExpression))

let firstMatchOpt regularExpression regexOptions input = 
    let regex = new Regex(regularExpression)
    FsMatch (regex, Regex.Match(input, regularExpression, regexOptions))

let matchAt regularExpression startAt input = 
    let regex = new Regex(regularExpression)
    FsMatch (regex, regex.Match (input, startAt))

let matchAtOpt regularExpression regexOptions startAt input = 
    let regex = new Regex(regularExpression, regexOptions)
    FsMatch (regex, regex.Match (input, startAt))

let matchAtFor regularExpression startAt length input = 
    let regex = new Regex(regularExpression)
    FsMatch (regex, regex.Match (input, startAt, length))

let matchAtForOpt regularExpression regexOptions startAt length input = 
    let regex = new Regex(regularExpression, regexOptions)
    FsMatch (regex, regex.Match (input, startAt, length))

let matches regularExpression input = 
    let regex = new Regex(regularExpression)
    (regex, regex.Matches(input))
    ||> Common.arrayFromMatches

let matchesOpt regularExpression regexOptions input = 
    let regex = new Regex(regularExpression, regexOptions)
    (regex, regex.Matches(input))
    ||> Common.arrayFromMatches

let matchesAt regularExpression startAt input = 
    let regex = new Regex(regularExpression)
    (regex, regex.Matches(input, startAt))
    ||>Common.arrayFromMatches

let matchesAtOpt regularExpression regexOptions startAt input = 
    let regex = new Regex(regularExpression, regexOptions)
    (regex, regex.Matches(input, startAt))
    ||> Common.arrayFromMatches

let replace regularExpression (replacement : string) input =
    Regex.Replace(input, regularExpression, replacement)

let replaceOpt regularExpression regexOptions (replacement : string) input =
    Regex.Replace(input, regularExpression, replacement, regexOptions)

let replaceByMatch regularExpression (evalutor : MatchEvaluator) input =
    Regex.Replace(input, regularExpression, evalutor)

let replaceByMatchOpt regularExpression regexOptions (evalutor : MatchEvaluator) input =
    Regex.Replace(input, regularExpression, evalutor, regexOptions)

let replaceMaxTimes regularExpression (replacement : string) count input =
    (new Regex(regularExpression)).Replace(input, replacement, count )

let replaceMaxTimesOpt regularExpression regexOptions (replacement : string) count input =
    (new Regex(regularExpression, regexOptions)).Replace(input, replacement, count )

let replaceByMatchMaxTimes regularExpression (evalutor : MatchEvaluator) count input =
    (new Regex(regularExpression)).Replace(input, evalutor, count)

let replaceByMatchMaxTimesOpt regularExpression regexOptions (evalutor : MatchEvaluator) count input =
    (new Regex(regularExpression, regexOptions)).Replace(input, evalutor, count)

let replaceMaxTimesStartAt regularExpression (replacement : string) count startAt input =
    (new Regex(regularExpression)).Replace(input, replacement, count, startAt)

let replaceMaxTimesStartAtOpt regularExpression regexOptions (replacement : string) count startAt input =
    (new Regex(regularExpression, regexOptions)).Replace(input, replacement, count, startAt)

let replaceByMatchMaxTimesStartAt regularExpression (evalutor : MatchEvaluator) count startAt input =
    (new Regex(regularExpression)).Replace(input, evalutor, count, startAt)

let replaceByMatchMaxTimesStartAtOpt regularExpression regexOptions (evalutor : MatchEvaluator) count startAt input =
    (new Regex(regularExpression, regexOptions)).Replace(input, evalutor, count, startAt)

let split regularExpression input =
    Regex.Split(input, regularExpression)

let splitOpt regularExpression regexOptions input =
    Regex.Split(input, regularExpression, regexOptions)

let splitMaxTimes regularExpression count input =
    (new Regex(regularExpression)).Split (input, count)

let splitMaxTimesOpt regularExpression regexOptions count input =
    (new Regex(regularExpression, regexOptions)).Split (input, count)

let splitMaxTimesStartAt regularExpression count startAt input =
    (new Regex(regularExpression)).Split (input, count, startAt)

let splitMaxTimesStartAtOpt regularExpression regexOptions count startAt input =
    (new Regex(regularExpression, regexOptions)).Split (input, count, startAt)

let capture regularExpression (groupName : string) input =
    let match' = (new Regex(regularExpression)).Match input 
    match'.Groups.[groupName].Value

let captureOpt regularExpression regexOptions (groupName : string) input =
    let match' = (new Regex(regularExpression, regexOptions)).Match input 
    match'.Groups.[groupName].Value

let captureGroup regularExpression (groupName : string) input =
    let regex = new Regex(regularExpression)

    let match' = regex.Match input 
    (groupName, match'.Groups.[groupName])
    |> FsGroup

let captureGroupOpt regularExpression regexOptions (groupName : string) input =
    let regex = new Regex(regularExpression, regexOptions)

    let match' = regex.Match input 
    (groupName, match'.Groups.[groupName])
    |> FsGroup

let groupNames regularExpression =
    (new Regex(regularExpression)).GetGroupNames()

let groupNumbers regularExpression =
    (new Regex(regularExpression)).GetGroupNumbers()

let isMatch regularExpression input =
    Regex.IsMatch(input, regularExpression)

let isMatchOpt regularExpression regexOptions input =
    Regex.IsMatch(input, regularExpression, regexOptions)

let isMatchAt regularExpression startAt input =
    (new Regex(regularExpression)).IsMatch (input, startAt)

let isMatchAtOpt regularExpression regexOptions startAt input =
    (new Regex(regularExpression, regexOptions)).IsMatch (input, startAt)