(**
On the Futility of Email Regex Validation
=========================================

Using regular expressions to validate email addresses is notoriously error prone. 
The complexity of the address format specification makes constructing a machine readable
grammar correctly discerning the difference between valid and invalid addresses hard.

Evolution of the Internet Message Format
----------------------------------------

[RFC 561](http://www.ietf.org/rfc/rfc0561.txt) Standardizing Network Mail Headers, September 5, 1973

[RFC 680](http://www.ietf.org/rfc/rfc0680.txt) Message Transmission Protocol, April 30, 1975

[RFC 724](http://www.ietf.org/rfc/rfc0724.txt) Proposed Official Standard for the Format of ARPA Network Messages, May 12, 1977

[RFC 733](http://www.ietf.org/rfc/rfc0733.txt) STANDARD FOR THE FORMAT OF ARPA NETWORK TEXT MESSAGES, November 21, 1977

Obsoletes:  RFC 561, RFC 680, RFC 724 

[RFC 822](http://www.ietf.org/rfc/rfc0822.txt) STANDARD FOR THE FORMAT OF ARPA INTERNET TEXT MESSAGES, August 13, 1982

Obsoletes:  RFC 733  

[RFC 2822](http://www.ietf.org/rfc/rfc2822.txt) Internet Message Format, April 2001

Obsoletes: RFC 822

[RFC 5322] (http://www.ietf.org/rfc/rfc5322.txt) Internet Message Format, October 2008

Obsoletes: RFC 2822

Testing Regular Expressions that Parse Email Addresses 
------------------------------------------------------

The tests are derived from a suite of tests available at [RFC 822 Email Address Parser in PHP](http://code.iamcal.com/php/rfc822/),
which we have converted to run under FsRegEx. The PHP parser author claims a much better success rate than we were able to achieve with
any regular expression, although still not perfect. The site also provides more background on the "muddy" internet specification. 

The suite consists of 280 validataion tests, which we converted to [Expecto](https://github.com/haf/expecto) tests 
[here](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/RFC822.fs). Run with the 
[Email.Tests](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests.fsproj) console app.

We chose some email parsing regular expressions available on the internet for testing:

[Simple](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/SimpleEmail.fs), from [emailregex.com](http://emailregex.com/).

[Moderate](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/ModerateEmail.fs), from [emailregex.com](http://emailregex.com/).

[Complex](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/ComplexEmail.fs), from [emailregex.com](http://emailregex.com/).

[Ultimate](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/Ultimate.fs), from
[Mail::RFC822::Address: regexp-based address validation](http://www.ex-parrot.com/pdw/Mail-RFC822-Address.html). This regular expression was machine generated.

Note that [emailregex.com](http://emailregex.com/) headlines "Email Address Regular Expression That 99.99% Works". The site offers many different
email address regular expressions. Apparently we never found the one that works 99.99% on our test suite.

We also tested a much simpler (non-regular expression) checker, that simply tests string for a single ampersand that is not wrapped in quotes and is not either
the first or last character in the string [here](http://github.com/VerbalExpressions/FSharpVerbalExpressions/blob/master/tests/Email.Tests/OneAmp.fs).

Results
-------

Simple: 

132 passed, 148 failed, 98 false negatives

Moderate:

190 passed, 90 failed, 72 false negatives

Complex:

172 passed, 108 failed, 60 false negatives

Ultimate:

170 passed, 110 failed, 11 false negatives

One Ampersand:

162 passed, 118 failed, 1 false negative (test166)

Conclusion
----------

The evolution of the Internet Email Address specification and way it is documented make it impossible to settle on a completely deterministic grammar covering all cases.
With that in mind it is already impossible to make any complete parser, let alone one based on a regular expression. That being said, the test results do not reflect
the likelihood of meeting any of the 280 test cases in the wild. 

Simply determining if there is a correctly placed ampersand is perhaps the best filter for most practical purposes, given the importance of this criterion.
(Note that even our attempt at rejecting multiple ampersands resulted in a false negative.)
*)