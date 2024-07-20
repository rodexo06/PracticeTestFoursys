using PracticeTestFoursys.Infra.Context;
using PracticeTestFoursys.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace PracticeTestFoursys.Infra.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PracticeTestFoursysContext _context;
        public BaseRepository(PracticeTestFoursysContext context)
        {
            _context = context;
        }
        public IDataReader ExecuteReaderProc(string storedProcedure, DbParameter parametros)
        {
            NpgsqlCommand cmd = (NpgsqlCommand)_context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcedure;
            setParameter(cmd, parametros);
            _context.Database.OpenConnection();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public bool ExecuteNonQueryProc(string storedProcedure, DbParameter parametros)
        {
            NpgsqlCommand cmd = (NpgsqlCommand)_context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            setParameter(cmd, parametros);
            _context.Database.OpenConnection();
            bool retorno;
            try
            {
                cmd.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception ex)
            {
                _ = "Erro: " + ex.ToString();
                retorno = false;
            }
            _context.Database.CloseConnection();
            return retorno;
        }
        public int LastIdTable(DbParameter parametros)
        {
            NpgsqlCommand cmd = (NpgsqlCommand)_context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "select max(isnull(@id,0))+1 as max from @table t";
            setParameter(cmd, parametros);
            _context.Database.OpenConnection();
            using var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return Convert.ToInt32(dr["max"]);
        }
        public IDataReader ExecuteReader(string query, DbParameter parametros)
        {
            NpgsqlCommand cmd = (NpgsqlCommand)_context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = query;
            setParameter(cmd, parametros);
            _context.Database.OpenConnection();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public bool ExecuteNonQuery(string query, DbParameter parametros)
        {
            _context.Database.OpenConnection();
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = query;
            bool retorno;
            try
            {
                cmd.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception ex)
            {
                _ = "Erro: " + ex.ToString();
                retorno = false;
            }
            _context.Database.CloseConnection();
            return retorno;
        }
        private void setParameter(NpgsqlCommand cmd, DbParameter dbParameter)
        {
            if (dbParameter != null)
            {
                for (int i = 0; i < dbParameter.List.Count; i++)
                {
                    cmd.Parameters.AddWithValue(dbParameter.List[i].Key.StartsWith("@") ? dbParameter.List[i].Key : "@" + dbParameter.List[i].Key, dbParameter.List[i].Value);
                }

            }

            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                if (cmd.Parameters[i].Value == null)
                {
                    cmd.Parameters[i].Value = DBNull.Value;
                }
                if (cmd.Parameters[i].NpgsqlDbType == NpgsqlDbType.Varchar)
                {
                    string paramValue = Convert.ToString(cmd.Parameters[i].Value);
                    if (paramValue.Length > 8000)
                    {
                        cmd.Parameters[i].Size = paramValue.Length;
                        cmd.Parameters[i].NpgsqlDbType = NpgsqlDbType.Text;
                    }
                }
            }
        }

    }
}
