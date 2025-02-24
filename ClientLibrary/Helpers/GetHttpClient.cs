using BaseLibrary.DTOs;

namespace ClientLibrary.Helpers
{
    /// <summary>
    /// Ez az osztály egy segédosztály, amely HTTP klienseket hoz létre hitelesítési fejléccel vagy anélkül. 
    /// Ha egy érvényes token található a helyi tárolóban, akkor egy hitelesített kliens példányt hoz létre, 
    /// ellenkező esetben egy alapértelmezett (nyilvános) kliens példányt ad vissza. 
    /// Ez lehetővé teszi az API-hívások megfelelő kezelését hitelesített és hitelesítés nélküli módokban egyaránt.
    /// </summary>
    public class GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
    {
        private const string HeaderKey = "Authorization"; // Az Authorization fejléc kulcsa.

        /// <summary>
        /// Létrehoz egy HttpClient példányt hitelesítési fejléccel, ha egy érvényes token található a helyi tárolóban.
        /// </summary>
        /// <returns>HttpClient példány, amely lehet hitelesített vagy nem hitelesített.</returns>
        public async Task<HttpClient> GetPrivateHttpClient()
        {
            // Új HTTP kliens példány létrehozása egy megnevezett kliens konfigurációval.
            var client = httpClientFactory.CreateClient("SystemApiClient");

            // Az eltárolt hitelesítési token lekérése a helyi tárolóból.
            var stringToken = await localStorageService.GetToken();

            // Ha nincs token, a kliens visszaadása hitelesítés nélkül.
            if (string.IsNullOrEmpty(stringToken))
                return client;

            // A token deszerializálása egy UserSession objektumba.
            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);

            // Ha a deszerializálás sikertelen, a kliens hitelesítés nélkül tér vissza.
            if (deserializeToken == null) return client;

            // Az Authorization fejléc hozzáadása a HTTP klienshez a megszerzett tokennel.
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);

            return client;
        }

        /// <summary>
        /// Létrehoz egy HttpClient példányt hitelesítési fejléc nélkül.
        /// </summary>
        /// <returns>HttpClient példány hitelesítés nélkül.</returns>
        public HttpClient GetPublicHttpClient()
        {
            // Új HTTP kliens példány létrehozása egy megnevezett kliens konfigurációval.
            var client = httpClientFactory.CreateClient("SystemApiClient");

            // Biztosítja, hogy minden meglévő hitelesítési fejléc el legyen távolítva.
            client.DefaultRequestHeaders.Remove(HeaderKey);

            return client;
        }
    }
}
