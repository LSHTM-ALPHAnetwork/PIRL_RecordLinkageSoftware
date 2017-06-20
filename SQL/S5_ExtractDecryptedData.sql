USE KisesaDSSClinicLinkSystem 
GO
--identifiers

DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';
/*
-------------------Script
	SELECT 	',CAST(DECRYPTBYPASSPHRASE(@PassPhrase, ' + COLUMN_NAME +') AS VARCHAR(25))'+ CHAR(13) + CHAR(10) 
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'CB1EFC4E4471242FC8C2FE20241FE2E3E'
--------------------------------------------------------------
*/
--BEGIN TRY	
--BEGIN TRAN
SELECT 	CAST(DECRYPTBYPASSPHRASE(@PassPhrase, SessionID) AS VARCHAR(25))  AS SessionID
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, B03075FD1514479690ED5FEDA8B629C6) AS VARCHAR(25))  AS RecordNo
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A0492431ADB745FA949E9876393FEC7E) AS VARCHAR(25))  AS HealthFacility
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, F89DD26654256B5E527230D174365) AS VARCHAR(25))  AS HealthFacilityDepartment
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, BBEB00C8CA1A4DF0B969FDE821B23417) AS VARCHAR(25))  AS RegistrationDate
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, C4889A57F82B42B696DEE25F570F6496) AS VARCHAR(25))  AS FileRef
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, AAD6BA9F1CBB4861820A11ABB5C53A86) AS VARCHAR(25))  AS UniqueCTCIDNumber
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, FBBC6C2FAC4047D3AF06FD8519B47313) AS VARCHAR(25))  AS TGRFormNumber
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pCTCIDInfant) AS VARCHAR(25))  CTCIDInfant
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pUniqueHTCID) AS VARCHAR(25))  UniqueHTCID
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pUniqueANCID) AS VARCHAR(25))  UniqueANCID
	    ,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pANCIDInfant) AS VARCHAR(25))  ANCIDInfant
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pHEIDInfant) AS VARCHAR(25))  HEIDInfant
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A6D47617430D8737E2A9C7CA176F) AS VARCHAR(25))  AS FirstName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E73CCEDA4CF7445EB5FF71B84282AE4B) AS VARCHAR(25))  AS MiddleName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, DD50A64B30564134AA523C76DCC2CD94) AS VARCHAR(25))  AS LastName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A014C368F5E6469BBA04F2CDF502334E) AS VARCHAR(25))  AS Sex
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, AB44DE55ABC64282A1FBC8E07DCA7E4B) AS VARCHAR(25))  AS YearofBirth
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E1EE5FBA27EE4E7291B964D7763CA3B1) AS VARCHAR(25))  AS MonthofBirth
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E5DEBDB3E4844A22AF25165B687997C2) AS VARCHAR(25))  AS DayofBirth
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, CA28218807DE4870AF26F4C6274F2FBA) AS VARCHAR(25))  AS VillageName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, EC87DE4886EC4F23A270A5C0225CA63C) AS VARCHAR(25))  AS SubVillageName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D86C72DE6CE14B549CE4E9F93938576E) AS VARCHAR(25))  AS ResidenceStartYear
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pNeverDSS) AS VARCHAR(25))  AS NeverInDSSArea
		--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, EDB1F900027F428EB35A4070203F50B8) AS VARCHAR(25))  AS ResidenceStartMonth
		--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, F5E81AB3ACC3492395F1BAA7A657DAD2) AS VARCHAR(25))  AS ResidenceStartDay
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, DD517CF9CC68408EA0396CB56BED6CA7) AS VARCHAR(25))  AS TenCellLeaderFName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, CDCDD6B93EE14D53BCD0BA047E6DB8ED) AS VARCHAR(25))  AS TenCellLeaderMName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E62B5829956A4C498B359E16647DC1AC) AS VARCHAR(25))  AS TenCellLeaderLName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, DC4C09EEFBC046609D67227FCA3F6E99) AS VARCHAR(25))  AS HHMemberFirstName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, CD0F3F67A61E4F6EBB63FC28B40B5F93) AS VARCHAR(25))  AS HHMemberMiddleName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, FEB11DE49964470F9B0D56F2A1EFFADF) AS VARCHAR(25))  AS HHMemberLastName
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A1432DFE090944B39C45E453A0ED82FF) AS VARCHAR(25))  AS TelNumber
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D7604923F0F14743AAF10E89ADB7B905) AS VARCHAR(25))  AS ConsentStatus
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, SearchTarget) AS VARCHAR(25))  AS SearchTarget
		,DateEntered  
		,EnteredBy
		--,DateModified
		--,ModifiedBy   
		,MachineName
 FROM DFDE4316996951AB2E6006E7
 ORDER BY CAST(DECRYPTBYPASSPHRASE(@PassPhrase, SessionID) AS VARCHAR(25)), RecordNo ASC
 ;
