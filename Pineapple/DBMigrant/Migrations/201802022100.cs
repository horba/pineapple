using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201802022100 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "DROP TABLE Sessions;";
            query += "CREATE TABLE Sessions (session_id varchar(255) NOT NULL Primary key, user_id int NOT NULL, date datetime NOT NULL)";
            return query;
        }

        public string RevertQuery()
        {
            string query = "DROP TABLE Sessions;";
            query += "CREATE TABLE Sessions (session_id int NOT NULL Primary key IDENTITY, user_id int NOT NULL)";
            return query;
        }
    }
}
