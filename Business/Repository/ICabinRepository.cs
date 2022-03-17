using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business.Repository
{
    public interface ICabinRepository
    {
        public Task<CabinDTO> GetCabinAsync(int id);
        public Task<List<CabinDTO>> GetAllCabinsAsync();
        public Task<CabinDTO> CreateCabinAsync(CabinDTO cabinDTO);
        public Task<CabinDTO> UpdateCabinAsync(CabinDTO cabinDTO);
        public Task<int> DeleteCabin(int id);
        public Task<CabinDTO> IsCabinUniqe(string name);
        public Task<List<CabinDTO>> GetAllAvailableCabinsAsync(DateTime? start, DateTime? end);
    }
}
