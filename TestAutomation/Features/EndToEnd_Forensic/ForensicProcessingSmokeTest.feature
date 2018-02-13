@smokeTest @ForensicProcessingSmokeTest
Feature: ForensicProcessingSmokeTest

The Forensic smoke test checks that the number of files is correct in each of the Metadata Tables in this Forensic Exhibit database.
After the Exhibit is processed the processing time measured from Work table is being recorded to ExhibitProcessingTime.csv file.
The details of all files under FileMetadata table are checked to be the same as the expected data from the csv file.

@ProcessExhibit
Scenario:An Exhibit is processed
	When I process an Exhibit
	Then the processing time has been recorded

@CheckNumberOfFilesInFileMetadataTable
Scenario: Check number of files in FileMetadata table
	When I check 'FileMetadata' table
	Then the number of processed files is correct in 'FileMetadata' table

 @CheckNumberOfFilesInImageMetadataTable
Scenario: Check number of files in ImageMetadata table
	When I check 'ImageMetadata' table
	Then the number of processed files is correct in 'ImageMetadata' table

@CheckPornProbabilityOfFilesInImageMetadataTable
Scenario: Check porn probability of files in ImageMetadata table
	When I check 'ImageMetadata' table
	Then the porn probability values of all processed files are not NULL and not -1

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
	