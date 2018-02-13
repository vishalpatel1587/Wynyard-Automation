using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Exceptions;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Cases
{
    public class AddCasePage : PageBase
    {
        private IWebDriver _driver;
        
        [FindsBy(How = How.Id, Using = "MainContent_btnSave")]
        private IWebElement _saveButton;

        [FindsBy(How = How.Id, Using = "MainContent_txtPoliceFileNumber")]
        private IWebElement _policeFileNumber;

        [FindsBy(How = How.Id, Using = "MainContent_txtECLCaseNumber")]
        private IWebElement _eclCaseNumber;

        [FindsBy(How = How.Id, Using = "MainContent_txtDescription")]
        private IWebElement _description;

        [FindsBy(How = How.Id, Using = "MainContent_ddlOfficerInCharge")]
        private IWebElement _officerInCharge;

        [FindsBy(How = How.Id, Using = "MainContent_ddlDistrict")]
        private IWebElement _district;

        [FindsBy(How = How.Id, Using = "MainContent_ddlOffenceType")]
        private IWebElement _offenceType;

        [FindsBy(How = How.Id, Using = "MainContent_ddlAcquisitionSite")]
        private IWebElement _acquisitionSite;

        public AddCasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            
            _driver.SwitchTo().Frame(0);
            try
            {
                WaitForElementDisplayed(_driver, _saveButton);
            }
            catch (Exception e)
            {
                throw new ElementNotDisplayedException("Add Case page is expected to be shown with the Save button (id='" + _saveButton.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }
        }

        internal CasesPage AddCase(Model.DataModel.CaseDataModel caseDataModel)
        {
            TypeInto(_policeFileNumber, caseDataModel.PoliceFileNumber);
            TypeInto(_eclCaseNumber, caseDataModel.EclCaseNumber);
            TypeInto(_description, caseDataModel.Description);
            SelectFromDropdown(_officerInCharge, caseDataModel.OfficerInCharge);
            SelectFromDropdown(_district, caseDataModel.District);
            SelectFromDropdown(_offenceType, caseDataModel.OffenceType);
            SelectFromDropdown(_acquisitionSite, caseDataModel.AcquisitionSite);

            _saveButton.Click();
            return new CasesPage(_driver);
        }
    }
}
