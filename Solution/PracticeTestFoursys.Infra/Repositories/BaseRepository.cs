using PracticeTestFoursys.Infra.Context;
using PracticeTestFoursys.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data;
using PracticeTestFoursys.Infra.Utils;
using PracticeTestFoursys.Infra.Expressions;
using Npgsql;
using NpgsqlTypes;
using System.Reflection;


namespace PracticeTestFoursys.Infra.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PracticeTestFoursysContext _context;
        public BaseRepository(PracticeTestFoursysContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);

            return entity;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<T> GetAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (!includes.NullorEmpty())
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty.AsPath()));
            if (predicate is null) return await query.ToListAsync();

            return await query.Where(predicate).ToListAsync();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return;
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return;
        }
        public void RemoveList(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            return;
        }

        public async Task BulkInsertBinaryImporter(IEnumerable<T> entities, string table, string columns)
        {
            NpgsqlConnection conn = (NpgsqlConnection)_context.Database.GetDbConnection();
            using (NpgsqlBinaryImporter copyIn = conn.BeginBinaryImport($"COPY teachers ({columns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var entity in entities)
                {
                    await copyIn.StartRowAsync().ConfigureAwait(false);

                    foreach (PropertyInfo property in entity.GetType().GetProperties())
                    {
                        object value = property.GetValue(entity);
                        NpgsqlDbType dbType = GetNpgsqlDbType(property.PropertyType);
                        await copyIn.WriteAsync(value, dbType).ConfigureAwait(false);
                    }
                }
                await copyIn.CompleteAsync().ConfigureAwait(false);
            }
        }

        private NpgsqlDbType GetNpgsqlDbType(Type type)
        {
            if (type == typeof(string))
                return NpgsqlDbType.Varchar;
            if (type == typeof(int))
                return NpgsqlDbType.Integer;
            if (type == typeof(decimal))
                return NpgsqlDbType.Numeric;
            if (type == typeof(bool))
                return NpgsqlDbType.Boolean;
            if (type == typeof(DateTime))
                return NpgsqlDbType.Timestamp;

            throw new InvalidOperationException($"Tipo não suportado: {type}");
        }
    }
}
