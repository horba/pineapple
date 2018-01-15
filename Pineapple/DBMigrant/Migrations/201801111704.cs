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
    class _201801111704 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "CREATE TABLE Users (Id int NOT NULL Primary key IDENTITY, Nick VARCHAR(50) NULL, FirstName VARCHAR(50) NULL, SecondName VARCHAR(50) NULL, Email VARCHAR(50) NULL, Password VARCHAR(50) NULL)";

            return query;
        }
        public string RevertQuery()
        {
            string query = "DROP TABLE Users";
            
            return query;
        }
    }
}
