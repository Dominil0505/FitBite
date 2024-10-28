# FitBite
## Tech 
- ASP.NET core web API
- ASP.NET core Blazor Web Assembly
- MudBlazor framework

## Ötlet - Idea
- Az ötlet az volt hogy készíteni akarok egy szoftvert ahol egy dietetikus tudja kezelni az étrendeket, látja a pácienseit.
- A dietetikus tud készíteni adott menüket (reggeli, ebéd stb..) tud felvenni ételeket, hozzávalókat amiket az elején felkell venni de azután ami már felvan véve azt ki lehet választani egyböl számolja a kaloriát fehérjét stb...
- Az adminisztrátor kezeli a páciensek felvételét

# Database Design
## Adatbázis táblák - Database tables
- User(User_Id, User_name, Password, Email, Mobile, avatar_url, created_at, updated_at, deleted_at, is_profile_completed)
- Role(Role_Id, Role_name)
- Dietician(dietician_id)
- Patient(patient_id,age,description,height,weight,gender)
- Menu(menu_id, menu_name,coment,created_at,updated_at,created_by,menu_start,menu_end) -> A menu_start jelenti hogy például 2024.08.13-an 14:kor kezdödik az a menu, pl ebéd stb.. dátumot akarok benne tárolni majd a naptárhoz
- Food(food_id, food_name, calorie, protein, fat, carbohydrate, acid, ingredients, allergens)
- Disease( disease_id, disease_name)
- Medicine(medicine_id, medicine_name)
- Allergy(allergy_id, allergy_name)
- RefreshTokenInfo(id, token, userId)


## Kapcsolótáblák - Relation Tables
### 1:1
- User_Roles(user_id, role_id) -> 1:1
### 1:N
- dietetician_patient(dietetician_id, patient_id) | 1:N
- menu_assignment(dietician_id, menu_id, patient_id) | 1:N
### N:M kapcsolatok - N:M Relations
- patient_diseases (patient_id, disease_id) -> N:M
- patient_medicine (patient_id, medicine_id) -> N:M
- patient_allergy (patient_id, allergy_id) -> N:M
- food_ingredients (food_id, ingredients_id) -> N:M
- menu_food (menu_id, food_id)
- menu_patient(menu_id, patient_id)

# Osztályok funkciók
## Server library
### Contracts/IUserAccount
    - Ez egy interface amiben létre van hozva 3 db funkció loginasnyc, createasnyc és refreshtokenasync
### Impletemtations/UserAccountRepository
#### CreateUserAsync:
- Regisztrációs funkció, amely létrehozza a felhasználót az adatbázisban.
- Ellenőrzi, hogy az e-mail cím már regisztrálva van-e.
- A felhasználó szerepe alapján (Dietician, Patient, Admin) létrehozza a megfelelő rekordokat és hozzárendeli a szerepkört.

#### RefreshTokenAsync:
- Frissíti a felhasználó JWT tokenjét, ha van érvényes frissítési tokenje.
- Ellenőrzi, hogy a token létezik-e, és érvényes-e.
- Ha minden rendben, új JWT és frissítési tokent generál.

#### SignInAsync:
- Bejelentkezési funkció, amely ellenőrzi a felhasználó hitelesítő adatait (e-mail és jelszó).
- Ha a felhasználó megtalálható, létrehozza a JWT tokent és frissítési tokent tárol számára.

#### GenerateToken:
- JWT tokent hoz létre, amely tartalmazza a felhasználó azonosítóját, nevét, e-mail címét és szerepkörét.

#### AddToDatabase:
- Általános funkció, amely egy új rekordot ad hozzá az adatbázishoz.

#### AddUserRoles:
- Hozzárendeli a felhasználóhoz a megfelelő szerepkört a User_Roles táblában.

#### Segédfüggvények:
- FindRole, FindUserRole, FindRoleName, FindUserByEmail: Ezek a funkciók a különböző adatbázis keresésekért felelősek, például szerepkörök vagy felhasználók kereséséért.

#### GenerateRefreshToken:
- Létrehoz egy biztonságos, véletlenszerű frissítési tokent, amelyet a felhasználók később használhatnak a tokenek frissítéséhez.

