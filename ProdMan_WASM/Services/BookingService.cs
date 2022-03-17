using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient client;

        public BookingService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDTO)
        {
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(bookingDTO), Encoding.UTF8, "application/json");
            var respons = await client.PostAsync("api/booking", content);
            var stringdata = await respons.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<BookingDTO>(stringdata);
            return booking;
        }

        public async Task DeleteBookingAsync(int ID)
        {
            await client.DeleteAsync("api/booking/"+ID);
        }
    }
}
