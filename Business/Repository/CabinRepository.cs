using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CabinRepository : ICabinRepository
    {
        private readonly CabinDbContext db;
        private readonly IMapper mapper;

        public CabinRepository(CabinDbContext dbContext, IMapper mapper)
        {
            db = dbContext;
            this.mapper = mapper;
        }

        public async Task<CabinDTO> CreateCabinAsync(CabinDTO cabinDTO)
        {
            if ( cabinDTO == null )
            {
                return null;
            }
            else
            {
                var cabin = mapper.Map<CabinDTO, Cabin>(cabinDTO);
                var newentry = db.Cabins.Add(cabin);
                await db.SaveChangesAsync();
                return mapper.Map<Cabin, CabinDTO>(newentry.Entity);
            }
        }

        public async Task<int> DeleteCabin(int id)
        {
            Cabin cabin = await db.Cabins.FirstOrDefaultAsync(cabin => cabin.ID == id);
            if (cabin != null)
            {
                db.Cabins.Remove(cabin);
                db.Images.RemoveRange(db.Images.Where(i => i.CabinID == id));
                await db.SaveChangesAsync();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public async Task<List<CabinDTO>> GetAllCabinsAsync()
        {
            return mapper.Map<List<Cabin>, List<CabinDTO>>(await db.Cabins.Include(i => i.Images).ToListAsync());
        }

        public async Task<CabinDTO> GetCabinAsync(int id)
        {
            var cabin = await db.Cabins.Include(i=>i.Images).FirstOrDefaultAsync(cabin => cabin.ID == id);
            return mapper.Map<Cabin, CabinDTO>(cabin);
        }

        public async Task<CabinDTO> IsCabinUniqe(string name)
        {
            var cabin = await db.Cabins.FirstOrDefaultAsync(cabin => cabin.Name == name);
            if (cabin != null)
            {
                return mapper.Map<Cabin, CabinDTO>(cabin);
            }
            else
            {
                return null;
            }
        }

        public async Task<CabinDTO> UpdateCabinAsync(CabinDTO cabinDTO)
        {

            Cabin cabin = await db.Cabins.FirstOrDefaultAsync(cabin => cabin.ID == cabinDTO.ID);
            if (cabin == null)
            {
                return null;
            }
            else
            {

                cabin = mapper.Map(cabinDTO, cabin);
                db.Cabins.Update(cabin);
                await db.SaveChangesAsync();
                return cabinDTO;
            }
        }

        public async Task<List<CabinDTO>> GetAllAvailableCabinsAsync(DateTime? start, DateTime? end)
        {
            var cabins = db.Cabins.Include(i => i.Images).Where(c => !c.Bookings
            .Any(b => b.StartDate <= start && b.EndDate >= start & b.StartDate >= start && b.StartDate <= end));
            return mapper.Map<List<CabinDTO>>(cabins);
        }
    }
}