## Client library
### Helpers/CustomAuthenticationStateProvider
- Az osztály fő feladata az, hogy minden oldalváltáskor (pl. admin felület és dolgozók felület közti váltás) biztosítsa, hogy a felhasználó megfelelő jogosultsággal rendelkezzen. Ellenőrzi, hogy be van-e jelentkezve, és a szerepköre alapján jogosult-e az adott oldal megtekintésére.
#### GetAuthenticationStateAsync:
- Ellenőrzi, hogy van-e tárolt token a felhasználó eszközén (a LocalStorage-ból olvas).
- Ha a token nem található, vagy érvénytelen, visszaad egy névtelen felhasználói állapotot.
- Ha talál érvényes tokent, akkor deszerializálja a tárolt felhasználói munkamenetet, és a token alapján lekérdezi a felhasználó adatait (Claims-ek).
- Az eredményként kapott Claims-ek alapján létrehozza a felhasználói állapotot (ClaimsPrincipal).

#### UpdateAuthenticationState:
- Frissíti a felhasználó hitelesítési állapotát.
- Ha van érvényes token vagy frissítési token, akkor azokat elmenti a LocalStorage-be és frissíti a felhasználó Claims adatait.
- Ha nincs érvényes token, törli a LocalStorage-ból a tárolt tokent és beállítja a névtelen felhasználói állapotot.

#### SetClaimPrincipal:
- Létrehozza a ClaimsPrincipal objektumot a felhasználói adatok alapján (név, e-mail, szerepkör), amelyet a hitelesítési állapothoz használ.
- Ez a fő módszer, amely a felhasználói jogosultságokat beállítja.

#### DecryptToken:
- Dekódolja a JWT tokent, és kinyeri belőle a felhasználó adatait (azonosító, név, e-mail, szerepkör).
- Ez a funkció felelős azért, hogy a token Claims-ei alapján visszaadja a felhasználó információit egyedi Claims objektumként.
### Helpers/GetHttpClient
Használat:

    A GetPrivateHttpClient akkor hasznos, amikor az alkalmazás privát API-kat ér el, ahol szükség van felhasználói azonosításra a JWT token segítségével.
    A GetPublicHttpClient olyan esetekre jó, amikor nincs szükség a felhasználó azonosítására (pl. nyilvános API-hívások).

Ez az osztály megkönnyíti a HTTP-kliensek létrehozását azzal, hogy automatikusan hozzáadja a szükséges hitelesítési információkat a kérésekhez, ha a felhasználó be van jelentkezve.
#### GetPrivateHttpClient:
- Létrehozza a privát HTTP-klienst.
- A kliens beállítja az Authorization fejlécet a JWT token alapján, amit a LocalStorage-ból olvas ki.
- Ha nincs érvényes token, vagy ha a token nem található meg, akkor a kliens hitelesítési fejléc nélkül tér vissza.
- Ez a funkció biztosítja, hogy a felhasználó hitelesítése a token alapján történjen, amikor API-hívásokat tesz.

#### GetPublicHttpClient:
- Létrehozza a nyilvános HTTP-klienst.
- Eltávolítja az Authorization fejlécet, így a kliens nem küld hitelesítési információt a kérések során.
- Ez a kliens olyan API-hívásokhoz használható, amelyekhez nincs szükség hitelesítésre, vagy publikus API-khoz.
### Helpers/LocalStorageService
Ez az osztály leegyszerűsíti a local storage kezelését azáltal, hogy a token mentését, olvasását és törlését könnyen elérhetővé teszi. Különösen fontos, hogy a JWT tokent biztonságosan kezelje, amelyet a hitelesítési folyamatok során használunk fel.
#### GetToken:
- Visszaadja a local storage-ben tárolt hitelesítési tokent (authentication-token), amit a böngészőben mentett el.
- Ha a token nem létezik, akkor null vagy üres értéket ad vissza.

#### SetToken:
- Elmenti a megadott tokent a local storage-be az authentication-token kulcs alatt.
- Ezzel a funkcióval a felhasználó hitelesítési tokenjét tárolhatjuk, hogy később felhasználhassuk hitelesítéshez.

#### RemoveToken:
- Eltávolítja a local storage-ből a tárolt hitelesítési tokent, ezzel kijelentkezteti a felhasználót.
- Hasznos funkció, amikor a felhasználó kijelentkezik, és törölni kell a tokenjét.
### Helpers/Serializations
Ez az osztály megkönnyíti a JSON-alapú adatfeldolgozást az alkalmazásban, lehetővé téve az objektumok egyszerű és gyors átalakítását JSON stringekké és vissza. Az általános megközelítés rugalmassá teszi különböző típusú objektumok kezelésére.