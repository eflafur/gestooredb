using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Ecosystem.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Ecosystem.Utils;

namespace Ecosystem.RepoService
{
    public class InsertTable<T> : IInsertTable<T> where T : class
    {
        DbContextOptionsBuilder<IOTManager> _context;
        IConfiguration _config;
        public InsertTable(IConfiguration config)
        {
            _config = config;
            _context = new DbContextOptionsBuilder<IOTManager>();
        }
        public object getData(string procName, string _model)
        {
            var connectionString = _config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            NpgsqlTransaction trans = conn.BeginTransaction();

            using (var comm = new NpgsqlCommand("sql", conn))
            {
                comm.CommandText = procName;
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("p_entity", _model);
                var result = comm.ExecuteScalar();
                return result;
            }
        }
        public string insertData(T data, string procName = null)
        {
            _context.UseNpgsql(_config.GetValue<string>("ConnectionStrings:PostgreeAuth"));
            var type = data.GetType();
            var type2 = typeof(service);
            using (IOTManager db = new IOTManager(_context.Options))
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _db = db.Set<T>();
                        var nodes = _db.Select(x => x).ToList();
                        db.Add(data);
                        db.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception exec)
                    {
                        trans.Rollback();
                        throw exec;
                    }
                }
                return "ok";
            }
        }
        public string insertDataSp(string data)
        {
            var connectionString = _config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var JData = JObject.Parse(data.ToString());
            var model = JData["model"];
            var body = JData["data"];

            using (var comm = new NpgsqlCommand("sql", conn))
            {
                //                comm.CommandText = "rest_container_post";
                comm.CommandText = "rest_post";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("p_json_model", model.ToString());
                comm.Parameters.AddWithValue("p_json_in", body.ToString());
                var result = comm.ExecuteScalar().ToString();
                return result;
            }
        }
        public userprofile IsValidUserInformationFDirect(userprofile model)
        {
            _context.UseNpgsql(_config.GetValue<string>("ConnectionStrings:PostgreeAuth"));
            userprofile node;

            using (IOTManager db = new IOTManager(_context.Options))
            {
                node = db.userprofile.FirstOrDefault(k => k.username == model.username);
            }

            var connectionString = _config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            return node;
        }
        public IEnumerable<Dictionary<string, object>> GetDataByParam(string query)
        {
            var connectionString = _config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(query, conn);
            DataTable dt = new DataTable();
            IEnumerable<Dictionary<string, object>> JSONresult = null;

            try
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                JSONresult = utils.dt2dic(dt);
                //var jsonjson2 = DataConversion.DataTable2DictList(dt);
            }
            catch (Exception ex) { }
            conn.Dispose();
            return JSONresult;
        }

        public int CreateTable(string create)
        {
            var connectionString = _config.GetValue<string>("ConnectionStrings:PostgreeAuth");
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var command = new NpgsqlCommand(create, conn);
            try
            {
                var result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                conn.Close();
                conn.Dispose();
            }
            //using(NpgsqlConnection conn2=new NpgsqlConnection())
            //{
            //    conn.Open();
            //    using( NpgsqlCommand command2=new NpgsqlCommand(create,conn2))
            //    {
            //        command2.ExecuteNonQuery();
            //    }
            //    conn.Close();
            //}
            return 0;
        }
    }
}
