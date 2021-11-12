Ê	
{/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Context/AuthorContext.cs
	namespace 	
Persistence
 
. 
Context 
{ 
public 

class 
AuthorContext 
:  
	DbContext! *
{ 
public 
AuthorContext 
( 
DbContextOptions -
<- .
AuthorContext. ;
>; <
options= D
)D E
:F G
baseH L
(L M
optionsM T
)T U
{		 	
}

 	
public 
DbSet 
< 
Author 
> 
? 
Authors %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
async 
Task 
< 
int 
> 
SaveChangesAsync /
(/ 0
)0 1
{ 	
return 
await 
base 
. 
SaveChangesAsync .
(. /
)/ 0
;0 1
} 	
} 
} ¾	
y/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Context/BookContext.cs
	namespace 	
Persistence
 
. 
Context 
{ 
public 

class 
BookContext 
: 
	DbContext (
{ 
public 
BookContext 
( 
DbContextOptions +
<+ ,
BookContext, 7
>7 8
options9 @
)@ A
:B C
baseD H
(H I
optionsI P
)P Q
{		 	
}

 	
public 
DbSet 
< 
Book 
> 
? 
Books !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
async 
Task 
< 
int 
> 
SaveChangesAsync /
(/ 0
)0 1
{ 	
return 
await 
base 
. 
SaveChangesAsync .
(. /
)/ 0
;0 1
} 	
} 
} Ä	
z/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Context/GenreContext.cs
	namespace 	
Persistence
 
. 
Context 
{ 
public 

class 
GenreContext 
: 
	DbContext  )
{ 
public 
GenreContext 
( 
DbContextOptions ,
<, -
GenreContext- 9
>9 :
options; B
)B C
:D E
baseF J
(J K
optionsK R
)R S
{		 	
}

 	
public 
DbSet 
< 
Genre 
> 
? 
Genres #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
async 
Task 
< 
int 
> 
SaveChangesAsync /
(/ 0
)0 1
{ 	
return 
await 
base 
. 
SaveChangesAsync .
(. /
)/ 0
;0 1
} 	
} 
} ¾	
y/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Context/UserContext.cs
	namespace 	
Persistence
 
. 
Context 
{ 
public 

class 
UserContext 
: 
	DbContext (
{ 
public 
UserContext 
( 
DbContextOptions +
<+ ,
UserContext, 7
>7 8
options9 @
)@ A
:B C
baseD H
(H I
optionsI P
)P Q
{		 	
}

 	
public 
DbSet 
< 
User 
> 
? 
Users !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
async 
Task 
< 
int 
> 
SaveChangesAsync /
(/ 0
)0 1
{ 	
return 
await 
base 
. 
SaveChangesAsync .
(. /
)/ 0
;0 1
} 	
} 
} ñ
Ž/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Migrations/20211110153934_FirstMigration.cs
	namespace 	
Persistence
 
. 

Migrations  
{ 
public 

partial 
class 
FirstMigration '
:( )
	Migration* 3
{		 
	protected

 
override

 
void

 
Up

  "
(

" #
MigrationBuilder

# 3
migrationBuilder

4 D
)

D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Title 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 =
,= >
nullable? G
:G H
falseI N
)N O
,O P
Rating 
= 
table "
." #
Column# )
<) *
decimal* 1
>1 2
(2 3
type3 7
:7 8
$str9 ?
,? @
nullableA I
:I J
falseK P
)P Q
,Q R
Description 
=  !
table" '
.' (
Column( .
<. /
string/ 5
>5 6
(6 7
type7 ;
:; <
$str= C
,C D
nullableE M
:M N
falseO T
)T U
,U V
PublicationDate #
=$ %
table& +
.+ ,
Column, 2
<2 3
DateTime3 ;
>; <
(< =
type= A
:A B
$strC I
,I J
nullableK S
:S T
falseU Z
)Z [
,[ \
ImageUri 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% /
,/ 0
x1 2
=>3 5
x6 7
.7 8
Id8 :
): ;
;; <
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name   
:   
$str   
)   
;   
}!! 	
}"" 
}## ø
•/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Migrations/Author/20211110153943_FirstMigration.cs
	namespace 	
Persistence
 
. 

Migrations  
.  !
Author! '
{ 
public 

partial 
class 
FirstMigration '
:( )
	Migration* 3
{		 
	protected

 
override

 
void

 
Up

  "
(

" #
MigrationBuilder

# 3
migrationBuilder

4 D
)

D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
,  
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
	FirstName 
= 
table  %
.% &
Column& ,
<, -
string- 3
>3 4
(4 5
type5 9
:9 :
$str; A
,A B
nullableC K
:K L
falseM R
)R S
,S T
LastName 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 1
,1 2
x3 4
=>5 7
x8 9
.9 :
Id: <
)< =
;= >
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name 
: 
$str 
)  
;  !
} 	
} 
}   ½
”/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Migrations/Genre/20211110153950_FirstMigration.cs
	namespace 	
