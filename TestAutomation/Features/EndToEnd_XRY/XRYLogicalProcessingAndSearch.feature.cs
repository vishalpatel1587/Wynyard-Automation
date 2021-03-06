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
namespace TestAutomation.Features.EndToEnd_XRY
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Xry Logical Processing And Search")]
    [NUnit.Framework.CategoryAttribute("smokeTest")]
    [NUnit.Framework.CategoryAttribute("guiTest")]
    [NUnit.Framework.CategoryAttribute("loginAsAdmin")]
    public partial class XryLogicalProcessingAndSearchFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "XRYLogicalProcessingAndSearch.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Xry Logical Processing And Search", "This test checks that after processing XRY logical extracts the Processing Statis" +
                    "tics and the Exhibit Properties are populated correctly.", ProgrammingLanguage.CSharp, new string[] {
                        "smokeTest",
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
        [NUnit.Framework.DescriptionAttribute("Process an XRY exhibit")]
        [NUnit.Framework.CategoryAttribute("processXRY")]
        [NUnit.Framework.TestCaseAttribute("Logical", null)]
        public virtual void ProcessAnXRYExhibit(string version, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "processXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Process an XRY exhibit", @__tags);
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given(string.Format("an exhibit of a version {0}", version), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.When("I process that Exhibit", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Contact Name")]
        [NUnit.Framework.CategoryAttribute("SearchXRYContactName")]
        [NUnit.Framework.TestCaseAttribute("Morgen", null)]
        public virtual void SearchContactName(string contactName, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "SearchXRYContactName"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Contact Name", @__tags);
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Name\' appears in Mobile Content search results", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Calendar")]
        [NUnit.Framework.CategoryAttribute("CalenderSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("Waxing 4.30", "Waxing 4.30", "28 May 2012 12:00:00 a.m.", "28 May 2012 01:00:00 a.m.", "Wairakei road", "May 28, 2012 12:00:00 a.m.", "May 28, 2012 01:00:00 a.m.", null)]
        public virtual void SearchCalendar(string calendarDetail, string subject, string dateStart, string dateStop, string location, string startDate, string stopDate, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "CalenderSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Calendar", @__tags);
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", calendarDetail), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", calendarDetail), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 27
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\', \'{3}\' and \'{4}\' are valid for calendar.", startDate, stopDate, location, calendarDetail, subject), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Outgoing Calls")]
        [NUnit.Framework.CategoryAttribute("OutgoingCallsSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("Dialled", "+64221863220", "14 Oct 2012 02:58:47 p.m.", "14 Oct 2012 02:59:29 p.m.", "42", "October 14, 2012 02:58:47 p.m.", null)]
        public virtual void SearchOutgoingCalls(string direction, string callee, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "OutgoingCallsSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Outgoing Calls", @__tags);
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", callee), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
 testRunner.Then(string.Format("I can see that \'{0}\' or \'To\' appears in Mobile Content search results", callee), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, callee, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Incoming Calls")]
        [NUnit.Framework.CategoryAttribute("IncomingCallsSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("Received", "0212444061", "02 Jun 2012 03:53:13 a.m.", "02 Jun 2012 03:53:45 a.m.", "32", "June 02, 2012 03:53:13 a.m.", null)]
        public virtual void SearchIncomingCalls(string direction, string caller, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "IncomingCallsSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Incoming Calls", @__tags);
#line 54
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
 testRunner.Then(string.Format("I can see that \'{0}\' or \'From\' appears in Mobile Content search results", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 57
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 62
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, caller, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Missed Calls")]
        [NUnit.Framework.CategoryAttribute("MissedCallsSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("Missed", "0220878224", "18 Oct 2012 11:39:53 p.m.", "18 Oct 2012 11:39:53 p.m.", "0", "October 18, 2012 11:39:53 p.m.", null)]
        public virtual void SearchMissedCalls(string direction, string caller, string dateStart, string dateStop, string duration, string time, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "MissedCallsSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Missed Calls", @__tags);
#line 69
this.ScenarioSetup(scenarioInfo);
#line 70
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
 testRunner.Then(string.Format("I can see that \'{0}\' or \'From\' appears in Mobile Content search results", caller), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 72
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And(string.Format("the  dates \'{0}\' \'Stop Date\' are valid in the search result", dateStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 76
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 77
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\' and \'{3}\' are valid.", time, duration, caller, direction), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search SMS")]
        [NUnit.Framework.CategoryAttribute("SMSSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("01 Jan 2013 05:23:02 p.m.", "caravan", "January 01, 2013 05:23:02 p.m.", "0276018882", null)]
        public virtual void SearchSMS(string startDate, string smsMessage, string time, string recipient, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "SMSSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search SMS", @__tags);
#line 85
this.ScenarioSetup(scenarioInfo);
#line 86
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 87
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 88
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", startDate), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
 testRunner.And(string.Format("the \'{0}\', \'{1}\' and \'{2}\' should be present in viewer text.", time, recipient, smsMessage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search MMS")]
        [NUnit.Framework.CategoryAttribute("MMSSearchXRY")]
        [NUnit.Framework.TestCaseAttribute("64212252994", "", "03 Dec 2013 09:56:13 a.m.", "64211618519", "+64212252994", "December 03, 2013 09:56:13 a.m.", "20131204_105022.jpeg", null)]
        public virtual void SearchMMS(string mMS, string subject, string dateStart, string to, string from, string startDate, string attachment, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "MMSSearchXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search MMS", @__tags);
#line 100
this.ScenarioSetup(scenarioInfo);
#line 101
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", mMS), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 102
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", mMS), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 103
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
 testRunner.And(string.Format("the  dates \'{0}\' \'From\' are valid in the search result", from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.And(string.Format("the  dates \'{0}\' \'To\' are valid in the search result", to), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 109
 testRunner.And(string.Format("the \'{0}\',\'{1}\', \'{2}\', \'{3}\', \'{4}\' and \'{5}\' are valid for MMS.", startDate, subject, mMS, to, attachment, from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
 testRunner.And(string.Format("the user should be able to open the \'{0}\'.", attachment), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute(": Search Contact name in File Metadata plugin")]
        [NUnit.Framework.CategoryAttribute("ContactSearchInFIleMetadataXRY")]
        [NUnit.Framework.TestCaseAttribute("Morgen", null)]
        public virtual void SearchContactNameInFileMetadataPlugin(string contactName, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactSearchInFIleMetadataXRY"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo(": Search Contact name in File Metadata plugin", @__tags);
#line 117
this.ScenarioSetup(scenarioInfo);
#line 118
 testRunner.When(string.Format("I search for a \'{0}\' in \'File Metadata\'", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 119
 testRunner.Then(string.Format("I can see that \'{0}\' appears in File Metadata search results", contactName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute(": Search Emails in MobileContent")]
        [NUnit.Framework.CategoryAttribute("emailSearchInMobileContent")]
        [NUnit.Framework.TestCaseAttribute("newsletter@email.lumosity.com", "ellie_lp@hotmail.co.nz", "Sent", "08 May 2012 10:03:35 a.m.", "Click, Click, Brain Health", "brain training program", null)]
        public virtual void SearchEmailsInMobileContent(string fromEmailAddress, string toEmailAddress, string status, string receivedDate, string subject, string textBody, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "emailSearchInMobileContent"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo(": Search Emails in MobileContent", @__tags);
#line 126
this.ScenarioSetup(scenarioInfo);
#line 127
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", subject), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 128
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Subject\' appears in Mobile Content search results", subject), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute(": Search Emails in Content")]
        [NUnit.Framework.CategoryAttribute("EmailSearchContent")]
        [NUnit.Framework.TestCaseAttribute("newsletter@email.lumosity.com", "May 08, 2012 10:03:35 a.m.", "Click, Click, Brain Health", "Reduce anxiety", null)]
        public virtual void SearchEmailsInContent(string fromEmailAddress, string recievedDate, string subject, string textBody, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "EmailSearchContent"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo(": Search Emails in Content", @__tags);
#line 135
this.ScenarioSetup(scenarioInfo);
#line 136
 testRunner.When(string.Format("I search for a \'{0}\' in \'Content Search\'", subject), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 137
 testRunner.Then(string.Format("I can see that \'{0}\' appears in Content search results", fromEmailAddress), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 139
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 140
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 141
 testRunner.And(string.Format("the \'{0}\', \'{1}\' and \'{2}\' should be present in viewer text.", recievedDate, subject, textBody), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Video in Video plugin")]
        [NUnit.Framework.CategoryAttribute("VideoSearchVideoPlugin")]
        [NUnit.Framework.TestCaseAttribute("testplay.3gp", "November 09, 2011 04:51:28 p.m.", "January 25, 2012 06:18:58 p.m", "January 09, 2013 10:25:58 a.m.", null)]
        public virtual void SearchVideoInVideoPlugin(string videoName, string created, string lastAccessed, string lastModified, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "VideoSearchVideoPlugin"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Video in Video plugin", @__tags);
#line 148
this.ScenarioSetup(scenarioInfo);
#line 149
 testRunner.When(string.Format("I search for a \'{0}\' in \'Video plugin\'", videoName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 150
 testRunner.Then(string.Format("I can see that \'{0}\' it appears in Video search results", videoName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 152
 testRunner.When("i click on the image or video search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 153
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 154
 testRunner.And(string.Format("the \'{0}\',\'{1}\' and \'{2}\' are present in the header of viewer.", created, lastAccessed, lastModified), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 155
 testRunner.And("the Video is visible to user.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Chat")]
        [NUnit.Framework.CategoryAttribute("ChatSearchMobileContent")]
        [NUnit.Framework.TestCaseAttribute("sod", "08 Nov 2013 01:33:30 p.m.", "100004885753514", "100006922600641", "November 08, 2013 01:33:30 p.m.", "met me at sod in 30", null)]
        public virtual void SearchChat(string text, string dateStart, string to, string from, string startDate, string message, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ChatSearchMobileContent"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Chat", @__tags);
#line 162
this.ScenarioSetup(scenarioInfo);
#line 163
 testRunner.When(string.Format("I search for a \'{0}\' in \'Mobile Content\'", text), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 164
 testRunner.Then(string.Format("I can see that \'{0}\' or \'Message\' appears in Mobile Content search results", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 165
 testRunner.And(string.Format("the  dates \'{0}\' \'Start Date\' are valid in the search result", dateStart), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 166
 testRunner.And(string.Format("I can see that \'{0}\' or \'From\' appears in Mobile Content search results", from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 167
 testRunner.And(string.Format("I can see that \'{0}\' or \'To\' appears in Mobile Content search results", to), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 169
 testRunner.When("i click on the search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 170
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 171
 testRunner.And(string.Format("the \'{0}\', \'{1}\', \'{2}\' and \'{3}\' are valid for Chat.", startDate, message, to, from), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search Image in Image plugin")]
        [NUnit.Framework.CategoryAttribute("ImageSearchImages")]
        [NUnit.Framework.TestCaseAttribute("logo_voda.jpg", "January 01, 1970 12:00:00 a.m.", "January 25, 2012 06:18:58 p.m.", "January 09, 2013 10:25:58 a.m.", null)]
        public virtual void SearchImageInImagePlugin(string imageName, string created, string lastAccessed, string lastModified, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ImageSearchImages"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Image in Image plugin", @__tags);
#line 178
this.ScenarioSetup(scenarioInfo);
#line 179
 testRunner.When(string.Format("I search for a \'{0}\' in \'Images plugin\'", imageName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 180
 testRunner.Then(string.Format("I can see that \'{0}\' it appears in Images search results", imageName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 182
 testRunner.When("i click on the image or video search result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 183
 testRunner.Then("the viewer should open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 184
 testRunner.And(string.Format("the \'{0}\',\'{1}\' and \'{2}\' are present in the header of viewer.", created, lastAccessed, lastModified), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 185
 testRunner.And("the Image is visible to user.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate Device Info")]
        [NUnit.Framework.CategoryAttribute("VErifyDeviceInfo")]
        public virtual void ValidateDeviceInfo()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Device Info", new string[] {
                        "VErifyDeviceInfo"});
#line 192
this.ScenarioSetup(scenarioInfo);
#line 193
testRunner.When("i am on the exhibit page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 194
testRunner.Then("the Device info should be dispalyed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "Source",
                        "Mobile Device Information Parser"});
            table1.AddRow(new string[] {
                        "DEVICE NAME",
                        "Samsung GT-S5360 Galaxy Y"});
            table1.AddRow(new string[] {
                        "MANUFACTURER",
                        "Broadcom"});
            table1.AddRow(new string[] {
                        "MODEL",
                        "GT-S5360"});
            table1.AddRow(new string[] {
                        "REVISION",
                        "BCM21553_Modem_SI1220.2_V2.4"});
            table1.AddRow(new string[] {
                        "IMEI",
                        "358757043045932"});
            table1.AddRow(new string[] {
                        "SIM STATUS",
                        "UNKNOWN"});
            table1.AddRow(new string[] {
                        "MANUFACTURER",
                        "samsung /samsung"});
            table1.AddRow(new string[] {
                        "MODEL",
                        "GT-S5360T"});
            table1.AddRow(new string[] {
                        "REVISION",
                        "2.3.6 / /GINGERBREAD"});
            table1.AddRow(new string[] {
                        "LANGUAGE PREFERENCE",
                        "en"});
            table1.AddRow(new string[] {
                        "DEVICE TIMEZONE",
                        "Pacific/Auckland"});
            table1.AddRow(new string[] {
                        "DEVICE CLOCK",
                        "2011-01-07T05:33:22+00:00"});
            table1.AddRow(new string[] {
                        "PC CLOCK",
                        "2013-11-20T15:36:45+13:00"});
            table1.AddRow(new string[] {
                        "BLUETOOTH ADDRESS",
                        "E4:B0:21:54:B4:3E"});
            table1.AddRow(new string[] {
                        "DEVICE STATUS",
                        "Bootmode = unknown"});
            table1.AddRow(new string[] {
                        "BASEBAND VERSION",
                        "unknown"});
#line 195
testRunner.And("the Device info should have the following values", ((string)(null)), table1, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
