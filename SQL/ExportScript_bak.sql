USE KisesaDSSClinicLinkSystem
GO

IF Object_ID('tempdb..#tnames') IS NOT NULL
	DROP TABLE #tnames;
CREATE TABLE #tnames(TableName varchar(64));
INSERT INTO #tnames(TableName) 
VALUES ('DFDE4316996951AB2E6006E7'), ('CB1EFC4E471242FC8C2FE20241FE2E3E'), ('AAEE923ABF224A46A4DA8DC8D9CF8F05'), ('AC817925DEAC405280C826E890A53CE6');


declare @CurrentDate varchar(25) = FORMAT(GETDATE(), 'yyyyMMddhhmmss');
declare @MachineName varchar(255) = Host_Name();
declare @databasename varchar(64) = 'DB_' + @CurrentDate + '_' + @MachineName;
declare @tname varchar(64);
declare @sql nvarchar(255) = 'CREATE Database ' + @databasename;
EXEC SP_EXECUTESQL @sql


declare tcursor cursor for 
	SELECT TableName
	FROM #tnames

	OPEN tcursor
    FETCH NEXT FROM tcursor INTO @tname

    WHILE @@FETCH_STATUS = 0
    BEGIN
		SET @sql = 'SELECT * INTO ' + @databasename + '.dbo.' + @tname + ' FROM dbo.' + @tname + ' where MachineName = ''' + @MachineName +  ''';';
		EXEC SP_EXECUTESQL @sql;

        FETCH NEXT FROM tcursor INTO @tname
    END

    CLOSE tcursor
    DEALLOCATE tcursor


DECLARE @backup nvarchar(1000)= 'BACKUP DATABASE [' + @databasename + '] TO  DISK = N''c:\LinkSystemExport\' + @databasename + '.bak'' WITH NOFORMAT, NOINIT,  NAME = N''' + @databasename + '-Full Database Backup'', SKIP, NOREWIND, NOUNLOAD,  STATS = 10';
EXEC SP_EXECUTESQL @backup
SET @sql = 'DROP database ' + @databasename;
EXEC SP_EXECUTESQL @sql





/*  

Table 1		Search attempts			dbo.DFDE4316996951AB2E6006E7
Table 2		Linked DSSID			dbo.CB1EFC4E471242FC8C2FE20241FE2E3E
Table 3		Match notes				dbo.AAEE923ABF224A46A4DA8DC8D9CF8F05
Table 4		Visits					dbo.AC817925DEAC405280C826E890A53CE6

*/