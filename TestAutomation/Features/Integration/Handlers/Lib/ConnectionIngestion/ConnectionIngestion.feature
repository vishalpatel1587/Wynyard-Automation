@integration @ConnectionIngestionProcessor 
Feature: ConnectionIngestionProcessor
	As part of processing exhibits 
	files are processed and known files are identified


Scenario: Process UFED item
	Given I have a UFED item
	When I process the item
	Then The necessary work items are created