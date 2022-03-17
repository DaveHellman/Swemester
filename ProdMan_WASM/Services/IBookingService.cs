using Models;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public interface IBookingService
    {
        public Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDTO);
        public Task DeleteBookingAsync(int ID);
    }
}
