using CherryShop_API.Contracts;
using CherryShop_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Services
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext db;

        public ImageRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Image entity)
        {
            await db.Images.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Image entity)
        {
            db.Images.Remove(entity);
            return await Save();
        }

        public async Task<IList<Image>> FindAll()
        {
            var images = await db.Images
                .Include(q => q.Product)
                .ThenInclude(q => q.Brand)
                .Include(q => q.Product)
                .ThenInclude(q => q.Category)
                .ToListAsync();
            return images;
        }

        public async Task<Image> FindById(int id)
        {
            var image = await db.Images
                .Include(q => q.Product)
                .ThenInclude(q => q.Brand)
                .Include(q => q.Product)
                .ThenInclude(q => q.Category)
                .FirstOrDefaultAsync(q => q.Id == id);
            return image;
        }

        public async Task<bool> IsExists(int id)
        {
            return await db.Images.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Image entity)
        {
            db.Images.Update(entity);
            return await Save();
        }
    }
}
