⁄
s/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/ApplicationDI.cs
	namespace 	
Application
 
{ 
public 

static 
class 
ApplicationDI %
{ 
public		 
static		 
void		 
AddApplication		 )
(		) *
this		* .
IServiceCollection		/ A
services		B J
)		J K
{		L M
services

 
.

 

AddMediatR

 
(

  
Assembly

  (
.

( ) 
GetExecutingAssembly

) =
(

= >
)

> ?
)

? @
;

@ A
} 	
} 
} ≤
ã/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateAuthorCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
CreateAuthorCommand $
:% &
IRequest' /
</ 0
Guid0 4
>4 5
{ 
public 
string 
? 
	FirstName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
? 
LastName 
{  !
get" %
;% &
set' *
;* +
}, -
}		 
}

 ß
í/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateAuthorCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class &
CreateAuthorCommandHandler +
:, -
IRequestHandler. =
<= >
CreateAuthorCommand> Q
,Q R
GuidS W
>W X
{ 
private		 
readonly		 
IAuthorRepository		 *

repository		+ 5
;		5 6
public &
CreateAuthorCommandHandler )
() *
IAuthorRepository* ;

repository< F
)F G
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
CreateAuthorCommand' :
request; B
,B C
CancellationTokenD U
cancellationTokenV g
)g h
{ 	
var 
author 
= 
new 
Author #
{ 
	FirstName 
= 
request #
.# $
	FirstName$ -
,- .
LastName 
= 
request "
." #
LastName# +
} 
; 
await 

repository 
. 
AddAsync %
(% &
author& ,
), -
;- .
return 
author 
. 
Id 
; 
} 	
} 
} ï

â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateBookCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
CreateBookCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
string 
? 
Title 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Rating 
{ 
get  #
;# $
set% (
;( )
}* +
public		 
string		 
?		 
Description		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
DateTime

 
PublicationDate

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
public 
Uri 
? 
ImageUri 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} „
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateBookCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
CreateBookCommandHandler )
:* +
IRequestHandler, ;
<; <
CreateBookCommand< M
,M N
GuidO S
>S T
{ 
private		 
readonly		 
IBookRepository		 (

repository		) 3
;		3 4
public $
CreateBookCommandHandler '
(' (
IBookRepository( 7

repository8 B
)B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
CreateBookCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
book 
= 
new 
Book 
{ 
Title 
= 
request 
.  
Title  %
,% &
Rating 
= 
request  
.  !
Rating! '
,' (
Description 
= 
request %
.% &
Description& 1
,1 2
PublicationDate 
=  !
request" )
.) *
PublicationDate* 9
,9 :
ImageUri 
= 
request "
." #
ImageUri# +
} 
; 
await 

repository 
. 
AddAsync %
(% &
book& *
)* +
;+ ,
return 
book 
. 
Id 
; 
} 	
} 
} Ä
ä/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateGenreCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
CreateGenreCommand #
:$ %
IRequest& .
<. /
Guid/ 3
>3 4
{ 
public 
string 
? 
Name 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		 §
ë/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateGenreCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class %
CreateGenreCommandHandler *
:+ ,
IRequestHandler- <
<< =
CreateGenreCommand= O
,O P
GuidQ U
>U V
{ 
private		 
readonly		 
IGenreRepository		 )

repository		* 4
;		4 5
public %
CreateGenreCommandHandler (
(( )
IGenreRepository) 9

repository: D
)D E
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
CreateGenreCommand' 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 
genre 
= 
new 
Genre !
{ 
Name 
= 
request 
. 
Name #
} 
; 
await 

repository 
. 
AddAsync %
(% &
genre& +
)+ ,
;, -
return 
genre 
. 
Id 
; 
} 	
} 
} ≠
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateUserCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
CreateUserCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
string 
? 
Username 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
? 
Password 
{  !
get" %
;% &
set' *
;* +
}, -
}		 
}

 è
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/CreateUserCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
CreateUserCommandHandler )
:* +
IRequestHandler, ;
<; <
CreateUserCommand< M
,M N
GuidO S
>S T
{ 
private		 
readonly		 
IUserRepository		 (

repository		) 3
;		3 4
public $
CreateUserCommandHandler '
(' (
IUserRepository( 7

repository8 B
)B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
CreateUserCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
user 
= 
new 
User 
{ 
Username 
= 
request "
." #
Username# +
,+ ,
Password 
= 
request "
." #
Password# +
} 
; 
await 

repository 
. 
AddAsync %
(% &
user& *
)* +
;+ ,
return 
user 
. 
Id 
; 
} 	
} 
} ·
ã/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteAuthorCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
DeleteAuthorCommand $
:% &
IRequest' /
</ 0
Guid0 4
>4 5
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DeleteAuthorCommand "
(" #
Guid# '
id( *
)* +
{		 	
this

 
.

 
Id

 
=

 
id

 
;

 
} 	
} 
} ’
í/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteAuthorCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class &
DeleteAuthorCommandHandler +
:, -
IRequestHandler. =
<= >
DeleteAuthorCommand> Q
,Q R
GuidS W
>W X
{ 
private		 
readonly		 
IAuthorRepository		 *

repository		+ 5
;		5 6
public &
DeleteAuthorCommandHandler )
() *
IAuthorRepository* ;

