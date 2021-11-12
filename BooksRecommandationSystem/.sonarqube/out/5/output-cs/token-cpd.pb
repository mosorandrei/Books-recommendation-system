á
r/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Domain/Common/BaseEntity.cs
	namespace 	
Domain
 
. 
Common 
{ 
public 

abstract 
class 

BaseEntity $
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
} ±
p/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Domain/Entities/Author.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
Author 
: 

BaseEntity $
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
 ”	
n/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Domain/Entities/Book.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
Book 
: 

BaseEntity "
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
} ÿ
o/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Domain/Entities/Genre.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
Genre 
: 

BaseEntity #
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
}		 ¬
n/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Domain/Entities/User.cs
	namespace 	
Domain
 
. 
Entities 
{ 
public 

class 
User 
: 

BaseEntity "
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
 