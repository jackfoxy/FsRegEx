///Common regular expressions in FsRegEx type.
module CommonFsRegEx 

open FsRegEx

///Valid email string. (Note that RE email validation is ill-advised.)
val Email : FsRegEx

///Valid Url string.
val Url : FsRegEx