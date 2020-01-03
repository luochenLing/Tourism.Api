using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Tourism.Util
{
    public class ConfigurationManager
    {
        public string GetSection(string section)
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

                var root = builder.Build();
                var sectionString = root.GetSection(section)?.Value;
                return sectionString;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetConnectionString(string conn)
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                var root = builder.Build();
                var connStr = root.GetConnectionString(conn);
                return connStr;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
