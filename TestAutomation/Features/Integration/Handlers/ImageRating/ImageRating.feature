@ignore @integration @handlerTest @ImageRatingHandler 
Feature: ImageRatingHandler
	As part of processing exhibits 
	image files are rated to determine their objectional material content


Scenario: Rating an image file of non interest
	Given I have an image file of non interest
	When I rate the file
	Then An image rating is generated
	And Image is not identified as of interest

Scenario: Rating an image file of interest
	Given I have an image file of interest
	When I rate the file
	Then An image rating is generated
	And Image is identified as of interest