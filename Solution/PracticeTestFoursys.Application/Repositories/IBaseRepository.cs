using PracticeTestFoursys.Application.DependenciesInjections;
using System.Data;
using System.Linq.Expressions;

namespace PracticeTestFoursys.Application.Repositories
{
    [Injectable]
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<int> SaveAsync();
        void Update(T entity);
        void Remove(T entity);
        void RemoveList(IEnumerable<T> entity);
        void BulkInsertBinaryImporter(IEnumerable<T> entities, string table, string columnsList);
    }
}
