using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProdMan_WASM.Helper;

namespace ProdMan_WASM.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient client;
        private const string tokenName = "jwttoken";

        public AuthStateProvider(ILocalStorageService localStorage, HttpClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>(tokenName);
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType")));

            return authState;
        }

        public void NotifyUserLogin(string token)
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(principal));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
