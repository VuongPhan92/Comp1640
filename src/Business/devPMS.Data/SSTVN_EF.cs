using System.Data.Entity;

namespace devPMS.Data
{
    public partial class Entities : DbContext
    {
        public Entities(string dbString)
            : base(dbString)
        {
        }
    }

    public partial class SSTVN_EF : Entities
    {
        public static string DbString = "Entities";
        private string _username = "pms";
        private string _password = "simpson12345";

        public SSTVN_EF()
            : base(SSTVN_EF.DbString)
        {
            var cnstr = GetConnectionString(@"105dev-staging\sstvn", "SSTVN_ProjectManagement_Staging", true);
#if DEBUG
            cnstr = GetConnectionString(@"105dev-staging\sstvn", "SSTVN_ProjectManagement_Staging", true);
#else
            cnstr = GetConnectionString(@"DINHDO3F97\DDOSQLSERVER", "SSTVN_ProjectManagement_Staging");
#endif

            Database.Connection.ConnectionString = cnstr;
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