repository< F
)F G
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
DeleteAuthorCommand' :
request; B
,B C
CancellationTokenD U
cancellationTokenV g
)g h
{ 	
var 
author 
= 

repository #
.# $
GetByIdAsync$ 0
(0 1
request1 8
.8 9
Id9 ;
); <
.< =
Result= C
;C D
if 
( 
author 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ G
)G H
;H I
} 
await 

repository 
. 
DeleteAsync (
(( )
author) /
)/ 0
;0 1
return 
author 
. 
Id 
; 
} 	
} 
} €
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteBookCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
DeleteBookCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DeleteBookCommand  
(  !
Guid! %
id& (
)( )
{		 	
this

 
.

 
Id

 
=

 
id

 
;

 
} 	
} 
} ø
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteBookCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
DeleteBookCommandHandler )
:* +
IRequestHandler, ;
<; <
DeleteBookCommand< M
,M N
GuidO S
>S T
{ 
private 
readonly 
IBookRepository (

repository) 3
;3 4
public

 $
DeleteBookCommandHandler

 '
(

' (
IBookRepository

( 7

repository

8 B
)

B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
DeleteBookCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
book 
= 

repository !
.! "
GetByIdAsync" .
(. /
request/ 6
.6 7
Id7 9
)9 :
.: ;
Result; A
;A B
if 
( 
book 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
await 

repository 
. 
DeleteAsync (
(( )
book) -
)- .
;. /
return 
book 
. 
Id 
; 
} 	
} 
} ﬁ
ä/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteGenreCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
DeleteGenreCommand #
:$ %
IRequest& .
<. /
Guid/ 3
>3 4
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DeleteGenreCommand !
(! "
Guid" &
id' )
)) *
{		 	
this

 
.

 
Id

 
=

 
id

 
;

 
} 	
} 
}  
ë/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteGenreCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class %
DeleteGenreCommandHandler *
:+ ,
IRequestHandler- <
<< =
DeleteGenreCommand= O
,O P
GuidQ U
>U V
{ 
private 
readonly 
IGenreRepository )

repository* 4
;4 5
public

 %
DeleteGenreCommandHandler

 (
(

( )
IGenreRepository

) 9

repository

: D
)

D E
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
DeleteGenreCommand' 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 
genre 
= 

repository "
." #
GetByIdAsync# /
(/ 0
request0 7
.7 8
Id8 :
): ;
.; <
Result< B
;B C
if 
( 
genre 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ F
)F G
;G H
} 
await 

