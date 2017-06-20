USE KisesaDSSClinicLinkSystem
GO 
--creates an encrypted table for DSS individual information 

IF OBJECT_ID('FF38D47A28F14F228E8EACA2491D4454','U') IS NOT NULL
	DROP TABLE FF38D47A28F14F228E8EACA2491D4454--DSSIndividuals
GO

CREATE TABLE FF38D47A28F14F228E8EACA2491D4454
(
 B51108AEFD034606936FAA3548122A2D varbinary(50) -- Id
,D601F9F5B38A48E79FC83C93CA8A6E04 varbinary(50) -- Abidance
,DE6AF4558C2448FCA9C1474A7E51272F varbinary(50) --idlong
,DBAEA5D569574246B4846BA46B6680B6 varbinary(50) --FirstName
,A2A53347A7BD4E67BFEA51465D9EF0CF varbinary(50)  --LastName
,AA454F2BF260456CAAB7343E5CF3B028 varbinary(50)  --Sex
,D67487DAC20D46B1955DFEC8E99F0464 varbinary(50)  --DoB
,F6D19436D7674395930D7FEC93F4FA87 varbinary(50)  --BYear
,A06BACB292CB48E09B1F2E665DD8CD83 varbinary(50)  --BMonth	
,E7896926FB634CC99EE0EBAFBA915BEC varbinary(50)  --BDay
,E49BDD61F5634F20A84EAC496B0B2176 varbinary(50)  --HHNumber
,BD095B093BF847AA8822EDBF7C5B74E5 varbinary(50)  --bcode
,A767FC7E407D4E38BBAA8A81F84F0E9E varbinary(50)  --hhfname
,F3F76BB6397C42ECA520A997FB0D214C varbinary(50)  --hhsname
,EE6F6DC46CDE4DA89F750A137A1E0897 varbinary(50)  --LineNumber
,BB29DE4034BE43A28F995D17B9422966 varbinary(50)  --Village
,B8D783A25CF7404E8B26C056A4F9D073 varbinary(50)  --VillageName
,CD5A37C76D2C43E283670D19DD768F3E varbinary(50)  --SubVillage
,A4C267941EE844079337557CD7B02FE4 varbinary(50)  --SubVillageName
,F0DA3D244B2848DB9CF2CF8557614492 varbinary(50)  --TenCell
,BED7D097892F4E53842842CDE6A6A28F varbinary(50)  --balozi_first_name
,F792C0E79026416886DA6E56FD4ED0D9 varbinary(50)  --balozi_second_name
,E57B658C823E48B89EBA945FABBC2E4E varbinary(50)  --EventStartDate
,AF7D083730D245B993000E7584273EA8 varbinary(50)  --EventEndDate
,DF217268EED949BDA7267B853E6CAC79 varbinary(50)  --StartEventName
,BFC564F2A5B44A13A875C59E8B98D0E0 varbinary(50)  --EndEventName
,ABE0738956CF47809BE9EDC60E749D18 varbinary(50)  --Current_Status
)

--==================================
-- GRANT PRIVILAGES TO USERS  
-- DESCRIPTION: 
--===================================

GRANT INSERT,SELECT, UPDATE
ON FF38D47A28F14F228E8EACA2491D4454
TO Admin
GO

/*
SELECT *
FROM DSSIndividuals
*/

--==========================================================================================================
--insert records from DSSIndividualsXX to encrypted ADSSIndividual table FF38D47A28F14F228E8EACA2491D4454
--========================================================================================================== 

DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';

