using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace ProdMan_WASM.Helper
{
    public static class JwtParser
    {

        /// <summary>
        /// Skapar en array av claims baserat på en JWT token
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwtToken)
        {

            var claims = new List<Claim>();
            var payload = jwtToken.Split('.')[1];
            var jsonbytes = ParseBase64WithoutPadding(payload);
            var keyvaluepairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonbytes);
            claims.AddRange(keyvaluepairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }


        /// <summary>
        /// Konverterar base64sträng till bytearray
        /// Lägger till = eller == i slutet om detta saknas.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        private static byte[] ParseBase64WithoutPadding(string payload)
        {
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }
            return Convert.FromBase64String(payload);
        }
    }
}
