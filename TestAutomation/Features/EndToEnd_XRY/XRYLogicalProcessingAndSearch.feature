@smokeTest @guiTest @loginAsAdmin
Feature: Xry Logical Processing And Search
	 This test checks that after processing XRY logical extracts the Processing Statistics and the Exhibit Properties are populated correctly.

@processXRY
Scenario Outline: Process an XRY exhibit
	Given an exhibit of a version <version>
	When I process that Exhibit

	Examples:
	| version |
	| Logical |

@SearchXRYContactName
Scenario Outline: Search Contact Name
	When I search for a '<Contact Name>' in 'Mobile Content'
	Then I can see that '<Contact Name>' or 'Name' appears in Mobile Content search results

	Examples:
	| Contact Name | 
	| Morgen       |

@CalenderSearchXRY
Scenario Outline: Search Calendar
	When I search for a '<Calendar Detail>' in 'Mobile Content'
	Then I can see that '<Calendar Detail>' or 'Message' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Start Date>','<Stop Date>', '<Location>', '<Calendar Detail>' and '<Subject>' are valid for calendar.

	Examples: 
	| Calendar Detail | Subject     | Date Start                | Date Stop                 | Location      | Start Date                 | Stop Date                  |
	| Waxing 4.30	  | Waxing 4.30 | 28 May 2012 12:00:00 a.m. | 28 May 2012 01:00:00 a.m. | Wairakei road | May 28, 2012 12:00:00 a.m. | May 28, 2012 01:00:00 a.m. |
	
@OutgoingCallsSearchXRY	
Scenario Outline: Search Outgoing Calls
	When I search for a '<Callee>' in 'Mobile Content'
	Then I can see that '<Callee>' or 'To' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Time>','<Duration>', '<Callee>' and '<Direction>' are valid.

	Examples: 
	| Direction | Callee       | Date Start                | Date Stop                 | Duration | Time                           |
	| Dialled   | +64221863220 | 14 Oct 2012 02:58:47 p.m. | 14 Oct 2012 02:59:29 p.m. | 42       | October 14, 2012 02:58:47 p.m. |

@IncomingCallsSearchXRY
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
	| Received  | 0212444061 | 02 Jun 2012 03:53:13 a.m. | 02 Jun 2012 03:53:45 a.m. | 32       | June 02, 2012 03:53:13 a.m. |

@MissedCallsSearchXRY
Scenario Outline: Search Missed Calls
	When I search for a '<Caller>' in 'Mobile Content'
	Then I can see that '<Caller>' or 'From' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And the  dates '<Date Stop>' 'Stop Date' are valid in the search result

	When i click on the search result
	Then the viewer should open
	And the '<Time>','<Duration>', '<Caller>' and '<Direction>' are valid.

	Examples: 
	| Direction | Caller     | Date Start                | Date Stop                 | Duration | Time                           |
	| Missed    | 0220878224 | 18 Oct 2012 11:39:53 p.m. | 18 Oct 2012 11:39:53 p.m. | 0        | October 18, 2012 11:39:53 p.m. |


@SMSSearchXRY
Scenario Outline:Search SMS
	When I search for a '<Sms Message>' in 'Mobile Content'
	Then I can see that '<Sms Message>' or 'Message' appears in Mobile Content search results
	And the  dates '<Start Date>' 'Start Date' are valid in the search result 

	When i click on the search result
	Then the viewer should open
	And the '<Time>', '<Recipient>' and '<Sms Message>' should be present in viewer text.
	
	Examples:
	| Start Date				  | Sms Message | Time                           | Recipient  |
	| 01 Jan 2013 05:23:02 p.m.	  | caravan	    | January 01, 2013 05:23:02 p.m. | 0276018882 |


@MMSSearchXRY
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
	| MMS         | Subject | Date Start                | To          | From         | Start Date                      | Attachment           |
	| 64212252994 |         | 03 Dec 2013 09:56:13 a.m. | 64211618519 | +64212252994 | December 03, 2013 09:56:13 a.m. | 20131204_105022.jpeg |

@ContactSearchInFIleMetadataXRY
Scenario Outline: : Search Contact name in File Metadata plugin
	When I search for a '<Contact Name>' in 'File Metadata'
	Then I can see that '<Contact Name>' appears in File Metadata search results
	
	Examples: 
	| Contact Name |
	| Morgen       |

