using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201802242220 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "ALTER TABLE dbo.Tweets ALTER COLUMN text varchar(max);";
            return query;
        }
        public string RevertQuery()
        {
            string query = "ALTER TABLE dbo.Tweets ALTER COLUMN text varchar(50);";
            return query;
        }
    }
}
