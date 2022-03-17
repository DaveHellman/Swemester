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
    public class ImageRepository : IImageRepository
    {
        private readonly CabinDbContext db;
        private readonly IMapper mapper;

        public ImageRepository(CabinDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ImageDTO> UpdateImage(ImageDTO imageDTO)
        {
            Image image = await db.Images.FirstOrDefaultAsync(image => image.ID == imageDTO.ID);
            if (image == null)
            {
                return null;
            }
            else
            {

                image = mapper.Map(imageDTO, image);
                db.Images.Update(image);
                await db.SaveChangesAsync();
                return imageDTO;
            }
        }

        public async Task<List<ImageDTO>> GetAllImagesByCabinId(int cabinid)
        {
            return mapper.Map<List<Image>, List<ImageDTO>>(
                await db.Images
                .Where(i => i.CabinID == cabinid)
                .ToListAsync());
        }

        public Task<int> AddProductImage(ImageDTO image)
        {
            throw new NotImplementedException();
        //    if (image != null)
        //    {
        //        var newImage = mapper.Map<ImageDTO, Image>(image);
        //        //db.Images.Add(newImage);
        //        return await db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        }

        public async Task<int> RemoveImageById(int imageid)
        {
            if (imageid != 0)
            {
                var obj = await db.Images.FirstOrDefaultAsync(i => i.ID == imageid);
                db.Images.Remove(obj);
                return await db.SaveChangesAsync();
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> RemoveImageByProductId(int cabinid)
        {
            if (cabinid != 0)
            {
                db.Images.RemoveRange(db.Images.Where(i => i.CabinID == cabinid));
                return await db.SaveChangesAsync();
            }
            else { return -1; }
        }
    }
}