@emailSearchInMobileContent
Scenario Outline: : Search Emails in MobileContent
	When I search for a '<Subject>' in 'Mobile Content'
	Then I can see that '<Subject >' or 'Subject' appears in Mobile Content search results
	
	Examples: 
	| From Email Address			| To Email Address				 | Status | Received Date             | Subject                    | Text Body              |
	| newsletter@email.lumosity.com | ellie_lp@hotmail.co.nz | Sent	  | 08 May 2012 10:03:35 a.m. | Click, Click, Brain Health | brain training program |

@EmailSearchContent
Scenario Outline: : Search Emails in Content
	When I search for a '<Subject>' in 'Content Search'
	Then I can see that '<From Email Address>' appears in Content search results

	When i click on the search result
	Then the viewer should open
	And the '<Recieved Date>', '<Subject>' and '<Text Body>' should be present in viewer text.
	
	Examples: 
	| From Email Address			| Recieved Date				| Subject                    | Text Body        |
	| newsletter@email.lumosity.com | May 08, 2012 10:03:35 a.m.| Click, Click, Brain Health | Reduce anxiety	|

@VideoSearchVideoPlugin
Scenario Outline: Search Video in Video plugin
	When I search for a '<Video Name>' in 'Video plugin'
	Then I can see that '<Video Name>' it appears in Video search results

	When i click on the image or video search result
	Then the viewer should open
	And the '<Created>','<Last Accessed>' and '<Last Modified>' are present in the header of viewer.
	And the Video is visible to user.

	Examples:
	| Video Name   | Created                         | Last Accessed                 | Last Modified                  |
	| testplay.3gp | November 09, 2011 04:51:28 p.m. | January 25, 2012 06:18:58 p.m | January 09, 2013 10:25:58 a.m. |

@ChatSearchMobileContent
Scenario Outline: Search Chat
	When I search for a '<Text>' in 'Mobile Content'
	Then I can see that '<Message>' or 'Message' appears in Mobile Content search results
	And the  dates '<Date Start>' 'Start Date' are valid in the search result
	And I can see that '<From>' or 'From' appears in Mobile Content search results
	And I can see that '<To>' or 'To' appears in Mobile Content search results

	When i click on the search result
	Then the viewer should open
	And the '<Start Date>', '<Message>', '<To>' and '<From>' are valid for Chat.

	Examples: 
	| Text | Date Start                | To              | From            | Start Date                      | Message             |
	| sod  | 08 Nov 2013 01:33:30 p.m. | 100004885753514 | 100006922600641 | November 08, 2013 01:33:30 p.m. | met me at sod in 30 |

@ImageSearchImages
Scenario Outline: Search Image in Image plugin
	When I search for a '<Image Name>' in 'Images plugin'
	Then I can see that '<Image Name>' it appears in Images search results

	When i click on the image or video search result
	Then the viewer should open
	And the '<Created>','<Last Accessed>' and '<Last Modified>' are present in the header of viewer.
	And the Image is visible to user.

	Examples:
	| Image Name    | Created                        | Last Accessed                  | Last Modified                  |
	| logo_voda.jpg | January 01, 1970 12:00:00 a.m.  | January 25, 2012 06:18:58 p.m. | January 09, 2013 10:25:58 a.m. |

@VErifyDeviceInfo
Scenario: Validate Device Info
When i am on the exhibit page
Then the Device info should be dispalyed
And the Device info should have the following values
| Field               | Value                            |
| Source              | Mobile Device Information Parser |
| DEVICE NAME         | Samsung GT-S5360 Galaxy Y        |
| MANUFACTURER        | Broadcom                         |
| MODEL               | GT-S5360                         |
| REVISION            | BCM21553_Modem_SI1220.2_V2.4     |
| IMEI                | 358757043045932                  |
| SIM STATUS          | UNKNOWN                          |
| MANUFACTURER        | samsung /samsung                 |
| MODEL               | GT-S5360T                        |
| REVISION            | 2.3.6 / /GINGERBREAD             |
| LANGUAGE PREFERENCE | en                               |
| DEVICE TIMEZONE     | Pacific/Auckland                 |
| DEVICE CLOCK        | 2011-01-07T05:33:22+00:00        |
| PC CLOCK            | 2013-11-20T15:36:45+13:00        |
| BLUETOOTH ADDRESS   | E4:B0:21:54:B4:3E                |
| DEVICE STATUS       | Bootmode = unknown               |
| BASEBAND VERSION    | unknown                          |