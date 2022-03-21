using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecosystem.RepoService
{
    public class ExtractData<T> where T : class
    {
        private readonly IConfiguration config;

        public ExtractData(IConfiguration config)
        {
            this.config = config;
        }
        public T InsertTable(T value)
        {
            DbContextOptionsBuilder<IOTManager> DBBuilder = DbContextOptionsRelease.getDbContext();
            DBBuilder.UseNpgsql(config.GetValue<string>("ConnectionStrings:PostgreeAuth"));
            using (IOTManager IOTDBcontext = new IOTManager(DBBuilder.Options))
            {
                var _db = IOTDBcontext.Set<T>();
                IOTDBcontext.Add(value);
                IOTDBcontext.SaveChanges();
            }
            return value;
        }

        public object insertdataJson(string _model, string content, string pattern, string field)
        {
            object result;
            var connectionString = config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            NpgsqlTransaction trans = conn.BeginTransaction();
            try
            {
                using (var comm = new NpgsqlCommand("sql", conn))
                {
                    comm.CommandText = "rest_insert_data";
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("p_model", _model);
                    comm.Parameters.AddWithValue("p_json_in", content);
                    comm.Parameters.AddWithValue("p_json_value", pattern);
                    comm.Parameters.AddWithValue("p_field", field);
                    result = comm.ExecuteScalar();
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;
        }

        public int GetId()
        {
            return 55;
        }
    }
}
