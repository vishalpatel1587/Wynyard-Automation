 @guiTest @Login
Feature: Login
Description:
	As a valid DEI application user
	I want to be able to login successfully.
	Invalid login attempt should cause the validation error message.

@validLoginDetails
Scenario: Valid login details
	When I try to log in with valid details 
	Then the page displayed contains 'Cases' heading
	And the 'Logout' link is displayed

@invalidLoginDetails
Scenario: Invalid login details
When I try to log in with invalid details 
Then the error message 'Invalid User Name or Password.' is shown

@invalidPassword
Scenario: Invalid password
When I try to log in with invalid password 
Then the error message 'Invalid User Name or Password.' is shown

@invalidUsername
Scenario: Invalid username
When I try to log in with invalid username
Then the error message 'Invalid User Name or Password.' is shown
