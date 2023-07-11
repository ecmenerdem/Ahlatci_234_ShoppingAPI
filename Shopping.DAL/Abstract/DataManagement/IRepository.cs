using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingAPI.Entity.Base;

namespace Shopping.DAL.Abstract.DataManagement
{
    public interface IRepository<T> where T : AuditableEntity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeProperties);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeProperties);
        Task<EntityEntry<T>> AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task RemoveAsync(T Entity);

    }
}
