/// Functions supporting composable verbal expression natural language regular expressions.
module VerbalExpressions

open System
open System.Text.RegularExpressions
open FsRegEx

///Searches the specified input string for the first occurrence of the FsRegEx.
val firstMatch : input : string -> fsRegEx : FsRegEx -> FsMatch

///Searches the specified input string for the first occurrence of the FsRegEx beginning at the specified starting position.
val matchAt : input : string -> startAt : int -> fsRegEx : FsRegEx -> FsMatch

///Searches the specified input string for the first occurrence of the FsRegEx beginning at the starting position for the length.
val matchAtFor : input : string -> startAt : int ->  length : int -> fsRegEx : FsRegEx -> FsMatch

///Searches the specified input string for all occurrences of a regular expression.
val matches : input : string -> fsRegEx : FsRegEx -> FsMatch[]

///Searches the input string for all occurrence of a regular expression, beginning at the specified starting position.
val matchesAt : input : string -> startAt : int -> fsRegEx : FsRegEx -> FsMatch[]

///In input string replaces all strings that match regular expression pattern with replacement string.
val replace : input : string -> replacement : string -> fsRegEx : FsRegEx -> string

///In input string replaces all strings that match regular expression with string returned by MatchEvaluator delegate.
val replaceByMatch : input : string -> evalutor : MatchEvaluator -> fsRegEx : FsRegEx -> string

///In input string replaces a specified maximum number of strings that match regular expression with replacement string.
val replaceMaxTimes : input : string -> replacement : string -> count : int -> fsRegEx : FsRegEx -> string

///In input string replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
val replaceByMatchMaxTimes : input : string -> evalutor : MatchEvaluator -> count : int -> fsRegEx : FsRegEx -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match regular expression with replacement string.
val replaceMaxTimesStartAt : input : string -> replacement : string -> count : int -> startAt : int -> fsRegEx : FsRegEx -> string

///In input string beginning at start at position replaces a specified maximum number of strings that match a regular expression with string returned by MatchEvaluator delegate.
val replaceByMatchMaxTimesStartAt : input : string -> evalutor : MatchEvaluator -> count : int -> startAt : int -> fsRegEx : FsRegEx -> string

///Splits input string into an array of substrings at the positions defined by regular expression.
val split : input : string -> fsRegEx : FsRegEx -> array<string>

///Splits input string a specified maximum number of times into an array of substrings at the positions defined by regular expression.
val splitMaxTimes : input : string -> count : int -> fsRegEx : FsRegEx -> array<string>

///Splits input string, begining at start position, a specified maximum number of times into an array of substrings at the positions defined by regular expression.
val splitMaxTimesStartAt : input : string -> count : int -> startAt : int -> fsRegEx : FsRegEx -> array<string>

///Return new FsRegEx with new RegExOptions.
val resetRegexOptions : newRegexOptions : RegexOptions option -> fsRegEx : FsRegEx -> FsRegEx

///Match and select groupname value.
val capture : input : string -> groupName : string -> fsRegEx : FsRegEx -> string

///Indicates whether the regular expression finds a match in the input string.
val isMatch : value : string -> fsRegEx : FsRegEx -> bool

///Indicates whether the regular expression finds a match in the input string beginning at the specified starting position.
val isMatchAt : value : string -> startAt : int -> fsRegEx : FsRegEx -> bool

///Returns an array of capturing group names for the regular expression.
val groupNames : fsRegEx : FsRegEx -> array<string>

///Returns an array of capturing group numbers that correspond to group names in an array.
val groupNumbers : fsRegEx : FsRegEx -> array<int>

///Value of regular expression.
val toString : fsRegEx : FsRegEx -> string

///Mark the expression to start at the beginning of the line; ^
val startOfLine : fsRegEx : FsRegEx -> FsRegEx

///Mark the expression to end at the last character of the line; $
val endOfLine : fsRegEx : FsRegEx -> FsRegEx

///Append unescaped literal expression to the expression.
val add : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Append escaped literal expression to the expression. (Same as find.)
val then' : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Append escaped literal expression to the expression. (Same as then'.)
val find : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Add escaped string to the expression that might appear once (or not); (value)?
val maybe : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Zero or more of any character; (.*)
val anything : fsRegEx : FsRegEx -> FsRegEx

///Zero or more of any character except escaped character(s); ([^value]*)
val anythingBut : value : string -> fsRegEx : FsRegEx -> FsRegEx

///One or more of any character; (.+)
val something : fsRegEx : FsRegEx -> FsRegEx
    
///One or more of any character except escaped character; ([^%s]+)
val somethingBut : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Refers to nth occurence of capturing groups; \ordinal
val backReference : ordinal : int -> fsRegEx : FsRegEx -> FsRegEx

