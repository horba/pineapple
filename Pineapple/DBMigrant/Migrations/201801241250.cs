using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201801241250 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "CREATE TABLE Followers(" +
                "CurrentID int FOREIGN KEY REFERENCES Users(ID)," +
                "TargetID int FOREIGN KEY REFERENCES Users(ID), " +
                "CONSTRAINT PK_Followers PRIMARY KEY(CurrentID, TargetID)" +
                ");";
            return query;
        }
        public string RevertQuery()
        {
            string query = "DROP TABLE Followers";

            return query;
        }
    }
}
