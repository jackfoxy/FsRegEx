/// Types and functions supporting composable F# regular expressions.
module FsRegEx

open System
open System.Text.RegularExpressions

///Unicode enumerations
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
        with
        override ToString : unit -> string

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
        with
        override ToString : unit -> string

///Composable wrapping type for System.Text.RegularExpressions.Group.
type FsGroup =
  class
    new : name:string * group:Group -> FsGroup
    ///Array of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the RegexOptions.RightToLeft option). The collection may have zero or more items.
    member Captures : unit -> Capture []
    override Equals : yobj:obj -> bool
    override GetHashCode : unit -> int
    override ToString : unit -> string
    ///The underlying System.Text.RegularExpressions.Group. Represents the results from a single capturing group.
    member Group : System.Text.RegularExpressions.Group
    ///The position in the original string where the first character of the captured substring is found.
    member Index : int
    ///Returns the length of the captured substring.
    member Length : int
    ///Returns name of capture group.
    member Name : string
    ///Returns a value indicating whether the match is successful.
    member Success : bool
    ///Returns the captured substring from the input string.
    member Value : string
  end

///Composable wrapping type for System.Text.RegularExpressions.Match. Represents the results from a single regular expression match.
type FsMatch =
  class
    new : regex:Regex * fsMatch:Match -> FsMatch
    ///Array of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the RegexOptions.RightToLeft option). The collection may have zero or more items.
    member Captures : unit -> Capture []
    override Equals : yobj:obj -> bool
    override GetHashCode : unit -> int
    /// Capturing groups matched by regular expression.
    member Groups : unit -> FsGroup []
    ///Returns the expansion of the specified replacement pattern.
    member Result : replacement:string -> string
    override ToString : unit -> string
    ///The position in the original string where the first character of the captured substring is found.
    member Index : int
    ///Returns the length of the captured substring.
    member Length : int
    ///Wrapped System.Text.RegularExpressions.Match. Represents the results from a single regular expression match.
    member Match : Match
    ///Returns a value indicating whether the match is successful.
    member Success : bool
    ///Returns the captured substring from the input string.
    member Value : string
  end

///Composable immutable wrapping type for System.Text.RegularExpressions.Regex.
type FsRegEx =
    new : unit -> FsRegEx
    new : regexOptions : RegexOptions -> FsRegEx
    new : regularExpression : string -> FsRegEx
    new : regularExpression : string * regexOptions : RegexOptions -> FsRegEx
    new : regularExpression : string * regexOptions : RegexOptions * matchTimeout : TimeSpan -> FsRegEx

    ///Match and select groupname value.
    member Capture : input : string -> string -> string

    member internal Clone : regexOptions : RegexOptions option -> FsRegEx
    member internal Prefixes : string with get, set 
    member internal Source : string with get, set
    member internal Suffixes : string with get, set

    ///Gets the group name that corresponds to the specified group number.
    member GroupNameFromNumber : n : int -> string option

    ///Returns an array of capturing group names for the regular expression.
    member GroupNames : unit -> array<string>

    ///Returns the group number that corresponds to the specified group name.
    member GroupNumberFromName : groupName : string -> int option

    ///Returns an array of capturing group numbers that correspond to group names in an array.
    member GroupNumbers : unit -> array<int>

    ///Indicates whether the regular expression finds a match in the input string.
    member IsMatch : input : string -> bool

    ///Indicates whether the regular expression finds a match in the input string beginning at the specified starting position.
    member IsMatch : input : string * startAt : int -> bool

    ///Searches the specified input string for the first occurrence of the regular expression.
    member Match : input : string -> FsMatch

    ///Searches the specified input string for the first occurrence of the regular expression beginning at the specified starting position.
    member Match : input : string * startAt : int -> FsMatch

    ///Searches the specified input string for the first occurrence of the regular expression beginning at the starting position for the length.
    member Match : input : string * startAt : int * length : int -> FsMatch

    ///Searches the specified input string for all occurrences of a regular expression.
    member Matches : input : string -> FsMatch[]

    ///Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position.
    member Matches : input : string * startAt : int -> FsMatch[]

    ///Gets the time-out interval of the current instance.
    member MatchTimeout : TimeSpan

    ///The underlying System.Text.RegularExpressions.Regex
    member Regex : Regex

    ///Enumerated values to use to set regular expression options.
    member RegexOptions : RegexOptions

    ///In input string replaces all strings that match regular expression pattern with replacement string.
    member Replace : input : string * replacement : string -> string

    ///In input string replaces all strings that match regular expression with string returned by MatchEvaluator delegate.
    member Replace : input : string * evalutor : MatchEvaluator -> string

    ///In input string replaces a specified maximum number of strings that match regular expression with replacement string.
    member Replace : input : string * replacement : string * count : int -> string

    ///In input string replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
    member Replace : input : string * evalutor : MatchEvaluator * count : int -> string

    ///In input string beginning at start at position replaces a specified maximum number of strings that match regular expression with replacement string.
    member Replace : input : string * replacement : string * count : int * startAt : int -> string

    ///In input string beginning at start at position replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
    member Replace : input : string * evalutor : MatchEvaluator * count : int * startAt : int -> string

    ///Indicates whether the regular expression searches from right to left.
    member RightToLeft : bool

    ///Splits input string into an array of substrings at the positions defined by regular expression.
    member Split : input : string -> array<string>

    ///Splits input string a specified maximum number of times into an array of substrings at the positions defined by regular expression.
    member Split : input : string * count : int -> array<string>

    ///Splits input string, begining at start position, a specified maximum number of times into an array of substrings at the positions defined by regular expression.
    member Split : input : string * count : int * startAt : int -> array<string>

