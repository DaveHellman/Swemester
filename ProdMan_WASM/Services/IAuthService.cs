using Models;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public interface IAuthService
    {
        public Task<AuthenticationResponsDTO> Login(AuthenticationRequestDTO request);
        public Task<UserRegisterResponsDTO> Register(UserRegisterRequestDTO request);
        public Task Logout();
    }
}