Persistence
 
. 

Migrations  
.  !
Genre! &
{ 
public 

partial 
class 
FirstMigration '
:( )
	Migration* 3
{		 
	protected

 
override

 
void

 
Up

  "
(

" #
MigrationBuilder

# 3
migrationBuilder

4 D
)

D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 <
,< =
nullable> F
:F G
falseH M
)M N
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 0
,0 1
x2 3
=>4 6
x7 8
.8 9
Id9 ;
); <
;< =
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name 
: 
$str 
) 
;  
} 	
} 
} ó
“/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/Migrations/User/20211110153956_FirstMigration.cs
	namespace 	
Persistence
 
. 

Migrations  
.  !
User! %
{ 
public 

partial 
class 
FirstMigration '
:( )
	Migration* 3
{		 
	protected

 
override

 
void

 
Up

  "
(

" #
MigrationBuilder

# 3
migrationBuilder

4 D
)

D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Username 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
,R S
Password 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% /
,/ 0
x1 2
=>3 5
x6 7
.7 8
Id8 :
): ;
;; <
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name 
: 
$str 
) 
; 
} 	
} 
}   º
s/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/PersistenceDI.cs
	namespace 	
Persistence
 
{		 
public

 

static

 
class

 
PersistenceDI

 %
{ 
public 
static 
void 
AddPersistence )
() *
this* .
IServiceCollection/ A
servicesB J
,J K
IConfigurationL Z
configuration[ h
)h i
{ 	
services 
. 
AddDbContext !
<! "
BookContext" -
>- .
(. /
options/ 6
=>7 9
options: A
.A B
	UseSqliteB K
(K L
$strL d
)d e
)e f
;f g
services 
. 
AddDbContext !
<! "
GenreContext" .
>. /
(/ 0
options0 7
=>8 :
options; B
.B C
	UseSqliteC L
(L M
$strM f
)f g
)g h
;h i
services 
. 
AddDbContext !
<! "
AuthorContext" /
>/ 0
(0 1
options1 8
=>9 ;
options< C
.C D
	UseSqliteD M
(M N
$strN h
)h i
)i j
;j k
services 
. 
AddDbContext !
<! "
UserContext" -
>- .
(. /
options/ 6
=>7 9
options: A
.A B
	UseSqliteB K
(K L
$strL d
)d e
)e f
;f g
services 
. 
AddTransient !
(! "
typeof" (
(( )
IRepository) 4
<4 5
>5 6
)6 7
,7 8
typeof9 ?
(? @

Repository@ J
<J K
>K L
)L M
)M N
;N O
services 
. 
AddTransient !
<! "
IBookRepository" 1
,1 2
BookRepository3 A
>A B
(B C
)C D
;D E
services 
. 
AddTransient !
<! "
IGenreRepository" 2
,2 3
GenreRepository4 C
>C D
(D E
)E F
;F G
services 
. 
AddTransient !
<! "
IAuthorRepository" 3
,3 4
AuthorRepository5 E
>E F
(F G
)G H
;H I
services 
. 
AddTransient !
<! "
IUserRepository" 1
,1 2
UserRepository3 A
>A B
(B C
)C D
;D E
} 	
} 
} ¾
y/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/v1/AuthorRepository.cs
	namespace 	
Persistence
 
. 
v1 
{ 
public 

class 
AuthorRepository !
:" #

Repository$ .
<. /
Author/ 5
>5 6
,6 7
IAuthorRepository8 I
{ 
public		 
AuthorRepository		 
(		  
AuthorContext		  -
context		. 5
)		5 6
:		7 8
base		9 =
(		= >
context		> E
)		E F
{

 	
} 	
} 
} ²
w/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/v1/BookRepository.cs
	namespace 	
Persistence
 
. 
v1 
{ 
public 

class 
BookRepository 
:  !

Repository" ,
<, -
Book- 1
>1 2
,2 3
IBookRepository4 C
{ 
public		 
BookRepository		 
(		 
BookContext		 )
context		* 1
)		1 2
:		3 4
base		5 9
(		9 :
context		: A
)		A B
{

 	
} 	
} 
} ¸
x/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/v1/GenreRepository.cs
	namespace 	
Persistence
 
