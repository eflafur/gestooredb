using Ecosystem.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem
{
    public class Program
    {
        static IConfiguration _config;
        static int kestrelPort;
        public static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();

            kestrelPort = _config.GetValue<int>("Kestrel:Port");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(opt =>
                    {
                        opt.ListenAnyIP(kestrelPort, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                        });
                    });
                    webBuilder
                    .UseConfiguration(_config)
                    .UseStartup<Startup>();
                });
    }



    //MIGRATION
    //        public static void Main(string[] args)
    //    {
    //        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
    //        CreateHostBuilder(args).Build().Run();
    //    }
    //    public static IHostBuilder CreateHostBuilder(string[] args)
    //    {
    //        var config = new ConfigurationBuilder()
    //          .SetBasePath(Directory.GetCurrentDirectory())
    //          .AddJsonFile("appsettings.json", true, false)
    //          .Build();

    //        var kestrelPort = config.GetValue<int>("Kestrel:Port");
    //        var kestrelHttpsPort = config.GetValue<int>("Kestrel:HttpsPort");
    //        var kestrelMaxRequestBufferSizeMB = config.GetValue<int>("Kestrel:MaxRequestBufferSizeMB");


    //            var builder = Host.CreateDefaultBuilder(args)
    //                .ConfigureWebHostDefaults(webBuilder =>
    //                {
    //                    webBuilder.UseKestrel(options =>
    //                    {
    //                        options.ListenAnyIP(kestrelPort, opt =>
    //                        {
    //                            opt.Protocols = HttpProtocols.Http1;
    //                        });
    //                    })
    //                    .UseConfiguration(config)
    //                    .UseStartup<Startup>();
    //                });
    //        return builder;
    //    }
    //}


    public class IOTManager : DbContext
    {
        public IOTManager(DbContextOptions<IOTManager> options)
            : base(options)
        {
        }

        public DbSet<userprofile> userprofile { get; set; }
        public DbSet<container> container { get; set; }
        public DbSet<service> service { get; set; }
        public DbSet<datacenter> datacenter { get; set; }
        public DbSet<application> application { get; set; }
        public DbSet<organigram> organigram { get; set; }
        public DbSet<tenant> tenant { get; set; }
        public DbSet<servicecategory> servicecategory { get; set; }
        public DbSet<rediskey> rediskey { get; set; }
        public DbSet<application_datacenter> application_datacenter { get; set; }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<userprofile>().HasKey(p => p.id);
            ModelBuilder.Entity<container>().HasKey(p => p.id);
            ModelBuilder.Entity<service>().HasKey(p => p.id);
            ModelBuilder.Entity<datacenter>().HasKey(p => p.id);
            ModelBuilder.Entity<application>().HasKey(p => p.id);
            ModelBuilder.Entity<organigram>().HasKey(p => p.id);
            ModelBuilder.Entity<tenant>().HasKey(p => p.id);
            ModelBuilder.Entity<servicecategory>().HasKey(p => p.id);
            ModelBuilder.Entity<application_datacenter>().HasKey(p => p.id);
            ModelBuilder.Entity<rediskey>().HasKey(p => p.id);
        }
    }
}