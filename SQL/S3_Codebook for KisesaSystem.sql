--=======================================
-- CREATE TABLE
-- NAME: FB30F35F3A0C460E9E96E33DFBA4BD5F
-- DESCRIPTION: CodeBook
-- information
--=======================================
/*--generate column names
 SELECT REPLACE(UPPER(NEWID()),'-','') AS KEY1
*/
USE  [KisesaDSSClinicLinkSystem]
go
IF OBJECT_ID('FB30F35F3A0C460E9E96E33DFBA4BD5F','U') IS NOT NULL
	DROP TABLE FB30F35F3A0C460E9E96E33DFBA4BD5F
GO
CREATE TABLE FB30F35F3A0C460E9E96E33DFBA4BD5F(
 F89DD26654256B5E527230D174365 VARBINARY(50) --Domain
,D2E35CCB2D7148BBB92CBAF919E65757 VARBINARY(50) --Code
,E0694BB7829745EBA0F17A46CE4712C2 VARBINARY(200) --ShortDescr
,DB24DF869B544D268B1D66568F072FFA VARBINARY(500) --Description
,F0BBE79E9A494C9481EF42F08282B78B VARBINARY(50) --Table
)
GO

/*
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316'
UPDATE [FB30F35F3A0C460E9E96E33DFBA4BD5F]
			SET [F89DD26654256B5E527230D174365]=CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, ''))--DOMAIN
				,[D2E35CCB2D7148BBB92CBAF919E65757]=CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, '')) --CODE
				,[E0694BB7829745EBA0F17A46CE4712C2]=CONVERT(VARBINARY(100),ENCRYPTBYPASSPHRASE (@PassPhrase, '')) --SHORTDESCRI
				,[DB24DF869B544D268B1D66568F072FFA]=CONVERT(VARBINARY(500),ENCRYPTBYPASSPHRASE (@PassPhrase, '')) --DESCRIPTION
				,[F0BBE79E9A494C9481EF42F08282B78B]=CONVERT(VARBINARY(500),ENCRYPTBYPASSPHRASE (@PassPhrase, ''))--TABLE
WHERE CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D2E35CCB2D7148BBB92CBAF919E65757) AS VARCHAR(25)) = ''
						
	GO


--
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316'
DELETE FB30F35F3A0C460E9E96E33DFBA4BD5F
WHERE CAST(DECRYPTBYPASSPHRASE(@PassPhrase, [F89DD26654256B5E527230D174365]) AS VARCHAR(25)) = 'AdvanceVisitReminder'
GO
*/

--=======================================
-- CREATE PROC
-- NAME: FB30F35F3A0C460E9E96E33DFBA4BD5F
-- DESCRIPTION: Insert Codebook
--=======================================

	
			--insert values
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316'
			
			INSERT INTO FB30F35F3A0C460E9E96E33DFBA4BD5F
			VALUES(	CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, 'VisitInfor'))  --DOMAIN
					,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, ''))      ---CODE
					,CONVERT(VARBINARY(200),ENCRYPTBYPASSPHRASE (@PassPhrase, 'PATIENT')) --SHORTDESCRI
					,CONVERT(VARBINARY(500),ENCRYPTBYPASSPHRASE (@PassPhrase, '')) --DESCRIPTION 
					,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, 'VisitInfor')) --TABLE
					 
					)
		      GO
		      
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316'
			
			INSERT INTO FB30F35F3A0C460E9E96E33DFBA4BD5F
			VALUES(	CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, 'VisitInfor'))  --DOMAIN
					,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, ''))      ---CODE
					,CONVERT(VARBINARY(200),ENCRYPTBYPASSPHRASE (@PassPhrase, 'TREAMENT SUPPORTER')) --SHORTDESCRI
					,CONVERT(VARBINARY(500),ENCRYPTBYPASSPHRASE (@PassPhrase, '')) --DESCRIPTION 
					,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, 'VisitInfor')) --TABLE
					 
					)
		      GO
		      
		      --=======================================
-- CREATE PROC 
-- NAME: spAP_CEA6A3CC06B543F392A4B5FE4B63A47B
-- DESCRIPTION: SELECT PATIENT STATUS OPTIONS
--=======================================

/*--generate column names
 SELECT REPLACE(UPPER(NEWID()),'-','') AS KEY1
*/
IF OBJECT_ID('spAP_CEA6A3CC06B543F392A4B5FE4B63A47B','P') IS NOT NULL
	DROP PROC spAP_CEA6A3CC06B543F392A4B5FE4B63A47B
GO

CREATE PROC spAP_CEA6A3CC06B543F392A4B5FE4B63A47B


WITH ENCRYPTION
AS
BEGIN

DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';

BEGIN TRY	
BEGIN TRAN

SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E0694BB7829745EBA0F17A46CE4712C2) AS VARCHAR(500)) [ShortDescr]
FROM FB30F35F3A0C460E9E96E33DFBA4BD5F
WHERE CAST(DECRYPTBYPASSPHRASE(@PassPhrase, F89DD26654256B5E527230D174365)AS VARCHAR(25)) ='VisitInfor'


COMMIT TRAN
	
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	
	DECLARE @M varchar(500)
	SET @M = ERROR_MESSAGE() 
	SET @M = 'Select Visit Info options failed!' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
	RAISERROR(@M,16,1)
	
END CATCH
END
GO


--==================================
-- GRANT PRIVILAGES TO USERS  
-- DESCRIPTION: 
--===================================

GRANT EXECUTE
ON spAP_CEA6A3CC06B543F392A4B5FE4B63A47B
TO Admin
GO
		      
