using System.Linq;
using Microsoft.Extensions.Options;
using NetCoreKit.Infrastructure.EfCore.Db;

namespace NetCoreKit.Infrastructure.EfCore.Postgres
{
    public sealed class DbConnStringFactory : IDbConnStringFactory
    {
        public DbOptions DbOptions { get; }

        public DbConnStringFactory()
        {
            var config = ConfigurationHelper.GetConfiguration();
            var dbSection = config.GetSection("Features:EfCore:PostgresDb");
            DbOptions = new DbOptions {
                ConnString = dbSection["ConnString"],
                FQDN = dbSection["FQDN"],
                Database = dbSection["Database"],
                UserName = dbSection["UserName"],
                Password = dbSection["Password"]
            };
        }

        public DbConnStringFactory(IOptions<DbOptions> options)
        {
            DbOptions = options.Value;
        }

        public string Create()
        {
            var connPattern = DbOptions.ConnString;
            var connConfigs = DbOptions.FQDN?.Split(':');
            var fqdn = connConfigs?.First();
            var port = connConfigs?.Except(new[] {fqdn}).First();

            return string.Format(
                connPattern,
                fqdn, port,
                DbOptions.UserName,
                DbOptions.Password,
                DbOptions.Database);
        }
    }
}