repository 
. 
DeleteAsync (
(( )
genre) .
). /
;/ 0
return 
genre 
. 
Id 
; 
} 	
} 
} €
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteUserCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
DeleteUserCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DeleteUserCommand  
(  !
Guid! %
id& (
)( )
{		 	
this

 
.

 
Id

 
=

 
id

 
;

 
} 	
} 
} ø
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/DeleteUserCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
DeleteUserCommandHandler )
:* +
IRequestHandler, ;
<; <
DeleteUserCommand< M
,M N
GuidO S
>S T
{ 
private 
readonly 
IUserRepository (

repository) 3
;3 4
public

 $
DeleteUserCommandHandler

 '
(

' (
IUserRepository

( 7

repository

8 B
)

B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
DeleteUserCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
user 
= 

repository !
.! "
GetByIdAsync" .
(. /
request/ 6
.6 7
Id7 9
)9 :
.: ;
Result; A
;A B
if 
( 
user 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
await 

repository 
. 
DeleteAsync (
(( )
user) -
)- .
;. /
return 
user 
. 
Id 
; 
} 	
} 
} ∆
ã/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateAuthorCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
UpdateAuthorCommand $
:% &
IRequest' /
</ 0
Guid0 4
>4 5
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
? 
	FirstName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public		 
string		 
?		 
LastName		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 
} Ä
í/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateAuthorCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class &
UpdateAuthorCommandHandler +
:, -
IRequestHandler. =
<= >
UpdateAuthorCommand> Q
,Q R
GuidS W
>W X
{ 
private 
readonly 
IAuthorRepository *

repository+ 5
;5 6
public

 &
UpdateAuthorCommandHandler

 )
(

) *
IAuthorRepository

* ;

repository

< F
)

F G
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
UpdateAuthorCommand' :
request; B
,B C
CancellationTokenD U
cancellationTokenV g
)g h
{ 	
var 
author 
= 

repository #
.# $
GetByIdAsync$ 0
(0 1
request1 8
.8 9
Id9 ;
); <
.< =
Result= C
;C D
if 
( 
author 
== 
null 
|| !
author" (
.( )
Id) +
==, .
Guid/ 3
.3 4
Empty4 9
)9 :
{ 
throw 
new  
InvalidDataException .
(. /
$str/ G
)G H
;H I
} 
author 
. 
	FirstName 
= 
request &
.& '
	FirstName' 0
;0 1
author 
. 
LastName 
= 
request %
.% &
LastName& .
;. /
await 

repository 
. 
UpdateAsync (
(( )
author) /
)/ 0
;0 1
return 
author 
. 
Id 
; 
} 	
} 
} ©
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateBookCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
UpdateBookCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
? 
Title 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
decimal		 
Rating		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 
string

 
?

 
Description

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
DateTime 
PublicationDate '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
Uri 
? 
ImageUri 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} ô
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateBookCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
UpdateBookCommandHandler )
:* +
IRequestHandler, ;
<; <
UpdateBookCommand< M
,M N
GuidO S
>S T
{ 
private 
readonly 
IBookRepository (

repository) 3
;3 4
public

 $
UpdateBookCommandHandler

 '
(

' (
IBookRepository

( 7

repository

8 B
)

B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
UpdateBookCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
book 
= 

repository !
.! "
GetByIdAsync" .
(. /
request/ 6
.6 7
Id7 9
)9 :
.: ;
Result; A
;A B
if 
( 
book 
== 
null 
|| 
book  $
.$ %
Id% '
==( *
Guid+ /
./ 0
Empty0 5
)5 6
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
book 
. 
Rating 
= 
request !
.! "
Rating" (
;( )
book 
. 
Description 
= 
request &
.& '
Description' 2
;2 3
book 
. 
Title 
= 
request  
.  !
Title! &
;& '
book 
. 
PublicationDate  
=! "
request# *
.* +
PublicationDate+ :
;: ;
book 
. 
ImageUri 
= 
request #
.# $
ImageUri$ ,
;, -
await 

repository 
. 
UpdateAsync (
(( )
book) -
)- .
;. /
return 
book 
. 
Id 
; 
} 	
} 
}   î
ä/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateGenreCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
UpdateGenreCommand #
:$ %
IRequest& .
<. /
Guid/ 3
>3 4
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
? 
Name 
{ 
get !
;! "
set# &
;& '
}( )
}		 
}

 ÿ
