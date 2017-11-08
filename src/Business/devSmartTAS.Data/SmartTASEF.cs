using System.Data.Entity;

namespace devSmartTAS.Data
{
    public partial class SmartTAS2012Entities : DbContext
    {
        public SmartTAS2012Entities(string dbString)
            : base(dbString)
        {

        }
    }

    public partial class SmartTASEF : SmartTAS2012Entities
    {
        public static string DbString = "SmartTAS2012Entities";
        private string _username = "pms";
        private string _password = "simpson12345";

        public SmartTASEF()
            : base(SmartTASEF.DbString)
        {
            this.Database.Connection.ConnectionString = GetConnectionString(@"105dev-staging\sstvn", "SmartTAS2012", true);
        }

        public string GetConnectionString(string serverName, string databaseName, bool isSQLServerAuthentication = false, string userName = "", string password = "")
        {
            var _connectionString = @"data source={0};initial catalog={1};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            if (isSQLServerAuthentication)
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    _username = userName;
                }

                if (!string.IsNullOrEmpty(password))
                {
                    _password = password;
                }
                _connectionString = @"data source={0};initial catalog={1};uid=" + _username + ";pwd=" + _password + ";MultipleActiveResultSets=True;App=EntityFramework";
            }
            return string.Format(_connectionString, serverName, databaseName); ;
        }
    }
}
