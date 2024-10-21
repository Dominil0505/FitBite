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
