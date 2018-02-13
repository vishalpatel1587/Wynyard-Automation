@guiTest @loginAsAdmin
Feature: Search
	AS a DEI user
	I want to search for contact names
	
*Background: I have logged in as an admin user.


@Search
Scenario Outline: A Search on a Contact Name
	Given an exhibit of a version <version>
	And I process that Exhibit
	When I search for a '<Contact Name>' in 'Mobile Content'
	Then I can see that '<Contact Name>' or 'Name' appears in Mobile Content search results


	Examples:
	| version | Contact Name | 
	| 3.9     |  addie       |


@SMSSearch
Scenario Outline:Search SMS
	When I search for a '<Sms Message>' in 'Mobile Content'
	Then I can see that '<Sms Message>' or 'Message' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Date Start' are valid in the search result 

	When i click on the search result
	Then the viewer should open
	And the '<Time>', '<Recipient>' and '<Sms Message>' should be valid.

	
	Examples:
	| Date Start                | Sms Message | Time                        | Recipient               |
	| 22 Jun 2013 05:41:10 a.m. | Bal         | June 22, 2013 05:41:10 a.m. | 777 (Customer Service ) |


@ContactSearchInFIleMetadata
Scenario Outline: : Search Contact name in File Metadata plugin
	When I search for a '<Contact Name>' in 'File Metadata'
	Then I can see that '<Contact Name>' it appears in File Metadata search results
	
	Examples: 
	| Contact Name |
	| addie        |

@BookmarkFilemetadata
Scenario: Bookmark a content on Filemetadata plugin
When I try to bookmark the content on 'Mobile Content' plugin
Then the content should get bookmarked.


Scenario Outline: Search Image in Image plugin
	When I search for a '<Image Name>' in 'Images plugin'
	Then I can see that '<Image Name>' it appears in Images search results

	When i click on the image or video search result
	Then the viewer should open
	And the '<Created>','<Last Accessed>' and '<Last Modified>' are valid for images and videos.
	And the Image is visible to user.

	Examples:
	| Image Name       | Created                      | Last Accessed                | Last Modified                |
	| Pic_0305_013.jpg | March 05, 2012 05:56:15 a.m. | March 09, 2015 11:00:00 a.m. | March 10, 2015 07:39:24 a.m. |


Scenario Outline: Search Outgoing Calls
	When I search for a '<Callee>' in 'Mobile Content'
	Then I can see that '<Callee>' or 'To' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Time>','<Duration>', '<Callee>' and '<Direction>' are valid.

	Examples: 
	| Direction | Callee     | Date Start                | Date Stop                 | Duration | Time                        |
	| Outgoing  | 0800438448 | 09 Jul 2013 08:29:22 a.m. | 09 Jul 2013 08:35:30 a.m. | 368      | July 09, 2013 08:29:22 a.m. |


Scenario Outline: Search Incoming Calls
	When I search for a '<Caller>' in 'Mobile Content'
	Then I can see that '<Caller>' or 'From' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Time>','<Duration>', '<Caller>' and '<Direction>' are valid.

	Examples: 
	| Direction | Caller     | Date Start                | Date Stop                 | Duration | Time                        |
	| Incoming  | 0800800021 | 09 Jul 2013 05:36:44 a.m. | 09 Jul 2013 05:53:33 a.m. | 1009     | July 09, 2013 05:36:44 a.m. |


Scenario Outline: Search Missed Calls
	When I search for a '<Caller>' in 'Mobile Content'
	Then I can see that '<Caller>' or 'From' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Time>','<Duration>', '<Caller>' and '<Direction>' are valid.

	Examples: 
	| Direction | Caller    | Date Start                | Date Stop                 | Duration | Time                        |
	| Missed    | 093552000 | 09 Jul 2013 11:13:46 p.m. | 09 Jul 2013 11:13:46 p.m. | 0        | July 09, 2013 11:13:46 p.m. |


Scenario Outline: Search Calendar
	When I search for a '<Calendar Detail>' in 'Mobile Content'
	Then I can see that '<Calendar Detail>' or 'Message' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Start Date>','<Stop Date>', '<Location>', '<Calendar Detail>' and '<Subject>' are valid for calendar.

	Examples: 
	| Calendar Detail | Subject   | Date Start                | Date Stop                 | Location | Start Date                      | Stop Date                       |
	| carpe           | Christmas | 24 Dec 2012 07:00:00 a.m. | 24 Dec 2012 03:00:00 p.m. | Paris    | December 24, 2012 07:00:00 a.m. | December 24, 2012 03:00:00 p.m. |
	

Scenario Outline: Search MMS
	When I search for a '<MMS>' in 'Mobile Content'
	Then I can see that '<MMS>' or 'Message' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<From>' 'From' are valid in the search result
	And the  dates '<To>' 'To' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Start Date>','<Subject>', '<MMS>', '<To>', '<Attachment>' and '<From>' are valid for MMS.
	And the user should be able to open the '<Attachment>'.

	Examples: 
	| MMS | Subject             | Date Start                | To         | From         | Start Date                      | Attachment      |
	| Xxx | This is the subject | 25 Dec 2012 12:05:37 a.m. | 0278707219 | +64272493699 | December 25, 2012 12:05:37 a.m. | imagejpeg_2.jpg |


Scenario Outline: Search Video in Video plugin
	When I search for a '<Video Name>' in 'Video plugin'
	Then I can see that '<Video Name>' it appears in Video search results

	When i click on the image or video search result
	Then the viewer should open
	And the '<Created>','<Last Accessed>' and '<Last Modified>' are valid for images and videos.
	And the Video is visible to user.

	Examples:
	| Video Name       | Created                        | Last Accessed               | Last Modified                |
	| Mov_0106_009.3gp | January 05, 1980 02:28:10 p.m. | March 09, 2015 11:00:00 a.m | March 10, 2015 07:39:24 a.m. |


#Scenario: Check that the plugin stats are visible for Images, Videos and Mobile content Plugins
#	When i go to the search Page
#	Then the Image, Video and Mobile Content has Plugin stats
#	And The plugin stats are valid.