INSERT INTO FF38D47A28F14F228E8EACA2491D4454
SELECT CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(Id AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(AbidanceID AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(CAST(IdLong AS BIGINT) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(FirstName) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(LastName) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(Sex) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(DoB AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(DATEPART(YEAR,DoB) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(DATEPART(MONTH,DoB) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(DATEPART(DAY,DoB) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(CAST(HHNumber AS BIGINT) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(bcode AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(hhfname) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(hhsname) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(LineNumber AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(Village AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(VillageName) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(SubVillage AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(SubVillageName) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(TenCell AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(balozi_first_name) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(UPPER(balozi_second_name) AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(ResidenceStartDate AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(ResidenceEndDate AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(StartEventName AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(EndEventName AS VARCHAR(25))))
,CONVERT(VARBINARY(50),ENCRYPTBYPASSPHRASE (@PassPhrase, CAST(current_status AS VARCHAR(25))))
FROM KisesaDSS..DSSIndividuals29
WHERE Current_status <>'D'  -- takes out deaths
GO


 --=======================================
-- CREATE PROC
-- NAME: spAP_BCFA7EB7D843466A995BF730FEA64255
-- DESCRIPTION: SELECT a records of members that an individual stays with from DSSIndividuals Table
 --SELECT REPLACE(UPPER(NEWID()),'-','') AS KEY1
--=======================================
IF OBJECT_ID('spAP_BCFA7EB7D843466A995BF730FEA64255','P') IS NOT NULL
	DROP PROC spAP_BCFA7EB7D843466A995BF730FEA64255
GO

CREATE PROC spAP_BCFA7EB7D843466A995BF730FEA64255
--IDENTIFERS
	 @Id VARCHAR(25)
	,@location  VARCHAR(25)
	    
WITH ENCRYPTION
AS
BEGIN
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';

BEGIN TRY	
BEGIN TRAN

SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.DBAEA5D569574246B4846BA46B6680B6 )  AS VARCHAR(25))AS  FirstName
,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.A2A53347A7BD4E67BFEA51465D9EF0CF )  AS VARCHAR(25))AS  LastName
FROM FF38D47A28F14F228E8EACA2491D4454 i
JOIN FF38D47A28F14F228E8EACA2491D4454 h 
ON CAST(DECRYPTBYPASSPHRASE(@PassPhrase,i.E49BDD61F5634F20A84EAC496B0B2176 ) AS VARCHAR(25)) = --HHNumber
    CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.E49BDD61F5634F20A84EAC496B0B2176 )AS VARCHAR(25))
    AND CAST(DECRYPTBYPASSPHRASE(@PassPhrase,i.DE6AF4558C2448FCA9C1474A7E51272F ) AS VARCHAR(25)) <> --idLong
      CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.DE6AF4558C2448FCA9C1474A7E51272F) AS VARCHAR(25))
WHERE CAST(DECRYPTBYPASSPHRASE(@PassPhrase,i.DE6AF4558C2448FCA9C1474A7E51272F ) AS VARCHAR(25)) = @Id
AND   CAST(DECRYPTBYPASSPHRASE(@PassPhrase,i.E49BDD61F5634F20A84EAC496B0B2176 ) AS VARCHAR(25))= @location 
ORDER BY CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.DBAEA5D569574246B4846BA46B6680B6 )  AS VARCHAR(25)),--FirstName
	     CAST(DECRYPTBYPASSPHRASE(@PassPhrase,h.A2A53347A7BD4E67BFEA51465D9EF0CF )  AS VARCHAR(25)) ASC--LastName
COMMIT TRAN
	
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	
	DECLARE @M varchar(500)
	SET @M = ERROR_MESSAGE() 
	SET @M = 'Select household member records failed!' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
	RAISERROR(@M,16,1)
	
END CATCH
END
GO

--==================================
-- GRANT PRIVILAGES TO USERS  
-- DESCRIPTION: 
--===================================
GRANT EXECUTE
ON spAP_BCFA7EB7D843466A995BF730FEA64255
TO Admin
GO

--====================================
--Create table to hold Probabilities And Weights
--====================================

IF OBJECT_ID(N'ProbsAndWeights','U') IS NOT NULL
	DROP TABLE ProbsAndWeights;
GO

CREATE TABLE [dbo].[ProbsAndWeights](
		 Variable varchar(25) not null
		,m float 
		,u float 
		,agree float
		,disagree float
		)
GO

INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'FirstName', 0.865168539, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'MiddleName', 0.865168539, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'LastName', 0.845906902, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'TLFirstName', 0.865168539, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'TLMiddleName', 0.865168539, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'TLLastName', 0.845906902, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'Gender', 0.99, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'BDay', 0.569823435, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'BMonth', 0.629213483, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'BYear', 0.796147673, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'Village', 0.894060995, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'SubVillage', 0.894060995, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'HMemberFirstName', 0.523274478, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'HMemberMiddleName', 0.523274478, NULL, NULL, NULL)
INSERT ProbsAndWeights (Variable, m, u, agree, disagree) VALUES (N'HMemberLastName', 0.523274478, NULL, NULL, NULL)
GO


--=======================================
-- CREATE PROC
-- NAME: spAP_FC287F11030F4901B63A6EE22FC62CE4
-- DESCRIPTION: SELECT TOP 20 DSS Individuals based on 
-- specified matching criteria
--=======================================
IF OBJECT_ID('spAP_FC287F11030F4901B63A6EE22FC62CE4','P') IS NOT NULL
	DROP PROC spAP_FC287F11030F4901B63A6EE22FC62CE4
GO


--spAP_FC287F11030F4901B63A6EE22FC62CE4 'MARIA','','CHELEMENGE','','','F','1975-02-24','','','','KISESA','KISESA',1,0,1,0,0,1,1,0,0,0,1,1

CREATE PROC spAP_FC287F11030F4901B63A6EE22FC62CE4
		@FirstName VARCHAR(25)
		,@MiddleName VARCHAR(25)
		,@LastName VARCHAR(25)
		,@TLFirstName VARCHAR(25)
		,@TLMiddleName VARCHAR(25)
		,@TLLastName VARCHAR(25)
		,@Gender VARCHAR(25)
		,@BDay VARCHAR(25)
		,@BMonth VARCHAR(25)
		,@BYear VARCHAR(25)
		,@Village VARCHAR(25)
		,@SubVillage VARCHAR(25)
		--,@HMemberFirstName VARCHAR(25)
		--,@HMemberMiddleName VARCHAR(25)
		--,@HMemberLastName VARCHAR(25)
		,@UseFirstName BIT
		,@UseMiddleName BIT
		,@UseLastName BIT
		,@UseTLFirstName BIT
		,@UseTLMiddleName BIT
		,@UseTLLastName BIT
		,@UseGender BIT
		,@UseBDay BIT
		,@UseBMonth BIT
		,@UseBYear BIT
		,@UseVillage BIT
		,@UseSubVillage BIT
		
		----Blocking
		--,@BlockOnGender BIT
		--,@BlockOnVillage BIT
		--,@BlockOnBYear BIT
WITH ENCRYPTION
AS
BEGIN

BEGIN TRY	


DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';
/*
SELECT ',CAST(DECRYPTBYPASSPHRASE(@PassPhrase,'+COLUMN_NAME+')  AS VARCHAR(25)) AS'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'FF38D47A28F14F228E8EACA2491D4454'
*/
SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase,DE6AF4558C2448FCA9C1474A7E51272F)  AS VARCHAR(25)) AS IdLong
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,DBAEA5D569574246B4846BA46B6680B6)  AS VARCHAR(25)) AS FirstName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,A2A53347A7BD4E67BFEA51465D9EF0CF)  AS VARCHAR(25)) AS LastName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,AA454F2BF260456CAAB7343E5CF3B028)  AS VARCHAR(25)) AS Sex
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,D67487DAC20D46B1955DFEC8E99F0464)  AS VARCHAR(25)) AS DoB
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,F6D19436D7674395930D7FEC93F4FA87)  AS VARCHAR(25)) AS BYear
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,A06BACB292CB48E09B1F2E665DD8CD83)  AS VARCHAR(25)) AS BMonth
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,E7896926FB634CC99EE0EBAFBA915BEC)  AS VARCHAR(25)) AS BDay
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,E49BDD61F5634F20A84EAC496B0B2176)  AS VARCHAR(25)) AS HHNumber
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,BD095B093BF847AA8822EDBF7C5B74E5)  AS VARCHAR(25)) AS bcode
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,A767FC7E407D4E38BBAA8A81F84F0E9E)  AS VARCHAR(25)) AS hhfname
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,F3F76BB6397C42ECA520A997FB0D214C)  AS VARCHAR(25)) AS hhsname
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,EE6F6DC46CDE4DA89F750A137A1E0897)  AS VARCHAR(25)) AS Linenumber
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,BB29DE4034BE43A28F995D17B9422966)  AS VARCHAR(25)) AS Village
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,B8D783A25CF7404E8B26C056A4F9D073)  AS VARCHAR(25)) AS VillageName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,CD5A37C76D2C43E283670D19DD768F3E)  AS VARCHAR(25)) AS SubVillage
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,A4C267941EE844079337557CD7B02FE4)  AS VARCHAR(25)) AS SubVillageName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,F0DA3D244B2848DB9CF2CF8557614492)  AS VARCHAR(25)) AS TenCell
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,BED7D097892F4E53842842CDE6A6A28F)  AS VARCHAR(25)) AS balozi_first_name --TLFirstName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,F792C0E79026416886DA6E56FD4ED0D9)  AS VARCHAR(25)) AS balozi_second_name --TLLastName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,E57B658C823E48B89EBA945FABBC2E4E)  AS VARCHAR(25)) AS ResStartDate
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,AF7D083730D245B993000E7584273EA8)  AS VARCHAR(25)) AS ResEndDate
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,DF217268EED949BDA7267B853E6CAC79)  AS VARCHAR(25)) AS StartEventName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,BFC564F2A5B44A13A875C59E8B98D0E0)  AS VARCHAR(25)) AS EndEventName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase,ABE0738956CF47809BE9EDC60E749D18)  AS VARCHAR(25)) AS CurrentStatus
		,cast(null as float) as FirstNameScore
		,cast(null as float) as LastNameScore
		,cast(null as float) as MiddleNameScore
		,cast(null as float) as TLFirstNameScore
		,cast(null as float) as TLMiddleNameScore
		,cast(null as float) as TLLastNameScore
		,cast(null as float) as GenderScore
		,cast(null as float) as BirthDayScore
		,cast(null as float) as BirthMonthScore
		,cast(null as float) as BirthYearScore
		,cast(null as float) as VillageScore
		,cast(null as float) as JWFirstNameFirstName   --dbo.JaroWinkler(@FirstName,FirstName)
		,cast(null as float) as JWFirstNameLastName   --dbo.JaroWinkler(@FirstName,LastName)
		,cast(null as float) as JWMiddleNameFirstName   --dbo.JaroWinkler(@MiddleName,FirstName)
		,cast(null as float) as JWMiddleNameLastName   --dbo.JaroWinkler(@MiddleName,LastName)
		,cast(null as float) as JWLastNameFirstName   --dbo.JaroWinkler(@LastName,FirstName)
		,cast(null as float) as JWLastNameLastName   --dbo.JaroWinkler(@LastName,LastName)
		,cast(null as float) as JWTLFirstNameBFirstName   --dbo.JaroWinkler(@TLFirstName,balozi_first_name)
		,cast(null as float) as JWTLFirstNameBSecondName   --dbo.JaroWinkler(@TLFirstName,balozi_second_name)
		,cast(null as float) as JWTLMiddleNameBFirstName   --dbo.JaroWinkler(@TLMiddleName,balozi_first_name)
		,cast(null as float) as JWTLMiddleNameBSecondName   --dbo.JaroWinkler(@TLMiddleName,balozi_second_name)
		,cast(null as float) as JWTLLastNameBFirstName  --dbo.JaroWinkler(@TLLastName,balozi_first_name)
		,cast(null as float) as JWTLLastNameBSecondName   --dbo.JaroWinkler(@TLLastName,balozi_second_name)
		