. 
v1 
{ 
public 

class 
GenreRepository  
:! "

Repository# -
<- .
Genre. 3
>3 4
,4 5
IGenreRepository6 F
{ 
public		 
GenreRepository		 
(		 
GenreContext		 +
context		, 3
)		3 4
:		5 6
base		7 ;
(		; <
context		< C
)		C D
{

 	
} 	
} 
} ê'
s/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/v1/Repository.cs
	namespace 	
Persistence
 
. 
v1 
{ 
public 

class 

Repository 
< 
TEntity #
># $
:% &
IRepository' 2
<2 3
TEntity3 :
>: ;
where< A
TEntityB I
:J K

BaseEntityL V
{ 
private		 
readonly		 
	DbContext		 "
context		# *
;		* +
public 

Repository 
( 
	DbContext #
context$ +
)+ ,
{ 	
this 
. 
context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
TEntity !
>! "
AddAsync# +
(+ ,
TEntity, 3
entity4 :
): ;
{ 	
if 
( 
entity 
== 
null 
) 
{ 
throw 
new 
ArgumentException +
(+ ,
$", .
{. /
nameof/ 5
(5 6
AddAsync6 >
)> ?
}? @
$str@ X
"X Y
)Y Z
;Z [
} 
await 
context 
. 
AddAsync "
(" #
entity# )
)) *
;* +
await 
context 
. 
SaveChangesAsync *
(* +
)+ ,
;, -
return 
entity 
; 
} 	
public 
async 
Task 
< 
TEntity !
>! "
DeleteAsync# .
(. /
TEntity/ 6
entity7 =
)= >
{ 	
if 
( 
entity 
== 
null 
) 
{ 
throw 
new 
ArgumentException +
(+ ,
$", .
{. /
nameof/ 5
(5 6
DeleteAsync6 A
)A B
}B C
$strC [
"[ \
)\ ]
;] ^
} 
context!! 
.!! 
Remove!! 
(!! 
entity!! !
)!!! "
;!!" #
await"" 
context"" 
."" 
SaveChangesAsync"" *
(""* +
)""+ ,
;"", -
return## 
entity## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
IEnumerable&& %
<&&% &
TEntity&&& -
>&&- .
>&&. /
GetAllAsync&&0 ;
(&&; <
)&&< =
{'' 	
return(( 
await(( 
context((  
.((  !
Set((! $
<(($ %
TEntity((% ,
>((, -
(((- .
)((. /
.((/ 0
ToListAsync((0 ;
(((; <
)((< =
;((= >
})) 	
public++ 
async++ 
Task++ 
<++ 
TEntity++ !
?++! "
>++" #
GetByIdAsync++$ 0
(++0 1
Guid++1 5
id++6 8
)++8 9
{,, 	
if-- 
(-- 
id-- 
==-- 
Guid-- 
.-- 
Empty--  
)--  !
{.. 
throw// 
new// 
ArgumentException// +
(//+ ,
$"//, .
{//. /
nameof/// 5
(//5 6
GetByIdAsync//6 B
)//B C
}//C D
$str//D Y
"//Y Z
)//Z [
;//[ \
}00 
return22 
await22 
context22  
.22  !
	FindAsync22! *
<22* +
TEntity22+ 2
>222 3
(223 4
id224 6
)226 7
;227 8
}33 	
public55 
async55 
Task55 
<55 
TEntity55 !
>55! "
UpdateAsync55# .
(55. /
TEntity55/ 6
entity557 =
)55= >
{66 	
if77 
(77 
entity77 
==77 
null77 
)77 
{88 
throw99 
new99 
ArgumentException99 +
(99+ ,
$"99, .
{99. /
nameof99/ 5
(995 6
UpdateAsync996 A
)99A B
}99B C
$str99C [
"99[ \
)99\ ]
;99] ^
}:: 
context<< 
.<< 
Update<< 
(<< 
entity<< !
)<<! "
;<<" #
await== 
context== 
.== 
SaveChangesAsync== *
(==* +
)==+ ,
;==, -
return>> 
entity>> 
;>> 
}?? 	
}@@ 
}AA ²
w/Users/alex-ama/Documents/GitHub/Books-recommendation-system/BooksRecommandationSystem/Persistence/v1/UserRepository.cs
	namespace 	
Persistence
 
. 
v1 
{ 
public 

class 
UserRepository 
:  !

Repository" ,
<, -
User- 1
>1 2
,2 3
IUserRepository4 C
{ 
public		 
UserRepository		 
(		 
UserContext		 )
context		* 1
)		1 2
:		3 4
base		5 9
(		9 :
context		: A
)		A B
{

 	
} 	
} 
} 