GO


--================================
--Decrypt MatchStatus table
--=================================
DECLARE @PassPhrase VARCHAR(64)
	SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';

SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase, B03075FD1514479690ED5FEDA8B629C6) AS VARCHAR(25))  RecordNo
--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A0492431ADB745FA949E9876393FEC7E) AS VARCHAR(25)) HealthFacilityName 
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, C4889A57F82B42B696DEE25F570F6496) AS VARCHAR(25))  AS FileRef
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, AAD6BA9F1CBB4861820A11ABB5C53A86) AS VARCHAR(25))  AS UniqueCTCIDNumber
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, FBBC6C2FAC4047D3AF06FD8519B47313) AS VARCHAR(25))  AS TGRFormNumber
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pCTCIDInfant) AS VARCHAR(25))  CTCIDInfant
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pUniqueHTCID) AS VARCHAR(25))  UniqueHTCID
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pUniqueANCID) AS VARCHAR(25))  UniqueANCID
--	    ,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pANCIDInfant) AS VARCHAR(25))  ANCIDInfant
--		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, pHEIDInfant) AS VARCHAR(25))  HEIDInfant
,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, DE6AF4558C2448FCA9C1474A7E51272F) AS VARCHAR(25))  AS DSSID
,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D548F01D94644D51A95D2727615D93F8) AS VARCHAR(200))  SearchCriteria
,Score
,ScoreRankGap
,ScoreRankNoGap
,ScoreRankIter
,DateMatched
--,MatchedBy
,MachineName 
FROM CB1EFC4E471242FC8C2FE20241FE2E3E
order by DateMatched
GO


--================================
--Decrypt Search for Match Notes Table
--=================================
DECLARE @PassPhrase VARCHAR(64)
	SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';

SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase, B03075FD1514479690ED5FEDA8B629C6) AS VARCHAR(25))  RecordNo
--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A0492431ADB745FA949E9876393FEC7E) AS VARCHAR(25)) HealthFacilityName 
--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, C4889A57F82B42B696DEE25F570F6496) AS VARCHAR(25))  FileRef
--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, AAD6BA9F1CBB4861820A11ABB5C53A86) AS VARCHAR(25))  UniqueCTCNumber
,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D548F01D94644D51A95D2727615D93F8) AS VARCHAR(200))  SearchCriteria
,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, D21589D11A6F428EBF1E0597BF6CB926) AS VARCHAR(MAX))  Search4MatchNotes
,DateSearched
--,SearchedBy 
,MachineName
FROM AAEE923ABF224A46A4DA8DC8D9CF8F05
ORDER BY DateSearched
GO

	/*
-------------------Script
	SELECT 	',CAST(DECRYPTBYPASSPHRASE(@PassPhrase, ' + COLUMN_NAME +') AS VARCHAR(25))'+ CHAR(13) + CHAR(10) 
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'AC817925DEAC405280C826E890A53CE6'
--------------------------------------------------------------
*/
	
-- Retrieve follow up visit date
DECLARE @PassPhrase VARCHAR(64)
SET @PassPhrase='242b9E089C9b30b892e7C6e45f2a35Ab2fBe480c36699E135A261D9bAd0316';	

SELECT CAST(DECRYPTBYPASSPHRASE(@PassPhrase, B03075FD1514479690ED5FEDA8B629C6) AS VARCHAR(25))  AS RecordNo
		--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, A0492431ADB745FA949E9876393FEC7E) AS VARCHAR(25))  AS HealthFacilityName
		--,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, C4889A57F82B42B696DEE25F570F6496) AS VARCHAR(25))  AS FileRef
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, BBEB00C8CA1A4DF0B969FDE821B23417) AS VARCHAR(25))  AS VisitDate
		,CAST(DECRYPTBYPASSPHRASE(@PassPhrase, E00919D3A5BB49D18083E8900F649A90) AS VARCHAR(25))  AS VisitBy
		, DateEntered  
		--,EnteredBy  
		--,DateModified  
		--,ModifiedBy
,MachineName
FROM   AC817925DEAC405280C826E890A53CE6
ORDER BY DateEntered
GO

--*/

-- NOTE: THE SAME COLUMN NAME IS GIVEN TO REGISTRATION DATE IN T1 AND VISIT DATE IN T4. 