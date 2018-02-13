﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace TestAutomation.Features.GUI.Search
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Search")]
    [NUnit.Framework.CategoryAttribute("guiTest")]
    [NUnit.Framework.CategoryAttribute("loginAsAdmin")]
    public partial class SearchFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Search.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Search", "AS a DEI user\r\nI want to search for contact names\r\n\r\n*Background: I have logged i" +
                    "n as an admin user.", ProgrammingLanguage.CSharp, new string[] {
                        "guiTest",
                        "loginAsAdmin"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A Search on a Contact Name")]
        [NUnit.Framework.CategoryAttribute("Search")]
        [NUnit.Framework.TestCaseAttribute("3.9", "addie", null)]
        public virtual void ASearchOnAContactName(string version, string contactName, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Search"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A Search on a Contact Name", @__tags);
#line 10
this.ScenarioSetup(scenarioInfo);
#line 11
 testRunner.Given(string.Format("an exhibit of a version {0}", version), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
 testRunner.And("I process that Exhibit", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Name\' appears in Mobile Content search results", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search SMS")]
        [NUnit.Framework.CategoryAttribute("SMSSearch")]
        [NUnit.Framework.TestCaseAttribute("22 Jun 2013 05:41:10 a.m.", "Bal", "June 22, 2013 05:41:10 a.m.", "777 (Customer Service )", null)]
        public virtual void SearchSMS(string dateStart, string smsMessage, string time, string recipient, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "SMSSearch"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search SMS", @__tags);
#line 23
this.ScenarioSetup(scenarioInfo);
#line 24
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.And(string.Format("the  dates \'{0}\' \'Date Start\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
 testRunner.And(string.Format("the \'{0}\', \'{1}\' and \'{2}\' should be valid.", time, recipient, smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute(": Search Contact name in File Metadata plugin")]
        [NUnit.Framework.CategoryAttribute("ContactSearchInFIleMetadata")]
        [NUnit.Framework.TestCaseAttribute("addie", null)]
        public virtual void SearchContactNameInFileMetadataPlugin(string contactName, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactSearchInFIleMetadata"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo(": Search Contact name in File Metadata plugin", @__tags);
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.When(string.Format("I search for a \'{0}\' in \'File Metadata\'", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
 testRunner.Then(string.Format("I can see that \'{0}\' it appears in File Metadata search results", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Bookmark a content on Filemetadata plugin")]
        [NUnit.Framework.CategoryAttribute("BookmarkFilemetadata")]
        public virtual void BookmarkAContentOnFilemetadataPlugin()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Bookmark a content on Filemetadata plugin", new string[] {
                        "BookmarkFilemetadata"});
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
testRunner.When("I try to bookmark the content on \'Mobile Content\' plugin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 50
testRunner.Then("the content should get bookmarked.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Image in Image plugin")]
        [NUnit.Framework.TestCaseAttribute("Pic_0305_013.jpg", "March 05, 2012 05:56:15 a.m.", "March 09, 2015 11:00:00 a.m.", "March 10, 2015 07:39:24 a.m.", null)]
        public virtual void SearchImageInImagePlugin(string imageName, string created, string lastAccessed, string lastModified, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Image in Image plugin", exampleTags);
#line 53
this.ScenarioSetup(scenarioInfo);
#line 54
 testRunner.When(string.Format("I search for a \'{0}\' in \'Images plugin\'", imageName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then(string.Format("I can see that \'{0}\' it appears in Images search results", imageName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 57
 testRunner.When("i click on the image or video search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 59
 testRunner.And(string.Format("the \'{0}\',\'{1}\' and \'{2}\' are valid for images and videos.", created, lastAccessed, lastModified), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And("the Image is visible to user.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Outgoing Calls")]
        [NUnit.Framework.TestCaseAttribute("Outgoing", "0800438448", "09 Jul 2013 08:29:22 a.m.", "09 Jul 2013 08:35:30 a.m.", "368", "July 09, 2013 08:29:22 a.m.", null)]
        public virtual void SearchOutgoingCalls(string direction, string callee, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Outgoing Calls", exampleTags);
#line 67
this.ScenarioSetup(scenarioInfo);
#line 68
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", callee), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 69
 testRunner.Then(string.Format("I can see that \'{0}\' or \'To\' appears in Mobile Content search results", callee), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 70
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 75
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, callee, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Incoming Calls")]
        [NUnit.Framework.TestCaseAttribute("Incoming", "0800800021", "09 Jul 2013 05:36:44 a.m.", "09 Jul 2013 05:53:33 a.m.", "1009", "July 09, 2013 05:36:44 a.m.", null)]
        public virtual void SearchIncomingCalls(string direction, string caller, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Incoming Calls", exampleTags);
#line 82
this.ScenarioSetup(scenarioInfo);
#line 83
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
 testRunner.Then(string.Format("I can see that \'{0}\' or \'From\' appears in Mobile Content search results", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 85
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 90
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, caller, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Missed Calls")]
        [NUnit.Framework.TestCaseAttribute("Missed", "093552000", "09 Jul 2013 11:13:46 p.m.", "09 Jul 2013 11:13:46 p.m.", "0", "July 09, 2013 11:13:46 p.m.", null)]
        public virtual void SearchMissedCalls(string direction, string caller, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Missed Calls", exampleTags);
#line 97
this.ScenarioSetup(scenarioInfo);
#line 98
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
 testRunner.Then(string.Format("I can see that \'{0}\' or \'From\' appears in Mobile Content search results", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 100
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 104
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 105
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, caller, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Calendar")]
        [NUnit.Framework.TestCaseAttribute("carpe", "Christmas", "24 Dec 2012 07:00:00 a.m.", "24 Dec 2012 03:00:00 p.m.", "Paris", "December 24, 2012 07:00:00 a.m.", "December 24, 2012 03:00:00 p.m.", null)]
        public virtual void SearchCalendar(string calendarDetail, string subject, string dateStart, string dateStop, string location, string startDate, string stopDate, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Calendar", exampleTags);
#line 112
this.ScenarioSetup(scenarioInfo);
#line 113
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", calendarDetail), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 114
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", calendarDetail), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 115
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 119
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 120
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\', \'{3}\' and \'{4}\' are valid for calendar.", startDate, stopDate, location, calendarDetail, subject), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search MMS")]
        [NUnit.Framework.TestCaseAttribute("Xxx", "This is the subject", "25 Dec 2012 12:05:37 a.m.", "0278707219", "+64272493699", "December 25, 2012 12:05:37 a.m.", "imagejpeg_2.jpg", null)]
        public virtual void SearchMMS(string mMS, string subject, string dateStart, string to, string from, string startDate, string attachment, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search MMS", exampleTags);
#line 127
this.ScenarioSetup(scenarioInfo);
#line 128
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", mMS), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 129
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", mMS), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 130
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 131
 testRunner.And(string.Format("the  dates \'{0}\' \'From\' are valid in the search result", from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 132
 testRunner.And(string.Format("the  dates \'{0}\' \'To\' are valid in the search result", to), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 134
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 135
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 136
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\', \'{3}\', \'{4}\' and \'{5}\' are valid for MMS.", startDate, subject, mMS, to, attachment, from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 137
 testRunner.And(string.Format("the user should be able to open the \'{0}\'.", attachment), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Video in Video plugin")]
        [NUnit.Framework.TestCaseAttribute("Mov_0106_009.3gp", "January 05, 1980 02:28:10 p.m.", "March 09, 2015 11:00:00 a.m", "March 10, 2015 07:39:24 a.m.", null)]
        public virtual void SearchVideoInVideoPlugin(string videoName, string created, string lastAccessed, string lastModified, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Video in Video plugin", exampleTags);
#line 144
this.ScenarioSetup(scenarioInfo);
#line 145
 testRunner.When(string.Format("I search for a \'{0}\' in \'Video plugin\'", videoName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 146
 testRunner.Then(string.Format("I can see that \'{0}\' it appears in Video search results", videoName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 148
 testRunner.When("i click on the image or video search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 149
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 150
 testRunner.And(string.Format("the \'{0}\',\'{1}\' and \'{2}\' are valid for images and videos.", created, lastAccessed, lastModified), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 151
 testRunner.And("the Video is visible to user.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion