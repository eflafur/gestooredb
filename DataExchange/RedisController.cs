using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataExchange
{
    public class RedisController
    {
        public  RedisController(IConfiguration config)
        {
            this.config = config;
        }
        IDatabase dbRedis;
        private readonly IConfiguration config;
        public string Publisher(string connectionParams,string key,string message)
        {
            var redisParam=config.GetValue<string>("redis:redishost"); 
            var mux = ConnectionMultiplexer.Connect(redisParam);
            dbRedis = mux.GetDatabase();
            dbRedis.StringSetAsync(key, message);
            return (connectionParams);
        }
    }
}
