using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProdMan_WASM.Services
{
    public class CabinService : ICabinService
    {
        private readonly HttpClient client;

        public CabinService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<CabinDTO> GetCabinAsync(int ID)
        {
            var respons = await client.GetAsync("api/Cabin/" + ID);
            var stringdata = await respons.Content.ReadAsStringAsync();
            var cabin = JsonConvert.DeserializeObject<CabinDTO>(stringdata);
            return cabin;
        }

        public async Task<List<CabinDTO>> GetCabinsAsync(DateTime? start, DateTime? end)
        {
            if (start != null & end != null)
            {
                var respons = await client.GetAsync("api/Cabin?start=" + start.ToString() + "&end=" + end.ToString());
                var stringdata = await respons.Content.ReadAsStringAsync();
                var cabins = JsonConvert.DeserializeObject<List<CabinDTO>>(stringdata);
                return cabins;
            }
            else
            {
                var respons = await client.GetAsync("api/Cabin");
                var stringdata = await respons.Content.ReadAsStringAsync();
                var cabins = JsonConvert.DeserializeObject<List<CabinDTO>>(stringdata);
                return cabins;
            }
        }
    }
}
