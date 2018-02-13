@guiTest @loginAsAdmin @AddCase
Feature: AddCase
Description:
	As a DEI user
	I want to be able to add a new case

*Background: I have logged in as an admin user.

@AddNewCase
Scenario: Add new case
	Given I am on Cases page
	When I add a new case
	Then I can see it appears in Cases list
