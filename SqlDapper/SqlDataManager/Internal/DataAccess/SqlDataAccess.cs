using System.Configuration;

namespace SqlDataManager.Internal.DataAccess
{
    internal static class SqlDataAccess
    {
        public static string getconnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString
                ?? throw new ArgumentException($"Connection string '{name}' not found."); ;
        }
    }
}