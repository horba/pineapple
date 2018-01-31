using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201801151400 : IMigration
    {
        public string ApplyQuery()
        {
            string query =  "CREATE TABLE dbo.Tweets (" +
                            "id int NOT NULL PRIMARY KEY IDENTITY," +
                            "text VARCHAR(50) NULL," +
                            "date DATETIME NULL" +
                            "); ";

            return query;
        }

        public string RevertQuery()
        {
            string query = "DROP TABLE dbo.Tweets;";

            return query;
        }
    }
}
