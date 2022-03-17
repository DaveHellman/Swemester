using Business.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace ProdMan_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingrepo;

        public BookingController(IBookingRepository bookingrepo)
        {
            this.bookingrepo = bookingrepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDTO)
        {
            return Ok(await bookingrepo.CreateBookingAsync(bookingDTO));
        }

        [HttpDelete("{ID:int}")]
        public async Task<IActionResult> DeleteBooking(int ID)
        {
            var success = await bookingrepo.DeleteBookingAsync(ID);
            if (success == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            return Ok( await bookingrepo.GetAllBookingsAsync());
        }

        [HttpGet("{ID:int}")]
        public async Task<IActionResult> GetBookings(int ID)
        {
            var booking = await bookingrepo.GetBookingAsync(ID);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }
    }
}
