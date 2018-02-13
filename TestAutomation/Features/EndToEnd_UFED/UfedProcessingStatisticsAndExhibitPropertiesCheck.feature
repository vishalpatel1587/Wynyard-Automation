@smokeTest @guiTest @loginAsAdmin @UfedProcessingStatisticsAndExhibitPropertiesCheck
Feature: Ufed Processing Statistics And Exhibit Properties Check
	 This test checks that after processing UFED extracts (logical, v3.9 and v4.1) the Processing Statistics and the Exhibit Properties are populated correctly.

Scenario Outline: Check processing statistics and exhibit properties for UFED
	Given an exhibit of a version <version>
	When I process that Exhibit
	Then the Exhibit Properties are populated: '<DeviceInfoDetectedManufacturer>' DeviceInfoDetectedManufacturer, '<DeviceInfoDetectedModel>' DeviceInfoDetectedModel, <IMEI> IMEI, '<DeviceInfoPhoneDateTime>' DeviceInfoPhoneDateTime
	And the Processing Statistics details are: <ImageNumber> 'Image', <MultimediaNumber> 'Multimedia', <ContactNumber> 'Contact', <DocumentNumber> 'Document'

	Examples: 

	| version | ImageNumber | MultimediaNumber | ContactNumber | DocumentNumber | DeviceInfoDetectedManufacturer | DeviceInfoDetectedModel | IMEI            | DeviceInfoPhoneDateTime   |
	| 3.9     | 4           | 4                | 97            | 1              | ZTE Corporation                | R101                    | 864169001022944 | 80/01/27,03:08:37         |
	| 4.1     | 3           | 4                | 105           | 1              | ZTE Corporation                | R101                    | 864169001022944 | 80/01/27,03:08:37         |
	| Logical | 13          | 10               | 7             | 3              | Samsung GSM                    | GT-I9506                | 359174050829076 | 2015-03-09T09:38:15+13:00 |