///Refers to named capturing group; \k<groupname>
val namedBackReference : groupname : string -> fsRegEx : FsRegEx -> FsRegEx

///Add expression to match a range (or multiple ranges), unescaped values.
val range : collection : seq<obj> -> fsRegEx : FsRegEx -> FsRegEx

///Matches universal line break expression; \n
val lineBreak : fsRegEx : FsRegEx -> FsRegEx

///Same as lineBreak; \n
val br : fsRegEx : FsRegEx ->  FsRegEx

///Matches a tab character; \t
val tab : fsRegEx : FsRegEx -> FsRegEx

///Matches any white-space character; \s
val whiteSpace : fsRegEx : FsRegEx -> FsRegEx

///Matches any non-white-space character; \S.
val nonWhiteSpace : fsRegEx : FsRegEx -> FsRegEx

///Expression to match a word; \w+
val word : fsRegEx : FsRegEx -> FsRegEx

///Matches any word character; \w
val wordCharacter  : fsRegEx : FsRegEx -> FsRegEx

///Matches any nonword character; \W
val nonWordCharacter  : fsRegEx : FsRegEx -> FsRegEx

///Matches any decimal digit; \d
val digit  : fsRegEx : FsRegEx -> FsRegEx

///Matches any nondigit; \D
val nonDigit  : fsRegEx : FsRegEx -> FsRegEx

///Matches any single character included in the specified set of escaped characters; [value]
val anyOf : value : string -> fsRegEx : FsRegEx -> FsRegEx

///Same as anyOf.
val any : value : string -> fsRegEx : FsRegEx ->  FsRegEx

///The escaped expression 1 or more times; (value)+
val multiple : value : string -> fsRegEx : FsRegEx -> FsRegEx

///The FsRegEx 1 or more times; (sourceFsRegEx)+
val multipleFsRegEx : sourceFsRegEx : FsRegEx -> fsRegEx : FsRegEx -> FsRegEx

///Or's the regular expression to the regular expression in a FsRegEx.
val or' : regularExpression : string -> fsRegEx : FsRegEx -> FsRegEx

///Return new FsRegEx of two FsRegExs or'ed.
val fsRegExOrFsRegEx : regexOptions : RegexOptions -> fsRegEx : FsRegEx -> fsRegEx : FsRegEx -> FsRegEx

///Begin capture group; (
val beginCapture : fsRegEx : FsRegEx -> FsRegEx

///Begin named capture group ;(?<groupName>
val beginCaptureNamed : groupName : string -> fsRegEx : FsRegEx -> FsRegEx

///End capture group; )
val endCapture : fsRegEx : FsRegEx -> FsRegEx

///Repeat previous exact number of times; {n}
val repeatPrevious : n : int -> FsRegEx -> FsRegEx

///Repeat previous number of times between n and m; {n,m}
val repeatBetweenPrevious : n : int ->  m : int -> FsRegEx -> FsRegEx

///Matches a Unicode character by using hexadecimal representation  (exactly four digits, as represented by nnnn); \unnnn
val unicode : nnnn : string -> FsRegEx -> FsRegEx

///Matches any single character in the Unicode general category; \p{name}
val unicodeCategory : category : Unicode.UnicodeGeneralCategory -> FsRegEx -> FsRegEx

///Matches any single character that is not in the Unicode general category; \P{name}
val notUnicodeCategory : category : Unicode.UnicodeGeneralCategory -> FsRegEx -> FsRegEx

///Matches any single character in the supported named block; \p{name}
val namedBlock : name : Unicode.SupportedNamedBlock -> FsRegEx -> FsRegEx

///Matches any single character that is not in the supported named block; \P{name}
val notNamedBlock : name : Unicode.SupportedNamedBlock -> FsRegEx -> FsRegEx

///Executes a function on a FsRegEx.
val iter : f : (FsRegEx -> unit) -> fsRegEx : FsRegEx -> unit

///Transforms a FsRegEx  with a mapping function
val map : f : (FsRegEx -> FsRegEx) -> fsRegEx : FsRegEx -> FsRegEx

///Applies a function to a FsRegEx and a state returning a state
val fold : f : ('State -> FsRegEx -> 'State) -> state : 'State -> fsRegEx : FsRegEx -> 'State

///Applies a function to a FsRegEx and a state returning a state
val foldBack : f : (FsRegEx -> 'State -> 'State) -> fsRegEx : FsRegEx -> state : 'State -> 'State

///Evaluates the boolean function on the FsRegEx
val exists : f : (FsRegEx -> bool) -> fsRegEx : FsRegEx -> bool
