USE [KisesaDSSClinicLinkSystem]
GO

/****** Object:  Table [dbo].[Villages]    Script Date: 05/05/2015 15:43:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Villages]') AND type in (N'U'))
DROP TABLE [dbo].[Villages]
GO

/****** Object:  Table [dbo].[Villages]    Script Date: 05/05/2015 15:43:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Villages]') AND type in (N'U'))
BEGIN
create TABLE [dbo].[Villages](
	[VillageName] [nvarchar](100) NULL,
	[SubVillage] [nvarchar](100) NULL
) ON [PRIMARY]
END
GO
grant select on dbo.villages to Admin


insert dbo.Villages

VALUES(
	'Igekemaja',		'Igekemaja'),(
	'Igekemaja',		'Ilagaja'),(
	'Igekemaja',		'Igunga'),(
	'Igekemaja',		'Ihala'),(
	'Igekemaja',		'Nyang''hulukulu'),(
	'Kitumba',		'Igudija A'),(
	'Kitumba',		'Igudija B'),(
	'Kitumba',		'Kitumba A'),(
	'Kitumba',		'Kitumba B'),(
	'Kitumba',		'Igadya'),(
	'Kitumba',		'Kigungumuli'),(
	'Kitumba',		'Kisha'),(
	'Kitumba',		'Mondo'),(
	'Kitumba',		'Kimaga'),(
	'Kanyama',		'Iseni Mlimani'),(
	'Kanyama',		'Iseni Bondeni'),(
	'Kanyama',		'Bukelebe'),(
	'Kanyama',		'Mwabuki'),(
	'Kanyama',		'Kanyama'),(
	'Kanyama',		'Igeye'),(
	'Kanyama',		'Changabe'),(
	'Kisesa',		'Kisesa Kati'),(
	'Kisesa',		'Lumve'),(
	'Kisesa',		'Wita'),(
	'Kisesa',		'Ngwanghalanga'),(
	'Kisesa',		'Ngwandulu'),(
	'Isangijo',		'Mahilinga'),(
	'Isangijo',		'Ng''wabongoso'),(
	'Isangijo',		'Kanami'),(
	'Isangijo',		'Mwamanyili'),(
	'Isangijo',		'Bukala'),(
	'Isangijo',		'Ng''wahuli'),(
	'Ihayabuyaga',		'Ihayabuyaga'),(
	'Ihayabuyaga',		'Bukandwe'),(
	'Ihayabuyaga',		'Njicha'),(
	'Ihayabuyaga',		'Igumo'),(
	'Ihayabuyaga',		'Ilendeja'),(
	'Welamasonga',		'Welamasonga'),(
	'Welamasonga',		'Nkola'),(
	'Welamasonga',		'Ilangale'),(
	'Welamasonga',		'Ikangabuta'),(
	'Welamasonga',		'Nyamikoma'),(
	'Welamasonga',		'Nyanhelela'),(
	'Welamasonga',		'Ikengele'),(
	'Welamasonga',		'Ikulicha'),(
	'Welamasonga',		'Mwadubi'),(
	'Welamasonga',		'Mwaneneka')

USE [KisesaDSSClinicLinkSystem]
GO

IF  EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'v_Villages', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Villages'

GO

IF  EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'v_Villages', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Villages'

GO

/****** Object:  View [dbo].[v_Villages]    Script Date: 05/05/2015 16:16:25 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_Villages]'))
DROP VIEW [dbo].[v_Villages]
GO

/****** Object:  View [dbo].[v_Villages]    Script Date: 05/05/2015 16:16:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_Villages]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[v_Villages]
AS
select ''(none)'' as VillageName 
UNION
SELECT DISTINCT VillageName
FROM     dbo.Villages
' 
GO


grant select on dbo.v_Villages to Admin

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_SubVillages]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[v_SubVillages]
AS
SELECT SubVillage,VillageName,replace(SubVillage,''?'','''') as  VisibleName 
FROM     dbo.Villages
' 
GO


grant select on dbo.v_SubVillages to Admin

GO
create proc dbo.spGetSubVillages @Village nvarchar(100)
as
if exists (select 1 from dbo.v_Subvillages where VillageName = @Village) 
select SubVillage,VisibleName from dbo.v_SubVillages where VillageName = @Village
else
select '(none)' as VisibleName,'(none)' as SubVillage
go
grant execute on dbo.spGetSubVillages to Admin
go









USE [KisesaDSSClinicLinkSystem]
GO
create proc dbo.ExportDataForSync
as
declare @CMD varchar(1000)
declare @MachineName varchar(255) = Host_Name()
declare @CurrentDate varchar(10) = FORMAT(GETDATE(), 'yyyyMMdd')

set @CMD='bcp "SELECT * FROM [KisesaDSSClinicLinkSystem].dbo.[AAEE923ABF224A46A4DA8DC8D9CF8F05] where MachineName = ''' + @MachineName +  '''" queryout "C:\LinkSystemExport\' + @CurrentDate + '_' + @MachineName + '_AAEE923ABF224A46A4DA8DC8D9CF8F05.txt" -c -S .\KISCLINICDB -U Admin -P @KisesaCLDM'
EXEC xp_cmdshell @CMD
set @CMD='bcp "SELECT * FROM [KisesaDSSClinicLinkSystem].dbo.[AC817925DEAC405280C826E890A53CE6] where MachineName = ''' + @MachineName +  '''" queryout "C:\LinkSystemExport\' + @CurrentDate + '_' + @MachineName + '_AC817925DEAC405280C826E890A53CE6.txt" -c -S .\KISCLINICDB -U Admin -P @KisesaCLDM'
EXEC xp_cmdshell @CMD
set @CMD='bcp "SELECT * FROM [KisesaDSSClinicLinkSystem].dbo.[CB1EFC4E471242FC8C2FE20241FE2E3E] where MachineName = ''' + @MachineName +  '''" queryout "C:\LinkSystemExport\' + @CurrentDate + '_' + @MachineName + '_CB1EFC4E471242FC8C2FE20241FE2E3E.txt" -c -S .\KISCLINICDB -U Admin -P @KisesaCLDM'
EXEC xp_cmdshell @CMD
set @CMD='bcp "SELECT * FROM [KisesaDSSClinicLinkSystem].dbo.[DFDE4316996951AB2E6006E7] where MachineName = ''' + @MachineName +  '''" queryout "C:\LinkSystemExport\' + @CurrentDate + '_' + @MachineName + '_DFDE4316996951AB2E6006E7.txt" -c -S .\KISCLINICDB -U Admin -P @KisesaCLDM'
EXEC xp_cmdshell @CMD
GO








USE [KisesaDSSClinicLinkSystem]
GO
/****** Object:  StoredProcedure [dbo].[ImportDataForSync]    Script Date: 08/05/2015 21:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportDataForSync]
as
--SET NOCOUNT ON;

DECLARE
    @folder NVARCHAR(2048),
    @cmd VARCHAR(2048),
	@filename NVARCHAR(2048);

SET @folder = N'C:\LinkSystemImport\';

SET @cmd = N'dir /b ' + @folder + '*.txt';

IF Object_ID('tempdb..#x') IS NOT NULL
	BEGIN 
		DROP TABLE #x;
	END;

CREATE TABLE #x(FileName NVARCHAR(2048));

INSERT #x EXEC [master].dbo.xp_cmdshell @cmd;

--SET DATEFORMAT dmy;

--;WITH x(n) AS (SELECT n FROM #x WHERE ISDATE(LEFT(n, 20)) = 1)
--    SELECT @folder + SUBSTRING(n,
--        LEN(n) - CHARINDEX(' ', REVERSE(n)) + 2,
--        2048) as FileName into #y FROM x
--    ORDER BY CONVERT(DATETIME, LEFT(n, 20)) DESC;

--SET DATEFORMAT mdy;

DECLARE file_cursor CURSOR FOR 
    SELECT FileName
    FROM #x
    WHERE FileName not like @folder + Host_Name() + '%' AND FileName IS NOT NULL

	OPEN file_cursor
    FETCH NEXT FROM file_cursor INTO @filename

    WHILE @@FETCH_STATUS = 0
    BEGIN

		if @filename like '%AAEE923ABF224A46A4DA8DC8D9CF8F05%'
		      BEGIN
			  create table ##stagingAAEE923ABF224A46A4DA8DC8D9CF8F05
					(
					B03075FD1514479690ED5FEDA8B629C6 VARBINARY(50) -- RecordNo\PatientId
					,A0492431ADB745FA949E9876393FEC7E VARBINARY(50) --HealthFacilityName
					,C4889A57F82B42B696DEE25F570F6496 varbinary(50)--FileRef
					,AAD6BA9F1CBB4861820A11ABB5C53A86 varbinary(50) -- UniqueCTC
					,FBBC6C2FAC4047D3AF06FD8519B47313 VARBINARY(50) --TGRFormNumber
					,pCTCIDInfant VARBINARY(50) --CTCInfantID
					,pUniqueHTCID VARBINARY(50) --UniqueHTCID
					,pUniqueANCID VARBINARY(50) --UniqueANCID
					,pANCIDInfant VARBINARY(50) --ANCInfantID
					,pHEIDInfant VARBINARY(50) --HEIDInfantID
					,D548F01D94644D51A95D2727615D93F8 varbinary(200) --SearchCriteria
					,D21589D11A6F428EBF1E0597BF6CB926 VARBINARY(500) --Search for Match Notes
					,DateSearched datetime 
					,SearchedBy VARCHAR(128)
					,MachineName varchar(125) 
					)		
					
					declare @ImportCMD1 nvarchar(1000)
					declare @MachineName1 varchar(100) 
					SET @MachineName1=''
					set @ImportCMD1=N'bcp ##stagingAAEE923ABF224A46A4DA8DC8D9CF8F05 in ' + @folder + @filename + N' -S .\KISCLINICDB -U Admin -P @KisesaCLDM -c'
					EXEC master..xp_cmdshell @ImportCMD1
					select  top 1 @MachineName1=MachineName from ##stagingAAEE923ABF224A46A4DA8DC8D9CF8F05
					delete from AAEE923ABF224A46A4DA8DC8D9CF8F05 where MachineName=@MachineName1
					insert AAEE923ABF224A46A4DA8DC8D9CF8F05 select * from ##stagingAAEE923ABF224A46A4DA8DC8D9CF8F05
					drop table ##stagingAAEE923ABF224A46A4DA8DC8D9CF8F05
			  END
			  if @filename like '%AC817925DEAC405280C826E890A53CE6%'
		      BEGIN
			  create  table ##stagingAC817925DEAC405280C826E890A53CE6(
						B03075FD1514479690ED5FEDA8B629C6 VARBINARY(50) -- RecordNo
						,A0492431ADB745FA949E9876393FEC7E VARBINARY(50) --HealthFacilityName
						,C4889A57F82B42B696DEE25F570F6496 VARBINARY(50) --FileRef
						,AAD6BA9F1CBB4861820A11ABB5C53A86 VARBINARY(50) --UniqueCTCIDNumber
						,FBBC6C2FAC4047D3AF06FD8519B47313 VARBINARY(50) --TGRFormNumber
						,pCTCIDInfant VARBINARY(50) --CTCInfantID
						,pUniqueHTCID VARBINARY(50) --UniqueHTCID
						,pUniqueANCID VARBINARY(50) --UniqueANCID
						,pANCIDInfant VARBINARY(50) --ANCInfantID
						,pHEIDInfant VARBINARY(50) --HEIDInfantID
						,BBEB00C8CA1A4DF0B969FDE821B23417 VARBINARY(50) --VisitDate
						,E00919D3A5BB49D18083E8900F649A90 VARBINARY(50) --VisitBy
						,DateEntered datetime 
						,EnteredBy  VARCHAR(128)
						,MachineName varchar(125) 

					)
					declare @ImportCMD2 nvarchar(1000)
					declare @MachineName2 varchar(100) 
					SET @MachineName2=''
					set @ImportCMD2=N'bcp ##stagingAC817925DEAC405280C826E890A53CE6 in ' + @folder + @filename + N' -S .\KISCLINICDB -U Admin -P @KisesaCLDM -c'
					EXEC master..xp_cmdshell @ImportCMD2
					select  top 1 @MachineName2=MachineName from ##stagingAC817925DEAC405280C826E890A53CE6
					delete from AC817925DEAC405280C826E890A53CE6 where MachineName=@MachineName2
					insert AC817925DEAC405280C826E890A53CE6 select * from ##stagingAC817925DEAC405280C826E890A53CE6
					drop table ##stagingAC817925DEAC405280C826E890A53CE6
			  END
			  if @filename like '%CB1EFC4E471242FC8C2FE20241FE2E3E%'
		      BEGIN
			  create table ##stagingCB1EFC4E471242FC8C2FE20241FE2E3E(
						 B03075FD1514479690ED5FEDA8B629C6 varbinary(50)--RecordNo
						,A0492431ADB745FA949E9876393FEC7E varbinary(50)--HealthFacilityName
						,C4889A57F82B42B696DEE25F570F6496 varbinary(50)--FileREf
						,AAD6BA9F1CBB4861820A11ABB5C53A86 varbinary(50) -- UniqueCTC
						,FBBC6C2FAC4047D3AF06FD8519B47313 VARBINARY(50) --TGRFormNumber
						,pCTCIDInfant VARBINARY(50) --CTCInfantID
						,pUniqueHTCID VARBINARY(50) --UniqueHTCID
						,pUniqueANCID VARBINARY(50) --UniqueANCID
						,pANCIDInfant VARBINARY(50) --ANCInfantID
						,pHEIDInfant VARBINARY(50) --HEIDInfantID
						,DE6AF4558C2448FCA9C1474A7E51272F varbinary(50) -- DSS_IDLong
						,D548F01D94644D51A95D2727615D93F8 varbinary(200) --SearchCriteria
						,Score float
						,ScoreRankGap int
						,ScoreRankNoGap int
						,ScoreRankIter int
						,DateMatched datetime 
						,MatchedBy VARCHAR(128) 
						,MachineName varchar(125) 
						)
					declare @ImportCMD3 nvarchar(1000)
					declare @MachineName3 varchar(100) 
					SET @MachineName3=''
					set @ImportCMD3=N'bcp ##stagingCB1EFC4E471242FC8C2FE20241FE2E3E in ' + @folder + @filename + N' -S .\KISCLINICDB -U Admin -P @KisesaCLDM -c'
					EXEC master..xp_cmdshell @ImportCMD3
					select  top 1 @MachineName3=MachineName from ##stagingCB1EFC4E471242FC8C2FE20241FE2E3E
					delete from CB1EFC4E471242FC8C2FE20241FE2E3E where MachineName=@MachineName3
					insert CB1EFC4E471242FC8C2FE20241FE2E3E select * from ##stagingCB1EFC4E471242FC8C2FE20241FE2E3E
					drop table ##stagingCB1EFC4E471242FC8C2FE20241FE2E3E
			  END
			  if @filename like '%DFDE4316996951AB2E6006E7%'
		      BEGIN
			  print @filename
			  create table ##stagingDFDE4316996951AB2E6006E7(
					B03075FD1514479690ED5FEDA8B629C6 VARBINARY(50) -- PatientId
					,A0492431ADB745FA949E9876393FEC7E VARBINARY(50) -- HealthFacilityName
					,F89DD26654256B5E527230D174365 VARBINARY(50) --HealthFacilityDepartment
					,BBEB00C8CA1A4DF0B969FDE821B23417 VARBINARY(50) --RegistrationDate
					,C4889A57F82B42B696DEE25F570F6496 VARBINARY(50) --FileRef
					,AAD6BA9F1CBB4861820A11ABB5C53A86 VARBINARY(50) --UniqueCTCIDNumber
					,FBBC6C2FAC4047D3AF06FD8519B47313 VARBINARY(50) --TGRFormNumber
					,A6D47617430D8737E2A9C7CA176F VARBINARY(50) --FirstName
					,E73CCEDA4CF7445EB5FF71B84282AE4B VARBINARY(50) --MiddleName
					,DD50A64B30564134AA523C76DCC2CD94 VARBINARY(50) --LastName
					,A014C368F5E6469BBA04F2CDF502334E VARBINARY(50) --Sex
					,AB44DE55ABC64282A1FBC8E07DCA7E4B VARBINARY(50) --YearofBirth
					,E1EE5FBA27EE4E7291B964D7763CA3B1 VARBINARY(50) --MonthofBirth
					,E5DEBDB3E4844A22AF25165B687997C2 VARBINARY(50) --DayofBirth
					,CA28218807DE4870AF26F4C6274F2FBA VARBINARY(50) --Village
					,EC87DE4886EC4F23A270A5C0225CA63C VARBINARY(50) --SubVillage
					,D86C72DE6CE14B549CE4E9F93938576E VARBINARY(50) --YearAtCurResident
					,EDB1F900027F428EB35A4070203F50B8 VARBINARY(50) --MonthAtCurResident
					,F5E81AB3ACC3492395F1BAA7A657DAD2 VARBINARY(50) --DayAtCurResident
					,DD517CF9CC68408EA0396CB56BED6CA7 VARBINARY(50) --TCLFirstName
					,CDCDD6B93EE14D53BCD0BA047E6DB8ED VARBINARY(50) --TCLMiddleName
					,E62B5829956A4C498B359E16647DC1AC VARBINARY(50) --TCLLastName
					,DC4C09EEFBC046609D67227FCA3F6E99 VARBINARY(50) --HHMemberFirstName
					,CD0F3F67A61E4F6EBB63FC28B40B5F93 VARBINARY(50) --HHMemberMiddleName
					,FEB11DE49964470F9B0D56F2A1EFFADF VARBINARY(50) --HHMemberLastName
					,A1432DFE090944B39C45E453A0ED82FF VARBINARY(50) --TelNumber
					,D7604923F0F14743AAF10E89ADB7B905 VARBINARY(50) --ConsentStatus
					,DateEntered datetime 
					,EnteredBy VARCHAR(128)
					,DateModified datetime 
					,ModifiedBy VARCHAR(128)
					,SessionID VARBINARY(50)
					,SearchTarget VARBINARY(50)
					,pCTCIDInfant VARBINARY(50)
					,pUniqueHTCID VARBINARY(50)
					,pUniqueANCID VARBINARY(50)
					,pANCIDInfant VARBINARY(50)
					,pHEIDInfant VARBINARY(50)
					,MachineName varchar(125) 

					)
					declare @ImportCMD4 nvarchar(1000)
					declare @MachineName4 varchar(100) 
					SET @MachineName4=''
					set @ImportCMD4=N'bcp ##stagingDFDE4316996951AB2E6006E7 in ' + @folder + @filename + N' -S .\KISCLINICDB -U Admin -P @KisesaCLDM -c'
					EXEC master..xp_cmdshell @ImportCMD4
					select  top 1 @MachineName4=MachineName from ##stagingDFDE4316996951AB2E6006E7
					delete from DFDE4316996951AB2E6006E7 where MachineName=@MachineName4
					insert DFDE4316996951AB2E6006E7 select * from ##stagingDFDE4316996951AB2E6006E7
					drop table ##stagingDFDE4316996951AB2E6006E7
			  END


        FETCH NEXT FROM file_cursor INTO @filename
        END

    CLOSE file_cursor
    DEALLOCATE file_cursor


SELECT * FROM #x
DROP TABLE #x
--,#y