ë/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateGenreCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class %
UpdateGenreCommandHandler *
:+ ,
IRequestHandler- <
<< =
UpdateGenreCommand= O
,O P
GuidQ U
>U V
{ 
private 
readonly 
IGenreRepository )

repository* 4
;4 5
public

 %
UpdateGenreCommandHandler

 (
(

( )
IGenreRepository

) 9

repository

: D
)

D E
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
UpdateGenreCommand' 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 
genre 
= 

repository "
." #
GetByIdAsync# /
(/ 0
request0 7
.7 8
Id8 :
): ;
.; <
Result< B
;B C
if 
( 
genre 
== 
null 
||  
genre! &
.& '
Id' )
==* ,
Guid- 1
.1 2
Empty2 7
)7 8
{ 
throw 
new  
InvalidDataException .
(. /
$str/ F
)F G
;G H
} 
genre 
. 
Name 
= 
request  
.  !
Name! %
;% &
await 

repository 
. 
UpdateAsync (
(( )
genre) .
). /
;/ 0
return 
genre 
. 
Id 
; 
} 	
} 
} ¡
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateUserCommand.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class 
UpdateUserCommand "
:# $
IRequest% -
<- .
Guid. 2
>2 3
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
? 
Username 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
string		 
?		 
Password		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 
} ‚
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Commands/UpdateUserCommandHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Commands '
{ 
public 

class $
UpdateUserCommandHandler )
:* +
IRequestHandler, ;
<; <
UpdateUserCommand< M
,M N
GuidO S
>S T
{ 
private 
readonly 
IUserRepository (

repository) 3
;3 4
public

 $
UpdateUserCommandHandler

 '
(

' (
IUserRepository

( 7

repository

8 B
)

B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
UpdateUserCommand' 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
user 
= 

repository !
.! "
GetByIdAsync" .
(. /
request/ 6
.6 7
Id7 9
)9 :
.: ;
Result; A
;A B
if 
( 
user 
== 
null 
|| 
user  $
.$ %
Id% '
==( *
Guid+ /
./ 0
Empty0 5
)5 6
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
user 
. 
Username 
= 
request #
.# $
Username$ ,
;, -
user 
. 
Password 
= 
request #
.# $
Password$ ,
;, -
await 

repository 
. 
UpdateAsync (
(( )
user) -
)- .
;. /
return 
user 
. 
Id 
; 
} 	
} 
} Ì
â/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetAuthorByIdQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetAuthorByIdQuery #
:$ %
IRequest& .
<. /
Author/ 5
>5 6
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 è
ê/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetAuthorByIdQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class %
GetAuthorByIdQueryHandler *
:+ ,
IRequestHandler- <
<< =
GetAuthorByIdQuery= O
,O P
AuthorQ W
>W X
{ 
private		 
readonly		 
IAuthorRepository		 *

repository		+ 5
;		5 6
public %
GetAuthorByIdQueryHandler (
(( )
IAuthorRepository) :

repository; E
)E F
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Author  
>  !
Handle" (
(( )
GetAuthorByIdQuery) ;
request< C
,C D
CancellationTokenE V
cancellationTokenW h
)h i
{ 	
var 
author 
= 
await 

repository )
.) *
GetByIdAsync* 6
(6 7
request7 >
.> ?
Id? A
)A B
;B C
if 
( 
author 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ G
)G H
;H I
} 
return 
author 
; 
} 	
} 
} ä
Ü/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetAuthorsQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetAuthorsQuery  
:! "
IRequest# +
<+ ,
IEnumerable, 7
<7 8
Author8 >
>> ?
>? @
{ 
} 
}		 €
ç/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetAuthorsQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class "
GetAuthorsQueryHandler '
:( )
IRequestHandler* 9
<9 :
GetAuthorsQuery: I
,I J
IEnumerableK V
<V W
AuthorW ]
>] ^
>^ _
{ 
private		 
readonly		 
IAuthorRepository		 *

repository		+ 5
;		5 6
public "
GetAuthorsQueryHandler %
(% &
IAuthorRepository& 7

repository8 B
)B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Author& ,
>, -
>- .
Handle/ 5
(5 6
GetAuthorsQuery6 E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 	
return 
await 

repository #
.# $
GetAllAsync$ /
(/ 0
)0 1
;1 2
} 	
} 
} Á
á/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetBookByIdQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetBookByIdQuery !
:" #
IRequest$ ,
<, -
Book- 1
>1 2
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 ˜
é/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetBookByIdQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class #
GetBookByIdQueryHandler (
:) *
IRequestHandler+ :
<: ;
GetBookByIdQuery; K
,K L
BookM Q
>Q R
{ 
private		 
readonly		 
IBookRepository		 (

repository		) 3
;		3 4
public #
GetBookByIdQueryHandler &
(& '
IBookRepository' 6

repository7 A
)A B
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Book 
> 
Handle  &
(& '
GetBookByIdQuery' 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
var 
book 
= 
await 

repository '
.' (
GetByIdAsync( 4
(4 5
request5 <
.< =
Id= ?
)? @
;@ A
if 
( 
book 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
return 
book 
; 
} 	
} 
} Ñ
Ñ/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetBooksQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetBooksQuery 
:  
IRequest! )
<) *
IEnumerable* 5
<5 6
Book6 :
>: ;
>; <
{ 
} 
}		 …
ã/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetBooksQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class  
GetBooksQueryHandler %
:& '
IRequestHandler( 7
<7 8
GetBooksQuery8 E
,E F
IEnumerableG R
<R S
BookS W
>W X
>X Y
{ 
private		 
readonly		 
IBookRepository		 (

repository		) 3
;		3 4
public  
GetBooksQueryHandler #
(# $
IBookRepository$ 3

repository4 >
)> ?
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Book& *
>* +
>+ ,
Handle- 3
(3 4
GetBooksQuery4 A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 	
return 
await 

repository #
.# $
GetAllAsync$ /
(/ 0
)0 1
;1 2
} 	
} 
} Í
à/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetGenreByIdQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetGenreByIdQuery "
:# $
IRequest% -
<- .
Genre. 3
>3 4
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 Ä
è/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetGenreByIdQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class $
GetGenreByIdQueryHandler )
:* +
IRequestHandler, ;
<; <
GetGenreByIdQuery< M
,M N
GenreO T
>T U
{ 
private		 
readonly		 
IGenreRepository		 )