into #MatchData
FROM FF38D47A28F14F228E8EACA2491D4454
WHERE 1=1	

update #MatchData 
set 
		 JWFirstNameFirstName=dbo.JaroWinkler(@FirstName,FirstName),
		 JWFirstNameLastName=dbo.JaroWinkler(@FirstName,LastName),
		 JWMiddleNameFirstName=dbo.JaroWinkler(@MiddleName,FirstName),
		 JWMiddleNameLastName=dbo.JaroWinkler(@MiddleName,LastName),
		 JWLastNameFirstName=dbo.JaroWinkler(@LastName,FirstName),
		 JWLastNameLastName=dbo.JaroWinkler(@LastName,LastName),
		 JWTLFirstNameBFirstName=dbo.JaroWinkler(@TLFirstName,balozi_first_name),
		 JWTLFirstNameBSecondName=dbo.JaroWinkler(@TLFirstName,balozi_second_name),
		 JWTLMiddleNameBFirstName=dbo.JaroWinkler(@TLMiddleName,balozi_first_name),
		 JWTLMiddleNameBSecondName=dbo.JaroWinkler(@TLMiddleName,balozi_second_name),
		 JWTLLastNameBFirstName=dbo.JaroWinkler(@TLLastName,balozi_first_name),
		 JWTLLastNameBSecondName=dbo.JaroWinkler(@TLLastName,balozi_second_name)