///Searches the specified input string for the first occurrence of the regular expression.
val firstMatch : regularExpression : string -> input : string -> FsMatch

///Searches the specified input string for the first occurrence of the regular expression with Regex options.
val firstMatchOpt : regularExpression : string -> regexOptions : RegexOptions -> input : string -> FsMatch

///Searches the specified input string for the first occurrence of the regular expression beginning at the specified starting position.
val matchAt : regularExpression : string -> startAt : int -> input : string -> FsMatch

///Searches the specified input string for the first occurrence of the regular expression beginning at the specified starting position with Regex options.
val matchAtOpt : regularExpression : string -> regexOptions : RegexOptions -> startAt : int -> input : string -> FsMatch

///Searches the specified input string for the first occurrence of the FsRegEx beginning at the starting position for the length.
val matchAtFor : regularExpression : string -> startAt : int ->  length : int -> input : string -> FsMatch

///Searches the specified input string for the first occurrence of the FsRegEx beginning at the starting position for the length with Regex options.
val matchAtForOpt : regularExpression : string -> regexOptions : RegexOptions -> startAt : int ->  length : int -> input : string -> FsMatch

///Searches the specified input string for all occurrences of a regular expression with Regex options.
val matches : regularExpression : string -> input : string -> FsMatch[]

///Searches the specified input string for all occurrences of a regular expression.
val matchesOpt : regularExpression : string -> regexOptions : RegexOptions -> input : string -> FsMatch[]

///Searches the input string for all occurrence of a regular expression, beginning at the specified starting position.
val matchesAt : regularExpression : string -> startAt : int -> input : string -> FsMatch[]

///Searches the input string for all occurrence of a regular expression, beginning at the specified starting position with Regex options.
val matchesAtOpt : regularExpression : string -> regexOptions : RegexOptions -> startAt : int -> input : string -> FsMatch[]

///In input string replaces all strings that match regular expression pattern with replacement string.
val replace : regularExpression : string -> replacement : string -> input : string -> string

///In input string replaces all strings that match regular expression with string returned by MatchEvaluator delegate.
val replaceByMatch : regularExpression : string -> evalutor : MatchEvaluator -> input : string -> string

///In input string replaces all strings that match regular expression with string returned by MatchEvaluator delegate with Regex options.
val replaceByMatchOpt : regularExpression : string -> regexOptions : RegexOptions -> evalutor : MatchEvaluator -> input : string -> string