repository		* 4
;		4 5
public $
GetGenreByIdQueryHandler '
(' (
IGenreRepository( 8

repository9 C
)C D
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Genre 
>  
Handle! '
(' (
GetGenreByIdQuery( 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 
book 
= 
await 

repository '
.' (
GetByIdAsync( 4
(4 5
request5 <
.< =
Id= ?
)? @
;@ A
if 
( 
book 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
return 
book 
; 
} 	
} 
} á
Ö/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetGenresQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetGenresQuery 
:  !
IRequest" *
<* +
IEnumerable+ 6
<6 7
Genre7 <
>< =
>= >
{ 
} 
}		 “
å/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetGenresQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class !
GetGenresQueryHandler &
:' (
IRequestHandler) 8
<8 9
GetGenresQuery9 G
,G H
IEnumerableI T
<T U
GenreU Z
>Z [
>[ \
{ 
private		 
readonly		 
IGenreRepository		 )

repository		* 4
;		4 5
public

 !
GetGenresQueryHandler

 $
(

$ %
IGenreRepository

% 5

repository

6 @
)

@ A
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Genre& +
>+ ,
>, -
Handle. 4
(4 5
GetGenresQuery5 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
return 
await 

repository #
.# $
GetAllAsync$ /
(/ 0
)0 1
;1 2
} 	
} 
} Á
á/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetUserByIdQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetUserByIdQuery !
:" #
IRequest$ ,
<, -
User- 1
>1 2
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 ˜
é/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetUserByIdQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class #
GetUserByIdQueryHandler (
:) *
IRequestHandler+ :
<: ;
GetUserByIdQuery; K
,K L
UserM Q
>Q R
{ 
private		 
readonly		 
IUserRepository		 (

repository		) 3
;		3 4
public #
GetUserByIdQueryHandler &
(& '
IUserRepository' 6

repository7 A
)A B
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
User 
> 
Handle  &
(& '
GetUserByIdQuery' 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
var 
user 
= 
await 

repository '
.' (
GetByIdAsync( 4
(4 5
request5 <
.< =
Id= ?
)? @
;@ A
if 
( 
user 
== 
null 
) 
{ 
throw 
new  
InvalidDataException .
(. /
$str/ E
)E F
;F G
} 
return 
user 
; 
} 	
} 
} Ñ
Ñ/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetUsersQuery.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class 
GetUsersQuery 
:  
IRequest! )
<) *
IEnumerable* 5
<5 6
User6 :
>: ;
>; <
{ 
} 
}		 …
ã/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Features/Queries/GetUsersQueryHandler.cs
	namespace 	
