using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shopping.DAL.Abstract.DataManagement;
using ShoppingAPI.Entity.Base;

namespace Shopping.DAL.Concrete.EntityFramework.DataManagement
{
    public class EfRepository<T> : IRepository<T> where T : AuditableEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbset;

        public EfRepository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }


        /* UserRepository.GetAsync(q=>q.id==18) */
        public async Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeProperties)
        {
            IQueryable<T> query = _dbset;
            query = query.Where(Filter);
            if (IncludeProperties.Length > 0)
            {
                foreach (var item in IncludeProperties)
                {
                    query = query.Include(item);
                }
            }

            return await query.SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeProperties)
        {
            IQueryable<T> query = _dbset;

            if (Filter != null)
            {
                query = query.Where(Filter);/* select * from product where id>5 */
            }

            if (IncludeProperties.Length > 0)
            {
                foreach (var item in IncludeProperties)
                {
                    query = query.Include(item);
                }
            }

            return await Task.Run(() => query);
        }

        /* UserRepository.AddAsync(user) */
        public async Task<EntityEntry<T>> AddAsync(T Entity)
        {
            return await _dbset.AddAsync(Entity);
        }

        public async Task UpdateAsync(T Entity)
        {
            await Task.Run(() => _dbset.Update(Entity));
        }

        public async Task RemoveAsync(T Entity)
        {
            await Task.Run(() => _dbset.Remove(Entity));
        }
    }
}
