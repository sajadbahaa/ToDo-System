using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary.GrenricRepo
{
    public interface IRepository<T, Tkey> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Tkey id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Tkey id);
    }
}
