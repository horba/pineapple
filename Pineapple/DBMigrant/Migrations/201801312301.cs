﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigration.Migrations
{
    class _201801312301 : IMigration
    {
        public string ApplyQuery()
        {
            string query = "ALTER TABLE dbo.Tweets ADD idOfAuthor INT NULL;";
            return query;
        }
        public string RevertQuery()
        {
            string query = "ALTER TABLE dbo.Tweets DROP COLUMN idOfAuthor;";
            return query;
        }

    }
}
