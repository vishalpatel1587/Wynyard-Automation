 @guiTest @loginAsAdmin @CheckHomePageContent
Feature: CheckHomePageContent
Description:
	The Home page of DEI shoud contain the following text.

*Background:I have logged in as an admin user.

@CheckPageContainsText
Scenario Outline: Check Home page contains text
	When I look at the Home page
	Then I should be able to see '<expectedText>' text

Examples:
| expectedText    |
| Cases           |

@CheckPageContainsLinks
Scenario Outline: Check Home page contains Links
	When I look at the Home page
	Then I should be able to see '<expectedText>' Links
Examples:
| expectedText    |
| Recent Activity |
| System Messages |


@CheckRecentActivityPage
Scenario: Check the Recent Activity Page
	When I go to the 'Recent Activity' Page
	Then the 'Recent Activity' page should contain 'Action' and 'Description'

@CheckSystemMessagePage
Scenario: Check the System Messages Page
	When I go to the 'System Messages' Page
	Then the 'System Messages' page should contain 'Date' and 'Subject'