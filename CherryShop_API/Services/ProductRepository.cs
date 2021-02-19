using CherryShop_API.Contracts;
using CherryShop_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Product entity)
        {
            await db.Products.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Product entity)
        {
            db.Products.Remove(entity);
            return await Save();
        }

        public async Task<IList<Product>> FindAll()
        {
            var products = await db.Products
                .Include(q => q.Brand)
                .Include(q => q.Category)
                .Include(q => q.Images)
                .ToListAsync();
            return products;
        }

        public async Task<Product> FindById(int id)
        {
            var product = await db.Products
                .Include(q => q.Brand)
                .Include(q => q.Category)
                .Include(q => q.Images)
                .FirstOrDefaultAsync(q => q.Id == id);
            return product;
        }

        public async Task<bool> IsExists(int id)
        {
            return await db.Products.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Product entity)
        {
            db.Products.Update(entity);
            return await Save();
        }
    }
}
