using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ProdMan_Server.Helpers
{
    public static class Helper
    {
        public async static Task SuccessToastr(this IJSRuntime runtime, string message)
        {
            await runtime.InvokeVoidAsync("ShowToast", "success", message);
        }

        public static async Task ErrorToastr(this IJSRuntime runtime, string message)
        {
            await runtime.InvokeVoidAsync("ShowToast", "error", message);
        }
    }
}
