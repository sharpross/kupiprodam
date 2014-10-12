using KupiProdam.Entities.Entites;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace IdentityMySQLDemo
{
    public class MySqlInitializer : IDatabaseInitializer<KupiProdam.Startup.ApplicationDbContext>
    {
        public void InitializeDatabase(KupiProdam.Startup.ApplicationDbContext context)
        {
            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();
            }
            else
            {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                string.Format(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
                    "CREATE TABLE `seller` (`Id` int(11) NOT NULL AUTO_INCREMENT, `Name` varchar(255) NOT NULL, `Password` varchar(255) NOT NULL, `About` varchar(255) DEFAULT NULL, `Email` varchar(255) NOT NULL,  `MainPhone` varchar(255) DEFAULT NULL,  `Photo` longblob,  `Site` varchar(255) DEFAULT NULL,  `Skype` varchar(255) DEFAULT NULL,  `VKontakte` varchar(255) DEFAULT NULL,  `Facebook` varchar(255) DEFAULT NULL,  PRIMARY KEY (`Id`)) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;"));
                    //"CREATE TABLE `seller` (`Id` int(11) NOT NULL AUTO_INCREMENT, `Name` varchar(200) NOT NULL, `Password` varchar(200) NOT NULL, `About` varchar(200) DEFAULT NULL, `Email` varchar(200) NOT NULL,  `MainPhone` varchar(200) DEFAULT NULL,  `Photo` longblob,  `Site` varchar(200) DEFAULT NULL,  `Skype` varchar(200) DEFAULT NULL,  `VKontakte` varchar(200) DEFAULT NULL,  `Facebook` varchar(200) DEFAULT NULL,  PRIMARY KEY (`Id`)) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;"));
                    //"CREATE TABLE `seller` (`Id`(11) int NOT NULL AUTO_INCREMENT, `Name`(190) varchar NOT NULL,  PRIMARY KEY (`Id`)) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;"));

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
        }
    }
}