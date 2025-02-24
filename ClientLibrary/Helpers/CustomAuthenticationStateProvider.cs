using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibrary.Helpers
{
    /// <summary>
    /// Ez az osztály egy egyedi hitelesítési állapot szolgáltató, amely kezeli a felhasználó hitelesítési állapotát a helyi tárolóban (LocalStorage) tárolt token alapján.
    /// A hitelesítési állapot frissítésekor a token érvényességét ellenőrzi, dekódolja a felhasználói adatokat, és frissíti az állapotot.
    /// Ha nincs érvényes token, a felhasználó névtelen állapotban marad.
    /// </summary>
    public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        /// <summary>
        /// Lekéri a felhasználó aktuális hitelesítési állapotát.
        /// </summary>
        /// <returns>Az aktuális hitelesítési állapotot tartalmazó AuthenticationState objektum.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken))
                return await Task.FromResult(new AuthenticationState(anonymous));

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null)
                return await Task.FromResult(new AuthenticationState(anonymous));

            var getUserClaims = DecryptToken(deserializeToken.Token!);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));

            var claimsPrincipal = SetClaimPrincipal(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        /// <summary>
        /// Frissíti a felhasználó hitelesítési állapotát a megadott UserSession alapján.
        /// </summary>
        /// <param name="userSession">A frissített felhasználói munkamenet adatai.</param>
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializeSession = Serializations.SerializeObj(userSession);
                await localStorageService.SetToken(serializeSession);
                var getUserClaims = DecryptToken(userSession.Token!);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await localStorageService.RemoveToken();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        /// <summary>
        /// Létrehoz egy ClaimsPrincipal objektumot a megadott felhasználói jogosultságok alapján.
        /// </summary>
        /// <param name="claims">A felhasználói jogosultságokat tartalmazó objektum.</param>
        /// <returns>Az új ClaimsPrincipal példány.</returns>
        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, claims.Id!),
                    new(ClaimTypes.Name, claims.Name!),
                    new(ClaimTypes.Email, claims.Email!),
                    new(ClaimTypes.Role, claims.Role!)
                }, "JwtAuth"));
        }

        /// <summary>
        /// Dekódolja a JWT tokent, és kinyeri belőle a felhasználói jogosultságokat.
        /// </summary>
        /// <param name="jwtToken">A dekódolandó JWT token.</param>
        /// <returns>A kinyert felhasználói jogosultságokat tartalmazó objektum.</returns>
        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userId = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);
            return new CustomUserClaims(userId!.Value!, name!.Value, email!.Value!, role!.Value);
        }
    }
}
