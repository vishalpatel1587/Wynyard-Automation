@guiTest @loginAsAdmin @AddExhibit
Feature: AddExhibit
	As a DEI user
	I want to be able to add a new exhibit
	So that I can process its media.

Scenario: Add a new Exhibit
	When I add a new Exhibit
	Then I can see it appears in Case Exhibits list