If @UseFirstName = 1
update 
       #MatchData
	   set FirstNameScore = 
		(
		CASE WHEN (CASE WHEN JWFirstNameFirstName > JWFirstNameLastName
		THEN JWFirstNameFirstName ELSE JWFirstNameLastName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='FirstName')
			/(
				SELECT SUM(CASE WHEN JWFirstNameFirstName >=  0.8
						OR JWFirstNameLastName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='FirstName'))
			/(1-(
			SELECT SUM(CASE WHEN JWFirstNameFirstName >=  0.8
					OR JWFirstNameLastName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) 
else
update 
       #MatchData
	   set FirstNameScore = 0

If @UseLastName = 1 
update 
       #MatchData
	   set LastNameScore=
		(
		CASE WHEN (CASE WHEN JWLastNameFirstName > JWLastNameLastName
		THEN JWLastNameFirstName ELSE JWLastNameLastName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='LastName')
			/(
				SELECT SUM(CASE WHEN JWLastNameFirstName >=  0.8
						OR JWLastNameLastName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='LastName'))
			/(1-(
			SELECT SUM(CASE WHEN JWLastNameFirstName >=  0.8
					OR JWLastNameLastName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) 
ELSE
update 
       #MatchData
	   set LastNameScore = 0


If @UseMiddleName  = 1 
update 
       #MatchData
	   set MiddleNameScore=
		(
		CASE WHEN (CASE WHEN JWMiddleNameFirstName > JWMiddleNameLastName
		THEN JWMiddleNameFirstName ELSE JWMiddleNameLastName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='MiddleName')
			/(
				SELECT SUM(CASE WHEN JWMiddleNameFirstName >=  0.8
						OR JWMiddleNameLastName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='MiddleName'))
			/(1-(
			SELECT SUM(CASE WHEN JWMiddleNameFirstName >=  0.8
					OR JWMiddleNameLastName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) 
ELSE
update 
       #MatchData
	   set MiddleNameScore = 0

if @UseTLFirstName=1
Update #MatchData
	set TLFirstNameScore=(
		CASE WHEN (CASE WHEN JWTLFirstNameBFirstName > JWTLFirstNameBSecondName
		THEN JWTLFirstNameBFirstName ELSE JWTLFirstNameBSecondName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLFirstName')
			/(
				SELECT SUM(CASE WHEN JWTLFirstNameBFirstName >=  0.8
						OR JWTLFirstNameBSecondName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLFirstName'))
			/(1-(
			SELECT SUM(CASE WHEN JWTLFirstNameBFirstName >=  0.8
					OR JWTLFirstNameBSecondName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		)
else
	update #MatchData set TLFirstNameScore=0

if @UseTLMiddleName=1
Update #MatchData
	set TLMiddleNameScore=(
		CASE WHEN (CASE WHEN JWTLMiddleNameBFirstName > JWTLMiddleNameBSecondName
		THEN JWTLMiddleNameBFirstName ELSE JWTLMiddleNameBSecondName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLMiddleName')
			/(
				SELECT SUM(CASE WHEN JWTLMiddleNameBFirstName >=  0.8
						OR JWTLMiddleNameBSecondName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLMiddleName'))
			/(1-(
			SELECT SUM(CASE WHEN JWTLMiddleNameBFirstName >=  0.8
					OR JWTLMiddleNameBSecondName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		)
else
	update #MatchData set TLMiddleNameScore=0

if @UseTLLastName=1
Update #MatchData
	set TLLastNameScore=(
		CASE WHEN (CASE WHEN JWTLLastNameBFirstName > JWTLLastNameBSecondName
		THEN JWTLLastNameBFirstName ELSE JWTLLastNameBSecondName END) >= 0.8 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLLastName')
			/(
				SELECT SUM(CASE WHEN JWTLLastNameBFirstName >=  0.8
						OR JWTLLastNameBSecondName >= 0.8 THEN 1 ELSE 0 END
						)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLLastName'))
			/(1-(
			SELECT SUM(CASE WHEN JWTLLastNameBFirstName >=  0.8
					OR JWTLLastNameBSecondName >= 0.8 THEN 1 ELSE 0 END
					)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		)
else
	update #MatchData set TLLastNameScore=0


SELECT *
		,
		--CASE WHEN @UseFirstName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWFirstNameFirstName > JWFirstNameLastName
		--THEN JWFirstNameFirstName ELSE JWFirstNameLastName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='FirstName')
		--	/(
		--		SELECT SUM(CASE WHEN JWFirstNameFirstName >=  0.8
		--				OR JWFirstNameLastName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='FirstName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWFirstNameFirstName >=  0.8
		--			OR JWFirstNameLastName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS FirstNameScore	
		--+
		--CASE WHEN @UseMiddleName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWMiddleNameFirstName > JWMiddleNameLastName
		--THEN JWMiddleNameFirstName ELSE JWMiddleNameLastName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='MiddleName')
		--	/(
		--		SELECT SUM(CASE WHEN JWMiddleNameFirstName >=  0.8
		--				OR JWMiddleNameLastName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='MiddleName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWMiddleNameFirstName >=  0.8
		--			OR JWMiddleNameLastName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS MiddleNameScore	
		--+CASE WHEN @UseLastName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWLastNameFirstName > JWLastNameLastName
		--THEN JWLastNameFirstName ELSE JWLastNameLastName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='LastName')
		--	/(
		--		SELECT SUM(CASE WHEN JWLastNameFirstName >=  0.8
		--				OR JWLastNameLastName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='LastName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWLastNameFirstName >=  0.8
		--			OR JWLastNameLastName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS LastNameScore		

		FirstNameScore + MiddleNameScore + LastNameScore + TLFirstNameScore + TLMiddleNameScore + TLLastNameScore 
		
		--CASE WHEN @UseTLFirstName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWTLFirstNameBFirstName > JWTLFirstNameBSecondName
		--THEN JWTLFirstNameBFirstName ELSE JWTLFirstNameBSecondName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLFirstName')
		--	/(
		--		SELECT SUM(CASE WHEN JWTLFirstNameBFirstName >=  0.8
		--				OR JWTLFirstNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLFirstName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWTLFirstNameBFirstName >=  0.8
		--			OR JWTLFirstNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS TLFirstNameScore
		--+CASE WHEN @UseTLMiddleName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWTLMiddleNameBFirstName > JWTLMiddleNameBSecondName
		--THEN JWTLMiddleNameBFirstName ELSE JWTLMiddleNameBSecondName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLMiddleName')
		--	/(
		--		SELECT SUM(CASE WHEN JWTLMiddleNameBFirstName >=  0.8
		--				OR JWTLMiddleNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLMiddleName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWTLMiddleNameBFirstName >=  0.8
		--			OR JWTLMiddleNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS TLMiddleNameScore
		--+CASE WHEN @UseTLLastName = 1 THEN
		--(
		--CASE WHEN (CASE WHEN JWTLLastNameBFirstName > JWTLLastNameBSecondName
		--THEN JWTLLastNameBFirstName ELSE JWTLLastNameBSecondName END) >= 0.8 
		--THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='TLLastName')
		--	/(
		--		SELECT SUM(CASE WHEN JWTLLastNameBFirstName >=  0.8
		--				OR JWTLLastNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--				)*1.0/COUNT(*) + 0.0000000000001 
		--		FROM #MatchData
		--		))/LOG(2) 
		--ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='TLLastName'))
		--	/(1-(
		--	SELECT SUM(CASE WHEN JWTLLastNameBFirstName >=  0.8
		--			OR JWTLLastNameBSecondName >= 0.8 THEN 1 ELSE 0 END
		--			)*1.0/COUNT(*) + 0.0000000000001 
		--	FROM #MatchData
		--	)))/LOG(2)  END 
		--) ELSE 0 END --AS TLLastNameScore
		+CASE WHEN @UseGender = 1 THEN
		(
		CASE WHEN Sex=@Gender
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='Gender')
			/(
				SELECT SUM(CASE WHEN Sex=@Gender THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='Gender'))
			/(1-(
			SELECT SUM(CASE WHEN Sex=@Gender THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END --AS GenderScore
		+CASE WHEN @UseBDay= 1 THEN
		(
		CASE WHEN DATEPART(DAY,dob)=@BDay
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='BDay')
			/(
				SELECT SUM(CASE WHEN DATEPART(DAY,dob)=@BDay THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='BDay'))
			/(1-(
			SELECT SUM(CASE WHEN DATEPART(DAY,dob)=@BDay THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END --AS BDayScore
		+CASE WHEN @UseBMonth= 1 THEN
		(
		CASE WHEN DATEPART(MONTH,dob)=@BMonth
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='BMonth')
			/(
				SELECT SUM(CASE WHEN DATEPART(MONTH,dob)=@BMonth THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='BMonth'))
			/(1-(
			SELECT SUM(CASE WHEN DATEPART(MONTH,dob)=@BMonth THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END --AS BMonthScore
		+CASE WHEN @UseBYear= 1 THEN
		(
		CASE WHEN ABS(DATEPART(YEAR,dob)-@BYear) <= 2 
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='BYear')
			/(
				SELECT SUM(CASE WHEN ABS(DATEPART(YEAR,dob)-@BYear) <= 2  THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='BYear'))
			/(1-(
			SELECT SUM(CASE WHEN ABS(DATEPART(YEAR,dob)-@BYear) <= 2  THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END --AS BYearScore
		+CASE WHEN @UseVillage= 1 THEN
		(
		CASE WHEN VillageName=@Village
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='Village')
			/(
				SELECT SUM(CASE WHEN VillageName=@Village THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='Village'))
			/(1-(
			SELECT SUM(CASE WHEN VillageName=@Village THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END --AS VillageScore
		+CASE WHEN @UseSubVillage= 1 THEN
		(
		CASE WHEN SubVillageName=@SubVillage
		THEN LOG((SELECT m FROM ProbsAndWeights WHERE Variable='SubVillage')
			/(
				SELECT SUM(CASE WHEN SubVillageName=@SubVillage THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
				FROM #MatchData
				))/LOG(2) 
		ELSE LOG((1-(SELECT m FROM ProbsAndWeights WHERE Variable='SubVillage'))
			/(1-(
			SELECT SUM(CASE WHEN SubVillageName=@SubVillage THEN 1 ELSE 0 END)*1.0/COUNT(*) + 0.0000000000001 
			FROM #MatchData
			)))/LOG(2)  END 
		) ELSE 0 END AS Score
into #MatchResults FROM #MatchData

SELECT TOP 20
			FirstName
			,LastName
			,Sex
			,CONVERT(NCHAR(10),CAST(DoB AS DATE),126) AS DoB
			,balozi_first_name AS TLFirstName
			,balozi_second_name AS TLLastName			
			,VillageName
			,SubVillageName
			,CONVERT(NCHAR(10),CAST(ResStartDate AS DATE),126) AS ResStartDate
			,CONVERT(NCHAR(10),CAST(ResEndDate AS DATE),126) AS ResEndDate
			,CurrentStatus
			,Score
			,IdLong
			,HHNumber
			,RANK() over(partition by 1 order by Score desc) as ScoreRankGap
			,DENSE_RANK() over(partition by 1 order by Score desc) as ScoreRankNoGap
			,ROW_NUMBER() over(partition by 1 order by Score desc) as RowNumber
			,FirstNameScore + MiddleNameScore + LastNameScore as NameScore
FROM #MatchResults
ORDER BY Score DESC

	
END TRY  
BEGIN CATCH
	
	DECLARE @M varchar(500)
	SET @M = ERROR_MESSAGE() 
	SET @M = 'Select ADSS individual record failed!' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
	RAISERROR(@M,16,1)
	
END CATCH
END
GO

--==================================
-- GRANT PRIVILAGES TO USERS  
-- DESCRIPTION: 
--===================================
GRANT EXECUTE
ON spAP_FC287F11030F4901B63A6EE22FC62CE4
TO Admin
GO