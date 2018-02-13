@guiTest @regression @loginAsAdmin @addDBExhibit @SaveCustomWorkflow
Feature: SaveCustomWorkflow
	As a DEI user
	I want to be able to save my Custom Workflow
	So that I can process only certain type of files

Scenario: Save Custom Workflow
	Given I have saved my Custom Workflow with no Images and Deleted Files included
	When I select that Workflow to process my exhibit
	Then I can see that Images and Deleted Files are not selected

