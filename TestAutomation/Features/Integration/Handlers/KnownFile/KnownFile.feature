 @integration @handlerTest @KnownFileHandler 
Feature: KnownFileHandler
	As part of processing exhibits 
	files are processed and known files are identified


Scenario: Process unknown file with existing hash
	Given I have a file
	And It has an existing hash
	When I process the file
	Then The file is not detected as known.
	
Scenario: Process unknown file
	Given I have a file
	When I process the file
	Then The file is not detected as known.

Scenario: Process known file
	Given I have a file	
	And This file is known
	When I process the file
	Then The file is detected as known.

Scenario: Process known file with a hash
	Given I have a file	
	And It has an existing hash
	And This file is known
	When I process the file
	Then The file is detected as known.