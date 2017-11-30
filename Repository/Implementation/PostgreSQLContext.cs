using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Repository.Implementation
{
    public class PostgreSQLContext : IDisposable, IBaseContext
    {
        private readonly NpgsqlConnection _dbConn;

        public PostgreSQLContext()
        {
            _dbConn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["NpgsqlConnection"].ConnectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //Any List TypeHandlers will be called here ~ EG: SqlMapper.TypeHandler<List<decimal>>
        }
        public PostgreSQLContext(string connectionString)
        {
            _dbConn = new NpgsqlConnection(connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //TypeHandlers if any
        }

        public T Select<T>(string sql, object parameters = null) where T : new()
        {
            using (_dbConn)
            {
                Open();
                var o = _dbConn.Query<T>(sql, parameters).FirstOrDefault();
                if (o != null)
                    return o;

                return new T();
            }
        }

        public List<T> SelectList<T>(string sql, object parameters = null)
        {
            using (_dbConn)
            {
                Open();
                return _dbConn.Query<T>(sql, parameters).ToList();
            }
        }

        public void InsertBulk<T>(string sql, object listPoco)
        {
            using (_dbConn)
            {
                Open();
                using (NpgsqlTransaction trans = _dbConn.BeginTransaction())
                {
                    _dbConn.Execute(sql, listPoco, transaction: trans);
                    trans.Commit();
                }
            }
        }

        #region Not Implemented
        public int Insert<T>(string sql, object poco)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(string sql, object poco)
        {
            throw new NotImplementedException();
        }

        public void Delete(string sql, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region helpers
        public void Dispose()
        {
            _dbConn.Close();
            _dbConn.Dispose();
        }

        public void Open()
        {
            if (_dbConn.State == ConnectionState.Closed)
                _dbConn.Open();
        }
        #endregion
    }
}
