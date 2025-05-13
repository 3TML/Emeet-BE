using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Infrastructure.Data
{
    public class EmeetDbContext : DbContext
    {
        public EmeetDbContext() { }
        public EmeetDbContext(DbContextOptions<EmeetDbContext> dbContextOptions) : base(dbContextOptions) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        }
    }
}
