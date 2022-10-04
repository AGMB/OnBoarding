using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task SaveChangesAsync();
        void SaveChanges();
        Task AddAsync(T entity);
        void Update(T entity);
        IQueryable<T> GetAllIQuerable();
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
