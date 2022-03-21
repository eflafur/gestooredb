using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.RepoService
{
    public class DbContextOptionsRelease
    {
        static  DbContextOptionsBuilder<IOTManager> builder = new DbContextOptionsBuilder<IOTManager>();
        private DbContextOptionsRelease(IConfiguration config)
        {
        }

        public static DbContextOptionsBuilder<IOTManager> getDbContext()
        {
            return builder;
        }
    }
}
