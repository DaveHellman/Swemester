using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public interface IImageRepository
    {
        public Task<int> AddProductImage(ImageDTO image);
        public Task<int> RemoveImageById(int imageid);
        public Task<int> RemoveImageByProductId(int cabinid);
        public Task<List<ImageDTO>> GetAllImagesByCabinId(int cabinid);
        public Task<ImageDTO> UpdateImage(ImageDTO image);
    }
}
