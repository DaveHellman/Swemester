using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public interface ICabinService
    {
        public Task<List<CabinDTO>> GetCabinsAsync(DateTime? start, DateTime? end);
        public Task<CabinDTO> GetCabinAsync(int ID);
    }
}
