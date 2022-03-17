using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace ProdMan_Server.Services
{
    public interface IFileManager
    {
        public Task<string> SaveFile(IBrowserFile file);
        public bool DeleteFile(string filename);
    }
}
