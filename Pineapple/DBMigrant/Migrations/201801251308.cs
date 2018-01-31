using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201801251308 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "CREATE TABLE Tweets(" +
                "id int IDENTITY(1,1) primary key," +
                "text varchar(50), " +
                "date datetime" +
                ");";
            return query;
        }
        public string RevertQuery()
        {
            string query = "DROP TABLE Tweets";
            return query;
        }

    }
}
