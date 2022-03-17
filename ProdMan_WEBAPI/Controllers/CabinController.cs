using Business.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ProdMan_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinController : ControllerBase
    {
        private readonly ICabinRepository cabinRepo;

        public CabinController(ICabinRepository cabinRepo)
        {
            this.cabinRepo = cabinRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCabinsAsync(DateTime? Start,DateTime? End)
        {
            if(Start !=null && End!=null)
            {
                return Ok(await cabinRepo.GetAllAvailableCabinsAsync(Start, End));
            }
            return Ok(await cabinRepo.GetAllCabinsAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCabinAsync(int id)
        {
            if (id < 1)
                return BadRequest("Du har angett ett ogiltigt ID");
            
            var cabin = await cabinRepo.GetCabinAsync(id);
            if (cabin == null)
                return NotFound("Produkten kunde inte hittas!");

            return Ok(await cabinRepo.GetCabinAsync(id));
        }
    }
}
