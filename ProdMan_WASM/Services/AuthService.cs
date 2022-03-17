using Blazored.LocalStorage;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService local;
        private readonly HttpClient client;
        private const string tokenName = "jwttoken";

        public AuthService(ILocalStorageService local, HttpClient client)
        {
            this.local = local;
            this.client = client;
        }
        public async Task<AuthenticationResponsDTO> Login(AuthenticationRequestDTO request)
        {
            var stringcontent = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var respons = await client.PostAsync("api/account/login", stringcontent);
            var stringdata = await respons.Content.ReadAsStringAsync();
            var Authreponse = JsonConvert.DeserializeObject<AuthenticationResponsDTO>(stringdata);
            
            if (respons.IsSuccessStatusCode)
            {
                await local.SetItemAsync(tokenName, Authreponse.Token);
                await local.SetItemAsync("UserDetails", Authreponse.UserDTO);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Authreponse.Token);
                return new AuthenticationResponsDTO() { IsAuthenticationSuccess = true, Token = Authreponse.Token };
            }
            else
            {
                return new AuthenticationResponsDTO() { IsAuthenticationSuccess = false, ErrorMessage = Authreponse.ErrorMessage };
            }
        }

        public async Task Logout()
        {
            await local.RemoveItemAsync(tokenName);
            await local.RemoveItemAsync("UserDetails");
        }

        public async Task<UserRegisterResponsDTO> Register(UserRegisterRequestDTO request)
        {
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/account/register", content);
            var stringdata = await response.Content.ReadAsStringAsync();
            var responseDTO = JsonConvert.DeserializeObject<UserRegisterResponsDTO>(stringdata);

            return responseDTO;
        }
    }
}
