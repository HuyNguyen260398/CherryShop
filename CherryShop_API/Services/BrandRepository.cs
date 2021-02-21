using CherryShop_API.Contracts;
using CherryShop_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Services
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext db;

        public BrandRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Brand entity)
        {
            await db.Brands.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Brand entity)
        {
            db.Brands.Remove(entity);
            return await Save();
        }

        public async Task<IList<Brand>> GetAll()
        {
            var brands = await db.Brands
                .Include(q => q.Products)
                .ToListAsync();
            return brands;
        }

        public async Task<Brand> GetById(int id)
        {
            var brand = await db.Brands
                .Include(q => q.Products)
                .FirstOrDefaultAsync(q => q.Id == id);
            return brand;
        }

        public async Task<bool> IsExists(int id)
        {
            return await db.Brands.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Brand entity)
        {
            db.Brands.Update(entity);
            return await Save();
        }
    }
}
