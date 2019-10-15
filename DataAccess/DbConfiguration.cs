using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DbConfiguration : DbBaseConfiguration
    {
        private readonly string DataConnectionKey = "AdvertiserConnection";
        public string GetDataConnectionString()
        {
            return GetConfiguration().GetConnectionString(DataConnectionKey);
        }
    }
}
