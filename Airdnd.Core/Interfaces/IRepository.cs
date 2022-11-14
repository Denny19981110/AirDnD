using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add( T entity );
        Task AddAsync( T entity );
        void AddRange( IEnumerable<T> entity );
        void Update( T entity );
        void Delete( T entity );
        Task DeleteAsync( T entity );
        void DeleteRange( IEnumerable<T> entity );
        IQueryable<T> GetAll();
        IQueryable<T> GetAllReadOnly();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression);
    }
}
