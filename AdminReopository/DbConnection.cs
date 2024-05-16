using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class DbConnection
    {
        private readonly IConfiguration _configuration;
        public DbConnection(IConfiguration configuration) { 
        _configuration = configuration;
        }
        public string GetDbConnection()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
