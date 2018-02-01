using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201802011728 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "CREATE TABLE Sessions (session_id int NOT NULL Primary key IDENTITY, user_id int NOT NULL)";
            return query;
        }

        public string RevertQuery()
        {
            return "DROP TABLE Sessions";
        }
    }
}
