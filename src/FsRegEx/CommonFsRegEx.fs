module CommonFsRegEx 

open FsRegEx

/// From http://emailregex.com/
let Email = new FsRegEx(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}")

let Url = new FsRegEx(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[^-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[^-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w_]*)?\??(?:[-\+=&;%@.\w-_]*)#?‌​(?:[\w]*))?)")

    

