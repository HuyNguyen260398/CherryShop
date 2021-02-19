using CherryShop_API.Contracts;
using CherryShop_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Category entity)
        {
            await db.Categories.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Category entity)
        {
            db.Categories.Remove(entity);
            return await Save();
        }

        public async Task<IList<Category>> FindAll()
        {
            var categories = await db.Categories
                .Include(q => q.Products)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> FindById(int id)
        {
            var category = await db.Categories
                .Include(q => q.Products)
                .FirstOrDefaultAsync(q => q.Id == id);
            return category;
        }

        public async Task<bool> IsExists(int id)
        {
            return await db.Categories.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Category entity)
        {
            db.Categories.Update(entity);
            return await Save();
        }
    }
}
