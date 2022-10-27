using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace HAHN.DataLayer
{
    public class ConfigSettings
    {
        private static IConfiguration _configuration;

        public ConfigSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static string HahnDataContextConnectionString;

        public static string HAHNDataContextConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(HahnDataContextConnectionString))
                {
                    var s = "Data Source={0};Initial Catalog={1};Persist Security Info=false;User ID={2};Password={3}";
                    var dataSource = _configuration.GetValue<string>("DataServer");
                    var catalogue = _configuration.GetValue<string>("Catalog");
                    var user = _configuration.GetValue<string>("DataUser");
                    var password = ("DataPassword");
                    HahnDataContextConnectionString = string.Format(s, dataSource, catalogue, user, password);
                }
                return HahnDataContextConnectionString;
            }
        }
    }
}