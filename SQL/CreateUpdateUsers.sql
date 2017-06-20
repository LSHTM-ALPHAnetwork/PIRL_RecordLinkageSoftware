--=======================================
-- CREATING USERS
-- NOTES:	1. Add user details into the Create Users section of this script
--			2. Run this script on each machine
----
-- DROPPING USERS (Make sure that the user is no longer needing access before running this script)
-- NOTES:   1. Remove user details from the Create Users section of this script
--          2. Add user details from the Drop Users section of this script
--          3. Run this script on each machine
----
-- ADD'L Note: If changing password to Admin, need to also update Visual Studio:
--			1. Open Settings.Settings file in Notepad and change two instances of password to new password
--			2. Open App.config file in Notepad and change one instance of password to new password
--=======================================


USE KisesaDSSClinicLinkSystem 
GO

-----------------------------------------------
/*             ADMINISTRATORS                */
-----------------------------------------------

--ADMIN 
IF EXISTS(SELECT * FROM master.sys.syslogins 
		WHERE name = 'Admin')
	DROP LOGIN [Admin]
GO

--CREATE LOGIN FROM WINDOWS WITH DEFAULT_DATABASE = [KisesaDSSClinicLinkSystem LHWDA], DEFAULT_LANGUAGE=[us_english];
CREATE LOGIN [Admin] WITH PASSWORD = '@Admin' --changed for GitHub

-- Add the user to the database using their login name and the user name
IF EXISTS(SELECT * from sys.sysusers
			WHERE name = 'Admin')
BEGIN
		DECLARE @sql nvarchar(1000);


		DECLARE user_cursor CURSOR FOR 
		SELECT 'EXEC sp_droprolemember '''+ rp.name + ''', '''+ mp.name + ''';'
		from sys.database_role_members drm
		  join sys.database_principals rp on (drm.role_principal_id = rp.principal_id)
		  join sys.database_principals mp on (drm.member_principal_id = mp.principal_id)
		where rp.name = 'linkapp'
		order by rp.name

		OPEN user_cursor

		FETCH NEXT FROM user_cursor 
		INTO @sql

		WHILE @@FETCH_STATUS = 0
		BEGIN
	
			EXEC sp_executeSQL @SQL
	
			FETCH NEXT FROM user_cursor 
			INTO @sql
		END
		CLOSE user_cursor;
		DEALLOCATE user_cursor;

	DROP ROLE [linkapp]
	DROP USER [Admin]
END
GO

CREATE USER [Admin]
	FOR LOGIN [Admin]
GO

IF DATABASE_PRINCIPAL_ID('linkapp') IS NULL
BEGIN
  CREATE ROLE linkapp AUTHORIZATION Admin;
END
ALTER ROLE [linkapp] ADD MEMBER Admin


-----------------------------------------------
/*             CREATE USERS                  */
-----------------------------------------------

--EMPLOYEE 1
IF EXISTS(SELECT * FROM master.sys.syslogins 
		WHERE name = 'Employee1')
	DROP LOGIN [Employee1]
GO
CREATE LOGIN [Employee1] WITH PASSWORD = 'P@ssword1'
IF EXISTS(SELECT * from sys.sysusers
			WHERE name = 'Employee1')
	DROP USER [Employee1]
GO
CREATE USER [Employee1]
	FOR LOGIN [Employee1]
GO
ALTER ROLE [linkapp] ADD MEMBER Employee1

--EMPLOYEE 2
IF EXISTS(SELECT * FROM master.sys.syslogins 
		WHERE name = 'Employee2')
	DROP LOGIN [Employee2]
GO
CREATE LOGIN [Employee2] WITH PASSWORD = 'P@ssword2'
IF EXISTS(SELECT * from sys.sysusers
			WHERE name = 'Employee2')
	DROP USER [Employee2]
GO
CREATE USER [Employee2]
	FOR LOGIN [Employee2]
GO
ALTER ROLE [linkapp] ADD MEMBER Employee2



-----------------------------------------------
/*               DROP USERS                  */
-----------------------------------------------
IF EXISTS(SELECT * FROM master.sys.syslogins 
		WHERE name = 'Employee2')
	DROP LOGIN [Employee2]
GO
IF EXISTS(SELECT * from sys.sysusers
			WHERE name = 'Employee2')
	DROP USER [Employee2]
GO







--==================================
-- GRANT PRIVILAGES TO ALL USERS  --
--==================================

GRANT INSERT,SELECT, UPDATE
ON CD251E3785F54A6583D24B4D9F23391F
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_F9B3186072CC43B786020D6914065B95
TO [linkapp]
GO
GRANT INSERT,SELECT, UPDATE
ON DFDE4316996951AB2E6006E7
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_DA2C8B1D8B3D4DF58F346673B3FA1024
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_E132EDAA8F404CB1A69BAE17EE47DD03
TO [linkapp]
GO
GRANT INSERT,SELECT, UPDATE
ON AC817925DEAC405280C826E890A53CE6
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_E9E31C7395EB4322817BBC578C1C4167
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_FE2AA0B3066347B1A54823DA8785B2F5
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_C7193C66FC1E43B496D3B78D2E19E528
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_CE6C38EA7BC4426796E9337016B932D3
TO [linkapp]
GO
GRANT INSERT,SELECT, UPDATE
ON CB1EFC4E471242FC8C2FE20241FE2E3E
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_E1C0DF3642614D7588FCA1B24CDD4272
TO [linkapp]
GO
GRANT EXECUTE
ON dbo.fnIT_F8B4A9F4F7334F0ABB83D253539A1D25
TO [linkapp]
GO
GRANT INSERT,SELECT, UPDATE
ON AAEE923ABF224A46A4DA8DC8D9CF8F05
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_B3823673C7634A76A840BA9AEE338D83
TO [linkapp]
GO
Grant Execute on spAP_FBBACFDDDA584A618694C9A4CBE7FD17
to [linkapp]
go
GRANT INSERT,SELECT, UPDATE
ON FF38D47A28F14F228E8EACA2491D4454
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_BCFA7EB7D843466A995BF730FEA64255
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_FC287F11030F4901B63A6EE22FC62CE4
TO [linkapp]
GO
GRANT EXECUTE
ON spAP_CEA6A3CC06B543F392A4B5FE4B63A47B
TO [linkapp]
GO
grant select 
on dbo.villages 
to [linkapp]
GO
grant select 
on dbo.v_Villages 
to [linkapp]
GO
grant select 
on dbo.v_SubVillages 
to [linkapp]
GO
grant execute 
on dbo.spGetSubVillages 
to [linkapp]
GO