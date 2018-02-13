@guiTest @loginAsAdmin
Feature: CheckMedia
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@checkMedia
Scenario: CheckAddMediaPage
	Given I am on the exhibit page
	When I click on Add media
	Then TimeZone should appear and user should be able to save it.

@AddandSaveMedia
Scenario: AddandSaveMedia
	Given I am on the exhibit page
	When I click on Add media and click save
	Then Media should be added.

