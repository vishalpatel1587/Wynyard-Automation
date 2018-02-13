@smokeTest @UfedLogicalProcessingExhibitDbTablesCheck
Feature: Ufed Logical Processing ExhibitDb Tables Check
	
	The Ufed smoke test checks that Entity, ConnectionEvent and all Metadata tables are populated correctly after the UFED exhibit processing.
	After the Exhibit is processed the processing time measured from Work table is being recorded to ExhibitProcessingTime.csv file.
	The details of all files under FileMetadata table are checked to be the same as the expected data from the csv file.
	
	The test config file and the expected data files are located under Steps\EndToEndTest\TestData\UfedSmokeTestExhibit. 

@ProcessExhibit
Scenario:An Exhibit is processed
	When I process an Exhibit
	Then the processing time has been recorded

@CheckNumberOfFilesInEntityTable
Scenario: Check number of files in Entity table
	When I check 'Entity' table
	Then the number of processed files is correct in 'Entity' table

@CheckNumberOfCertainEntityNameEntriesInEntityTable
Scenario: Check number of certain EntityName entries in Entity table
	When I check 'Entity' table
	Then the number of expected EntityName entries is correct in 'Entity' table

@CheckNumberOfFilesInConnectionEventTable
Scenario: Check number of files in ConnectionEvent table
	When I check 'ConnectionEvent' table
	Then the number of processed files is correct in 'ConnectionEvent' table

@CheckNumberOfCertainConnectionTypeIdEntriesInConnectionEventTable
Scenario: Check number of certain ConnectionTypeId entries in ConnectionEvent table
	When I check 'ConnectionEvent' table
	Then the number of expected ConnectionTypeId entries is correct in 'ConnectionEvent' table

 @CheckNumberOfFilesInFileMetadataTable
Scenario: Check number of files in FileMetadata table
	When I check 'FileMetadata' table
	Then the number of processed files is correct in 'FileMetadata' table

 @CheckNumberOfFilesInImageMetadataTable
Scenario: Check number of files in ImageMetadata table
	When I check 'ImageMetadata' table
	Then the number of processed files is correct in 'ImageMetadata' table

@CheckNumberOfFilesInPdfMetadataTable
Scenario: Check number of files in PdfMetadata table
	When I check 'PdfMetadata' table
	Then the number of processed files is correct in 'PdfMetadata' table

@CheckNumberOfFilesInVideoMetadataTable
Scenario: Check number of files in VideoMetadata table
	When I check 'VideoMetadata' table
	Then the number of processed files is correct in 'VideoMetadata' table

@CheckNumberOfFilesInInternetMetadataTable
Scenario: Check number of files in InternetMetadata table
	When I check 'InternetMetadata' table
	Then the number of processed files is correct in 'InternetMetadata' table

@CheckNumberOfFilesInPartitionMetadataTable
Scenario: Check number of files in PartitionMetadata table
	When I check 'PartitionMetadata' table
	Then the number of processed files is correct in 'PartitionMetadata' table

@CheckProcessedFileDetails
Scenario: CheckProcessedFileDetails
	When the csv file with the expected Media file details data exists
	Then the processed file details are correct in FileMetadata table as per the csv file
