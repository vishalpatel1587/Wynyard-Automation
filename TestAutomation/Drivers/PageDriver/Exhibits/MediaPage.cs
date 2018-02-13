using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Exhibits
{
    public class MediaPage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//span[contains(@id, 'ctl00_MainContent_ctlExhibitMediaDetails_StatisticsTreeView_i') and contains(@id, '_ctl00_StatisticNameLabel')]")]
        private IList<IWebElement> _processingStatisticsLabels;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ctlExhibitMediaDetails_StatisticsTreeView")] private
        IWebElement _processingStatisticsTable;

        [FindsBy(How = How.XPath, Using = "//span[text()='Image']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsImageNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Application']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsApplicationNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Multimedia']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsMultimediaNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Unknown Type']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsUnknownTypeNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Contact']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsContactNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Document']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsDocumentNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Calender']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsCalenderNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Calls']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsCallsNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Chat']/../..//span[contains(@id,'StatisticValueLabel')]")]
        private IWebElement _processingStatisticsChatNumber;

        public MediaPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            //Thread.Sleep(3000);
        }

        public List<string> GetProcessingStatisticsLabels()
        {
            WaitForElementDisplayed(_driver,_processingStatisticsTable);
            return _processingStatisticsLabels.Select(label => label.Text).ToList();
        }

        public string GetProcessingStatisticsNumberOfImages()
        {
            
            return _processingStatisticsImageNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfMultimedia()
        {
           
            return _processingStatisticsMultimediaNumber.Text;
        }
        public string GetProcessingStatisticsNumberOfApplications()
        {
            return _processingStatisticsApplicationNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfUnknownTypeFiles()
        {
            return _processingStatisticsUnknownTypeNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfDocumentFiles()
        {
            return _processingStatisticsDocumentNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfCalenders()
        {
            return _processingStatisticsCalenderNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfCalls()
        {
            return _processingStatisticsCallsNumber.Text;
        }

        public string GetProcessingStatisticsNumberOfChats()
        {
            return _processingStatisticsChatNumber.Text;
        }

          public string GetProcessingStatisticsNumberOfContacts()
        {
            return _processingStatisticsContactNumber.Text;
        }
    }
}
