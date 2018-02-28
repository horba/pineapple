namespace DBMigration.Migrations
{
    class _201801242320 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "ALTER TABLE dbo.Users ADD RegistrationDate DATETIME NULL";

            return query;
        }
        public string RevertQuery()
        {
            string query = "ALTER TABLE dbo.Users DROP COLUMN RegistrationDate";

            return query;
        }
    }
}
