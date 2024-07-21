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
        protected readonly PositionContext _context;
        public BaseRepository(PositionContext context)
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

        public void BulkMergeADO(IEnumerable<T> entities, string table, string columns)
        {
            string TEMP_TABLE = $"Temp_{table}";
            const int BULK_TIMEOUT = 60 * 10;
            string TEMP_CREATE = @$"CREATE temp TABLE IF NOT EXISTS {TEMP_TABLE}
                (
                    positionid text  NOT NULL,
                    productid text NOT NULL,
                    clientid text NOT NULL,
                    date timestamp NOT NULL,
                    value numeric NOT NULL,
                    quantity numeric NOT NULL
                )";

            string TEMP_MERGE = @$"
            MERGE INTO {table} AS TARGET
            USING {TEMP_TABLE} AS Source
            ON TARGET.positionid = SOURCE.positionid
		        AND TARGET.date = source.date
            WHEN NOT MATCHED
	            THEN
		            INSERT (positionid,productid,clientid,date,value,quantity)
                    VALUES (SOURCE.positionid,SOURCE.productid,SOURCE.clientid,
                            SOURCE.date,SOURCE.value,SOURCE.quantity)
            WHEN MATCHED
	            THEN
		            UPDATE
		            SET
                    positionid = source.positionid, 
                    productid = source.productid, 
                    clientid = source.clientid, 
                    date = source.date, 
                    value = source.value, 
                    quantity = source.quantity; 
                    ";
            string TEMP_SELECT = $"select * from {TEMP_TABLE} limit 0";

            NpgsqlCommand cmd = null;
            NpgsqlConnection conn = (NpgsqlConnection)_context.Database.GetDbConnection();
            conn.Open();

            cmd = new NpgsqlCommand(string.Empty, conn);
            cmd.CommandTimeout = BULK_TIMEOUT;

            cmd.CommandText = TEMP_CREATE;
            cmd.ExecuteNonQuery();

            cmd.CommandText = TEMP_SELECT;

            using (NpgsqlBinaryImporter copyIn = conn.BeginBinaryImport($"COPY {TEMP_TABLE} ({columns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var entity in entities)
                {
                    copyIn.StartRow();

                    foreach (PropertyInfo property in entity.GetType().GetProperties())
                    {
                        object value = property.GetValue(entity);
                        NpgsqlDbType dbType = GetNpgsqlDbType(property.PropertyType);
                        copyIn.Write(value, dbType);
                    }
                }
                copyIn.Complete();
            }
            cmd.CommandText = TEMP_MERGE;
            _ = cmd.ExecuteNonQuery();

            cmd.CommandText = $"drop TABLE {TEMP_TABLE};";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void BulkMergeEF(IEnumerable<T> entities)
        {
            _context.BulkMerge(entities);
            return;
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
