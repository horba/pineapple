DBCC CHECKIDENT (MigrationHistory, RESEED, 0)
INSERT  INTO dbo.MigrationHistory (FileNumber, Comment, DateApplied) VALUES ('0000', 'BaseLine', SYSDATETIME());