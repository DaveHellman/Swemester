using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Business.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CabinDbContext db;
        private readonly IMapper mapper;

        public BookingRepository(CabinDbContext dbContext, IMapper mapper)
        {
            db = dbContext;
            this.mapper = mapper;
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDTO)
        {
            var cabins = db.Cabins.Where(c => !c.Bookings
            .Any(b => b.StartDate <= bookingDTO.Start && b.EndDate >= bookingDTO.Start & b.StartDate >= bookingDTO.Start && b.StartDate <= bookingDTO.End));
            var cabin = db.Cabins.FirstOrDefault(c => c.ID == bookingDTO.CabinID);
            if (!cabins.Contains(cabin))
            {
                return new BookingDTO();
            }
            var booking = mapper.Map<Booking>(bookingDTO);
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
            return bookingDTO;
        }

        public async Task<int> DeleteBookingAsync(int ID)
        {
            var booking = db.Bookings.FirstOrDefault(book => book.ID == ID);
            if (booking == null)
                return 0;
            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();
            return 1;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<List<BookingDTO>> GetAllBookingsAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return mapper.Map<List<BookingDTO>>(db.Bookings);
        }

        public async Task<Booking> GetBookingAsync(int ID)
        {
            var booking = await db.Bookings.FirstOrDefaultAsync(booking => booking.ID == ID);
            return booking;
        }
    }
}