Application
 
. 
Features 
. 
Queries &
{ 
public 

class  
GetUsersQueryHandler %
:& '
IRequestHandler( 7
<7 8
GetUsersQuery8 E
,E F
IEnumerableG R
<R S
UserS W
>W X
>X Y
{ 
private		 
readonly		 
IUserRepository		 (

repository		) 3
;		3 4
public  
GetUsersQueryHandler #
(# $
IUserRepository$ 3

repository4 >
)> ?
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
User& *
>* +
>+ ,
Handle- 3
(3 4
GetUsersQuery4 A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 	
return 
await 

repository #
.# $
GetAllAsync$ /
(/ 0
)0 1
;1 2
} 	
} 
} ‚
Ñ/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IApplicationContext.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IApplicationContext (
{ 
DbSet 
< 
Domain 
. 
Entities 
. 
Book "
>" #
Books$ )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
DbSet 
< 
Domain 
. 
Entities 
. 
Genre #
># $
Genres% +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
DbSet		 
<		 
Domain		 
.		 
Entities		 
.		 
Author		 $
>		$ %
Authors		& -
{		. /
get		0 3
;		3 4
set		5 8
;		8 9
}		: ;
DbSet

 
<

 
Domain

 
.

 
Entities

 
.

 
User

 "
>

" #
Users

$ )
{

* +
get

, /
;

/ 0
set

1 4
;

4 5
}

6 7
Task 
< 
int 
> 
SaveChangesAsync "
(" #
)# $
;$ %
} 
} ∂
Ç/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IAuthorRepository.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IAuthorRepository &
:' (
IRepository) 4
<4 5
Author5 ;
>; <
{ 
} 
} ∞
Ä/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IBookRepository.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IBookRepository $
:% &
IRepository' 2
<2 3
Book3 7
>7 8
{ 
} 
} ≥
Å/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IGenreRepository.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IGenreRepository %
:& '
IRepository( 3
<3 4
Genre4 9
>9 :
{ 
} 
} È

|/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IRepository.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IRepository  
<  !
TEntity! (
>( )
where* /
TEntity0 7
:8 9

BaseEntity: D
{ 
Task 
< 
TEntity 
> 
AddAsync 
( 
TEntity &
entity' -
)- .
;. /
Task 
< 
TEntity 
> 
UpdateAsync !
(! "
TEntity" )
entity* 0
)0 1
;1 2
Task		 
<		 
TEntity		 
>		 
DeleteAsync		 !
(		! "
TEntity		" )
entity		* 0
)		0 1
;		1 2
Task

 
<

 
IEnumerable

 
<

 
TEntity

  
>

  !
>

! "
GetAllAsync

# .
(

. /
)

/ 0
;

0 1
Task 
< 
TEntity 
? 
> 
GetByIdAsync #
(# $
Guid$ (
id) +
)+ ,
;, -
} 
} ∞
Ä/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Application/Interfaces/IUserRepository.cs
	namespace 	
Application
 
. 

Interfaces  
{ 
public 

	interface 
IUserRepository $
:% &
IRepository' 2
<2 3
User3 7
>7 8
{ 
} 
} 