///In input string replaces a specified maximum number of strings that match regular expression with replacement string.
val replaceMaxTimes : regularExpression : string -> replacement : string -> count : int -> input : string -> string

///In input string replaces a specified maximum number of strings that match regular expression with replacement string with Regex options.
val replaceMaxTimesOpt : regularExpression : string -> regexOptions : RegexOptions -> replacement : string -> count : int -> input : string -> string

///In input string replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
val replaceByMatchMaxTimes : regularExpression : string -> evalutor : MatchEvaluator -> count : int -> input : string -> string

///In input string replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate with Regex options.
val replaceByMatchMaxTimesOpt : regularExpression : string -> regexOptions : RegexOptions -> evalutor : MatchEvaluator -> count : int -> input : string -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match regular expression with replacement string.
val replaceMaxTimesStartAt : regularExpression : string -> replacement : string -> count : int -> startAt : int -> input : string -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match regular expression with replacement string with Regex options.
val replaceMaxTimesStartAtOpt : regularExpression : string -> regexOptions : RegexOptions -> replacement : string -> count : int -> startAt : int -> input : string -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
val replaceByMatchMaxTimesStartAt : regularExpression : string -> evalutor : MatchEvaluator -> count : int -> startAt : int -> input : string -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate with Regex options.
val replaceByMatchMaxTimesStartAtOpt : regularExpression : string -> regexOptions : RegexOptions -> evalutor : MatchEvaluator -> count : int -> startAt : int -> input : string -> string

///Splits input string into an array of substrings at the positions defined by regular expression.
val split : regularExpression : string -> input : string -> array<string>

///Splits input string into an array of substrings at the positions defined by regular expression with Regex options.
val splitOpt : regularExpression : string -> regexOptions : RegexOptions -> input : string -> array<string>

///Splits input string a specified maximum number of times into an array of substrings at the positions defined by regular expression.
val splitMaxTimes : regularExpression : string -> count : int -> input : string -> array<string>

///Splits input string a specified maximum number of times into an array of substrings at the positions defined by regular expression with Regex options.
val splitMaxTimesOpt : regularExpression : string -> regexOptions : RegexOptions -> count : int -> input : string -> array<string>

///Splits input string, begining at start position, a specified maximum number of times into an array of substrings at the positions defined by regular expression.
val splitMaxTimesStartAt : regularExpression : string -> count : int -> startAt : int -> input : string -> array<string>

///Splits input string, begining at start position, a specified maximum number of times into an array of substrings at the positions defined by regular expression with Regex options.
val splitMaxTimesStartAtOpt : regularExpression : string -> regexOptions : RegexOptions -> count : int -> startAt : int -> input : string -> array<string>

///Match and select groupname value.
val capture : regularExpression : string  -> groupName : string -> input : string -> string

///Match and select groupname value with Regex options.
val captureOpt : regularExpression : string  -> regexOptions : RegexOptions -> groupName : string -> input : string -> string

///Match and select group.
val captureGroup : regularExpression : string -> groupName : string -> input : string -> FsGroup

///Match and select group with Regex options.
val captureGroupOpt : regularExpression : string -> regexOptions : RegexOptions -> groupName : string -> input : string -> FsGroup

///Returns an array of capturing group names for the regular expression.
val groupNames : regularExpression : string -> array<string>

///Returns an array of capturing group numbers that correspond to group names in an array.
val groupNumbers : regularExpression : string -> array<int>

///Indicates whether the regular expression finds a match in the input string.
val isMatch : regularExpression : string  -> input : string -> bool

///Indicates whether the regular expression finds a match in the input string with Regex options.
val isMatchOpt : regularExpression : string  -> regexOptions : RegexOptions -> input : string -> bool

///Indicates whether the regular expression finds a match in the input string beginning at the specified starting position.
val isMatchAt : regularExpression : string -> startAt : int -> input : string -> bool

///Indicates whether the regular expression finds a match in the input string beginning at the specified starting position with Regex options.
val isMatchAtOpt : regularExpression : string -> regexOptions : RegexOptions -> startAt : int -> input : string -> bool
