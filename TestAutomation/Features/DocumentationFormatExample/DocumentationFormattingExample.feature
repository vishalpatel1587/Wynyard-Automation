

Feature: Documentation Formatting Example

**Description:**

With Pickles framework

You can create your living documentation

with using most of the features that markdown language provides

Scenario:Using images, links and unordred lists

**General theme for Wynyard application should look like:**

![Wynyard Theme](WynyardTheme.png)

**Screen mockups:**

+ [Login screen mockup](LoginScreenMockup.png)
+ [Home page mockup](HomePageMockup.png)

**Word document**

[Word document Example](../../../Features/DocumentationFormatExample/WordDocument.doc)

Scenario: Using tables

The permissions for Wynyard users should work as follows:

User type		| Permissions granted
---------------	| -------------
admin			| all actions available
investigator	| read, write cases-related info
anotherUserType	|read cases-related info

Scenario:Using links
[Read more about Markdown basics here](https://guides.github.com/features/mastering-markdown/)

[And here :)](http://daringfireball.net/projects/markdown/syntax#header)

Some of the markdown features (heading, code blocks) might not be working correctly (cause Visual Studio parsing error, or do not do what is expected).

Scenario: Linking to other features
For detailed information please see:

+ [Login Feature](../GUI/Login/Login.html)
+ [Add Case Feature](../GUI/Cases/AddCase.html)
+ [Check Home Page Content Feature](../GUI/Home/CheckHomePageContent.html)

Depending on a browser the feature files will be downloaded to your PC.