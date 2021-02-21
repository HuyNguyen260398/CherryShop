using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> IsExists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
