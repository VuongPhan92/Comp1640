using System.Data.Entity;

namespace devForum.Data
{
    public partial class SSTVN_ForumEntities : DbContext
    {
        public SSTVN_ForumEntities(string dbString)
            : base(dbString)
        {

        }
    }

    public partial class SSTVN_ForumEF : SSTVN_ForumEntities
    {
        public static string DbString = "SSTVN_ForumEntities";
        private string _username = "pms";
        private string _password = "simpson12345";

        public SSTVN_ForumEF()
            : base(SSTVN_ForumEF.DbString)
        {
            //this.Database.Connection.ConnectionString = GetConnectionString(@"105dev-staging\sstvn", "SSTVN_Forum", true);
            var cnstr = GetConnectionString(@"105dev-staging\sstvn", "SSTVN_Forum", true);
#if DEBUG
            cnstr = GetConnectionString(@"105dev-staging\sstvn", "SSTVN_Forum", true);
#else
            cnstr = GetConnectionString(@"DINHDO3F97\DDOSQLSERVER", "SSTVN_Forum");
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
