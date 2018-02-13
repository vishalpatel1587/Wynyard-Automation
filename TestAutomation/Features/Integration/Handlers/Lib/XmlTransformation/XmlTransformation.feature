 @integration @XmlTransformation
Feature: XmlTransformation
	a Xslt transformation is perfomed over a known Xml

Scenario Outline: Transform an identified UFED section
	Given an extract type <extractType> for a certain Ufed file
	And a section <sectionName> is identified
	And that section relates to a specific transformation
	And a valid Xml document is provided
	And a valid Xlst document is provided
	When the XmlTransformation Handler is called
	Then a binary file with a serialized object is saved in a given path

Examples:
| sectionName     | extractType  |
| Contacts        | UfedLogical  |
| Outgoing Calls  | UfedLogical  |
| Incoming Calls  | UfedLogical  |
| Missed Calls    | UfedLogical  |
| SMS Messages    | UfedLogical  |
| MMS Messages    | UfedLogical  |
| Image Files     | UfedLogical  |
| Ringtone Files  | UfedLogical  |
| Audio Files     | UfedLogical  |
| Video Files     | UfedLogical  |
| Calendar Events | UfedLogical  |
| Contacts        | UfedPhysical |
| Calls           | UfedPhysical |
