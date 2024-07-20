using PracticeTestFoursys.Application.DependenciesInjections;
using System.Data;

namespace PracticeTestFoursys.Application.Repositories
{
    [Injectable]
    public interface IBaseRepository<T> where T : class
    {
        IDataReader ExecuteReaderProc(string storedProcedure, DbParameter parametros);
        bool ExecuteNonQueryProc(string storedProcedure, DbParameter parametros);
        IDataReader ExecuteReader(string query, DbParameter parametros);

        int LastIdTable(DbParameter parametros);

        bool ExecuteNonQuery(string query, DbParameter parametros);
    }
}
