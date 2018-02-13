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
namespace TestAutomation.Features.EndToEnd_UFED
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Ufed Processing Statistics And Exhibit Properties Check")]
    [NUnit.Framework.CategoryAttribute("smokeTest")]
    [NUnit.Framework.CategoryAttribute("guiTest")]
    [NUnit.Framework.CategoryAttribute("loginAsAdmin")]
    [NUnit.Framework.CategoryAttribute("UfedProcessingStatisticsAndExhibitPropertiesCheck")]
    public partial class UfedProcessingStatisticsAndExhibitPropertiesCheckFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "UfedProcessingStatisticsAndExhibitPropertiesCheck.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Ufed Processing Statistics And Exhibit Properties Check", "This test checks that after processing UFED extracts (logical, v3.9 and v4.1) the" +
                    " Processing Statistics and the Exhibit Properties are populated correctly.", ProgrammingLanguage.CSharp, new string[] {
                        "smokeTest",
                        "guiTest",
                        "loginAsAdmin",
                        "UfedProcessingStatisticsAndExhibitPropertiesCheck"});
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
        [NUnit.Framework.DescriptionAttribute("Check processing statistics and exhibit properties for UFED")]
        [NUnit.Framework.TestCaseAttribute("3.9", "4", "4", "97", "1", "ZTE Corporation", "R101", "864169001022944", "80/01/27,03:08:37", null)]
        [NUnit.Framework.TestCaseAttribute("4.1", "3", "4", "105", "1", "ZTE Corporation", "R101", "864169001022944", "80/01/27,03:08:37", null)]
        [NUnit.Framework.TestCaseAttribute("Logical", "13", "10", "7", "3", "Samsung GSM", "GT-I9506", "359174050829076", "2015-03-09T09:38:15+13:00", null)]
        public virtual void CheckProcessingStatisticsAndExhibitPropertiesForUFED(string version, string imageNumber, string multimediaNumber, string contactNumber, string documentNumber, string deviceInfoDetectedManufacturer, string deviceInfoDetectedModel, string iMEI, string deviceInfoPhoneDateTime, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check processing statistics and exhibit properties for UFED", exampleTags);
#line 5
this.ScenarioSetup(scenarioInfo);
#line 6
 testRunner.Given(string.Format("an exhibit of a version {0}", version), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 7
 testRunner.When("I process that Exhibit", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 8
 testRunner.Then(string.Format("the Exhibit Properties are populated: \'{0}\' DeviceInfoDetectedManufacturer, \'{1}\'" +
                        " DeviceInfoDetectedModel, {2} IMEI, \'{3}\' DeviceInfoPhoneDateTime", deviceInfoDetectedManufacturer, deviceInfoDetectedModel, iMEI, deviceInfoPhoneDateTime), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 9
 testRunner.And(string.Format("the Processing Statistics details are: {0} \'Image\', {1} \'Multimedia\', {2} \'Contac" +
                        "t\', {3} \'Document\'", imageNumber, multimediaNumber, contactNumber, documentNumber), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion