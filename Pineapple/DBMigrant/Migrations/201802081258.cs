using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201802081258 : IMigration
    {

        public string ApplyQuery()
        {
            string query = "CREATE TABLE Log(" +
                "Id int IDENTITY(1,1) primary key," +
                "Date datetime," +
                "Thread varchar(255), " +
                "Level varchar(50), " +
                "Logger varchar(255), " +
                "Message varchar(4000), " +
                "Exception varchar(2000) " +
                ");";
            return query;
        }
        public string RevertQuery()
        {
            string query = "DROP TABLE Log";

            return query;
        }


    }
}
