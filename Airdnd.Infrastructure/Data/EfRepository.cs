using Airdnd.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Infrastructure.Data
{
    public class EfRepository<T> :IRepository<T> where T : class
    {
        protected readonly AirBnBContext DbContext;
        public EfRepository( AirBnBContext dbContext )
        {
            DbContext = dbContext;
        }

        public void Add( T entity )
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
        }
        public async Task AddAsync( T entity )
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }
        public void AddRange( IEnumerable<T> entity )
        {
            DbContext.Set<T>().AddRange(entity);
            DbContext.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Any(expression);
        }

        public void Delete( T entity )
        {
            DbContext.Set<T>().Remove(entity);
            DbContext.SaveChanges();
        }
        public async Task DeleteAsync( T entity )
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
        public void DeleteRange( IEnumerable<T> entity )
        {
            DbContext.Set<T>().RemoveRange(entity);
            DbContext.SaveChanges();
        }
        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }
        public IQueryable<T> GetAllReadOnly()
        {
            return DbContext.Set<T>().AsNoTracking();
        }

        public void Update( T entity )
        {
            DbContext.Set<T>().Update(entity);
            DbContext.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(expression);
        }
    }
}
