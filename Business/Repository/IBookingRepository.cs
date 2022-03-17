using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using Models;

namespace Business.Repository
{
    public interface IBookingRepository
    {
        public Task<BookingDTO> CreateBookingAsync(BookingDTO booking);
        public Task<int> DeleteBookingAsync(int ID);
        public Task<Booking> GetBookingAsync(int ID);
        public Task<List<BookingDTO>> GetAllBookingsAsync();

    }
}
