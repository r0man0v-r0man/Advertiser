using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ContextFactory : IDesignTimeDbContextFactory<AdvertiserContext>
    {
        private static string DataConnectionString => new DbConfiguration().GetDataConnectionString();

        public AdvertiserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdvertiserContext>();
            optionsBuilder.UseSqlServer(DataConnectionString);
            return new AdvertiserContext(optionsBuilder.Options);
        }
    }
}
