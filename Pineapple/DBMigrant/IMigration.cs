namespace DBMigration
{
    interface IMigration
    {
        string ApplyQuery();
        string RevertQuery();
    }
}
