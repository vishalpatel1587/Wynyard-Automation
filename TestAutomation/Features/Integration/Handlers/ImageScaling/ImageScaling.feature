@integration @handlerTest @ImageScalingHandler 
Feature: ImageScalingHandler
	As part of processing exhibits 
	image files are processed and thumbnails produced


Scenario: Scaling an image file
	Given I have an image file
	When I scale the file
	Then A thumbnail image is generated

Scenario: Scaling a non image file
	Given I have a non image file
	When I scale the file
	Then No thumbnail image is generated