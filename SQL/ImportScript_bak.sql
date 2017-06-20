USE KisesaDSSClinicLinkSystem
GO






-- get directory of eligible files
DECLARE
    @folder NVARCHAR(2048),
    @cmd VARCHAR(2048),
	@filename NVARCHAR(2048),
	@sql NVARCHAR(max),
	@MachineName varchar(255) = Host_Name();

SET @folder = N'C:\LinkSystemImport\';

SET @cmd = N'dir /b ' + @folder + '*.bak';

IF Object_ID('tempdb..#x') IS NOT NULL
	BEGIN 
		DROP TABLE #x;
	END;

CREATE TABLE #x(FileName NVARCHAR(2048));

INSERT #x EXEC [master].dbo.xp_cmdshell @cmd;

DELETE FROM #x WHERE FileName IS NULL;


UPDATE #x 
SET FileName = LEFT(FileName, LEN(FileName)-4)
FROM #x;
SELECT * FROM #x

IF (SELECT COUNT(*) FROM #x) != 3
BEGIN
	PRINT 'Number of backup files is not 3';
	-- put in a premature STOP to the script if there are not 3 files
END


--DROP existing backup databases
SET @sql = '';
SELECT @sql = @sql + 
'IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ([name] = ''' + FileName + ''')))  DROP DATABASE [' + FileName + '];' + CHAR(13) + CHAR(10)
FROM #x;
SELECT @sql;
PRINT @sql;
EXEC SP_EXECUTESQL @sql;



DECLARE @restore nvarchar(max) = '';
SELECT @restore = @restore + 'RESTORE DATABASE [' + FileName + '] FROM  DISK = N''C:\LinkSystemImport\' + FileName + '.bak'' WITH  FILE = 1,  NOUNLOAD,  STATS = 5;' + CHAR(13) + CHAR(10)
FROM #x;
SELECT @restore;
PRINT @restore;
EXEC SP_EXECUTESQL @restore;



-- create table names
IF Object_ID('tempdb..#tnames') IS NOT NULL
	DROP TABLE #tnames;
CREATE TABLE #tnames(TableName varchar(64));
INSERT INTO #tnames(TableName) 
VALUES ('DFDE4316996951AB2E6006E7'), ('CB1EFC4E471242FC8C2FE20241FE2E3E'), ('AAEE923ABF224A46A4DA8DC8D9CF8F05'), ('AC817925DEAC405280C826E890A53CE6');


-- delete tables where machinename is = importing machine
SET @restore = '';
SELECT @restore = @restore + 'DELETE dbo.[' + TableName + '] WHERE MachineName = SUBSTRING(''' + FileName + ''', 19, LEN(''' + FileName + ''')-18) AND MachineName <> ''' + @MachineName +  '''; INSERT INTO dbo.[' + TableName + '] SELECT * FROM [' + FileName + '].dbo.[' + TableName + '] where MachineName <> ''' + @MachineName +  ''';' + CHAR(13) + CHAR(10)
FROM dbo.#x CROSS JOIN dbo.#tnames;
SELECT @restore;
PRINT @restore;
EXEC SP_EXECUTESQL @restore;





-- drop the databases
SET @sql = '';
SELECT @sql = @sql + 'DROP database ' + FileName + ';' + CHAR(13) + CHAR(10)
FROM #x;
SELECT @sql;
PRINT @sql;
EXEC SP_EXECUTESQL @sql;
