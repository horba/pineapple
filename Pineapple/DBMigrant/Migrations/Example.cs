//////////////////////////////////////////////////////////////////
//                 Example class of migration                   //
//////////////////////////////////////////////////////////////////
// The class name must match the following format: 197001011230 //
//                                                              //
// Where:   1970   01   01  12    30                            //
//          year month day hour minute                          //
//                                                              //
// The class must implement the interface IMigration            //
//////////////////////////////////////////////////////////////////

namespace DBMigration.Migrations
{
    class Example : IMigration
    {
        public string ApplyQuery()
        {
            //your query for database
            string query = "ALTER TABLE dbo.Tweets ADD name VARCHAR(20) NULL;";

            //You may formate your query as you want, but this query should be valid.
            //For example, next line may add as (All lines must have revert line):
            //
            // query += "ALTER TABLE dbo.Tweets ADD secondName VARCHAR(100) NULL;";
            //

            return query;
        }
        public string RevertQuery()
        {
            //your revert query for database
            string query = "ALTER TABLE dbo.Tweets DROP COLUMN name;";

            //query += "ALTER TABLE dbo.Tweets DROP COLUMN secondName;";

            return query;
        }
    }
}
