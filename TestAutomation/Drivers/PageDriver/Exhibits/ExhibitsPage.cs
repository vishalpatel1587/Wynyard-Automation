using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;
using TestAutomation.Exceptions;
using TestAutomation.Model.DataModel;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Exhibits
{
    public class ExhibitsPage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "MainContent_ExhibitTreeDiv")]
        private IWebElement _mainContentExhibitTreeDiv;

        [FindsBy(How = How.Id, Using = "MainContent_AddExhibitLink")]
        private IWebElement _addExhibitLink;

        [FindsBy(How = How.Id, Using = "MainContent_txtECLExhibitNumber")]
        private IWebElement _eclExhibitNumber;

        [FindsBy(How = How.Id, Using = "MainContent_txtPoliceExhibitNumber")]
        private IWebElement _policeExhibitNumber;

        [FindsBy(How = How.Id, Using = "MainContent_txtDescription")]
        private IWebElement _description;

        [FindsBy(How = How.Id, Using = "MainContent_btnSave")]
        private IWebElement _saveButton;

        [FindsBy(How = How.Id, Using = "MainContent_txtMediaNumber")]
        private IWebElement _mediaNumber;

        [FindsBy(How = How.Id, Using = "MainContent_txtDescription")]
        private IWebElement _mediaDescription;

        [FindsBy(How = How.Id, Using = "MainContent_txtEvidencePath")]
        private IWebElement _mediaEvidencePath;

        [FindsBy(How = How.Id, Using = "MainContent_ddlAcquisitionFormat")]
        private IWebElement _mediaAcquisitionFormat;

        [FindsBy(How = How.Id, Using = "MainContent_ddlTimezone")]
        private IWebElement _mediaTimeZone;

        [FindsBy(How = How.XPath, Using = "//span[contains(@id, 'ctl00_MainContent_ExhibitTreeView_i') and contains(@id, '_ctlExhibitNode_ExhibitNumberLabel')]")]
        private IList<IWebElement> _exhibitNumbersList;

        [FindsBy(How = How.CssSelector, Using = "#ctl00_MainContent_ExhibitTreeView>ul>li")]
        private IList<IWebElement> _exhibitsList;

        [FindsBy(How=How.XPath,Using="//li[contains(@class,'rtLI')]")]
        //[FindsBy(How = How.CssSelector, Using = "#ctl00_MainContent_ExhibitTreeView>ul>li")]
        private IList<IWebElement> _mediaList;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ExhibitTreeView_i0_i0_i0_ctlExhibitNodeMedia_MediaNumberLabel")]
        //[FindsBy(How = How.CssSelector, Using = "#ctl00_MainContent_ExhibitTreeView>ul>li")]
        private IWebElement _mediaAdded;
        
        [FindsBy(How = How.XPath, Using = "//span[contains(@id, 'ctl00_MainContent_ExhibitTreeView_i0_i0_') and contains(@id, '_HeadingLabel')]")]
        private IWebElement _mediaLink;

        //[FindsBy(How = How.XPath, Using = "//span[contains(@id='ctl00_MainContent_ExhibitTreeView_i0_i0_i0_ctlExhibitNodeMedia_MediaNumberLabel')]")]
        //private IWebElement _media

        [FindsBy(How = How.Id, Using = "MainContent_ctlExhibitMediaAdd_AddMediaLink")]
        private IWebElement _addMediaLink;

        [FindsBy(How = How.Id, Using = "MainContent_ctlExhibitDetails_ProcessExhibitLink")]
        private IWebElement _processExhibitLink;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_ctlExhibitDetails_MetadataDiv']/table")]
        private IWebElement _deviceinfo;

        [FindsBy(How = How.Id, Using = "MainContent_ctlExhibitDetails_EditExhibitLink")]
        private IWebElement _editExhibitLink;

        [FindsBy(How = How.Id, Using = "MainContent_btnProcess")]
        private IWebElement _processExhibitDialogButton;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ctl00_WorkflowOptionList")]
        private IWebElement _workflowOptionListToggle;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ctl00_WorkflowOptionList_DropDown")]
        private IWebElement _workflowOptionList;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_WorkflowCustomNameSave_OptionsCustomNameMenu']//a//span[text()='Save']")]
        private IWebElement _saveCustomWorkflowLink;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i0_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowDeletedFilesCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i1_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowLppCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i2_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowMultimediaCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i2_i0_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowVideoClassificationCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i3_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowImagesCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i3_i0_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowImageRatingCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i3_i1_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowImageClassificationCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i4_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowContentCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i5_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowEmailCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i6_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowBrowserCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_ctl00_ActivityTypeTreeView_i7_ctl00_CaptionLabel']/../../../label/input[@type='checkbox']")]
        private IWebElement _workflowUacCheckbox;

        [FindsBy(How = How.Id, Using = "MainContent_ctl00_DeleteWorkflowLink")]
        private IWebElement _deleteWorkflowLink;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ctl00_WorkflowCustomNameSave_OptionsCustomNameMenu_i0_i0_SaveButton")]
        private IWebElement _saveCustomWorkflowButton;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ctl00_WorkflowCustomNameSave_OptionsCustomNameMenu_i0_i0_CustomNameTextBox")]
        private IWebElement _customWorkflowNameInputField;
        
        [FindsBy(How = How.Id, Using = "ctl00_MainContent_ExhibitTreeView_i0_i0_i0_ctlExhibitNodeMedia_MediaNumberLabel")]
        private IWebElement _firstMediaLabel;

        private Dictionary<WorkflowDataModel.WorkflowMediaProcessingOptions, IWebElement> _workflowProcessingOptionToElementMap =
        new Dictionary<WorkflowDataModel.WorkflowMediaProcessingOptions, IWebElement>();

        private string _exhibitStatusIconRelatedToExhibitNumberElementXpath = "./../../../../../..//*[contains(@id,'MediaStatusImage')]";

        public void IsMediaPresent(string ExpectedMediaNumber)
        {
            try
            {
                //System.Threading.Thread.Sleep(500);
                Console.Write(_mediaList.GetType());
                WaitForElementDisplayed(_driver, _mediaAdded);
                //return _mediaList.Any(media => media.Text.Equals(ExpectedMediaNumber));
            }
            catch (Exception e)
            {
                
                throw new Exception(@"The media with "+ExpectedMediaNumber+" doesnot exist on the page"+e);
            }
           
        }
        public ExhibitsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            try
            {
                WaitForElementDisplayed(_driver, _mainContentExhibitTreeDiv);
            }
            catch (Exception e)
            {
                throw new ElementNotDisplayedException("Case Exhibits page is expected to be shown with Exhibits list div (id='" + _mainContentExhibitTreeDiv.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }

            InitialiseWorkflowProcessingOptionToElementMap();
        }

        public ExhibitsPage AddExhibit(ExhibitDataModel exhibitDataModel)
        {
            var initialNumberOfExistingExhibits = GetNumberOfDisplayedExhibits();
            _addExhibitLink.Click();
            _driver.SwitchTo().Frame(0);
            WaitForElementDisplayed(_driver, _saveButton);

            AddExhibitDetailsAndSave(exhibitDataModel);
            _driver.SwitchTo().DefaultContent();
            if (!HasNumberOfExhibitsIncreasedBy1(initialNumberOfExistingExhibits))
            {
                throw new Exception("An exhibit with number='" + exhibitDataModel.EclExhibitNumber + "' might not be added as expected. It might be not present in Exhibits list as the number of exhibits did not increase by 1.");
            }
            return new ExhibitsPage(_driver);
        }

       
        public bool DoesExhibitExistInListByExhibitNumber(string exhibitNumber)
        {
            return _exhibitNumbersList.Any(exhibit => exhibit.Text.Equals(exhibitNumber));
        }
        public ExhibitsPage AddMedia(MediaDataModel mediaDataModel,out bool isEnabled)
        {
            WaitForElementDisplayed(_driver, _mediaLink);
            _mediaLink.Click();
            WaitForElementDisplayed(_driver, _addMediaLink);
            _addMediaLink.Click();
            _driver.SwitchTo().Frame(0);
            WaitForElementDisplayed(_driver, _saveButton);


            TypeInto(_mediaNumber, mediaDataModel.MediaNumber);
            TypeInto(_mediaDescription, mediaDataModel.Description);
            TypeInto(_mediaEvidencePath, mediaDataModel.EvidencePath);
            SelectFromDropdown(_mediaAcquisitionFormat, mediaDataModel.AcquisitionFormat);

            if (mediaDataModel.AcquisitionFormat == "MobileDeviceUfed")
            {
                WaitForElementDisplayed(_driver, _mediaTimeZone);
                 // tells that the dropdown is present.
                SelectFromDropdown(_mediaTimeZone, mediaDataModel.TimeZone);
                
            }
            
            _driver.SwitchTo().DefaultContent();
            //DO NOT remove this sleep, unless there is a way now to wait for a new media to appear on Exhibits list in other way.
            System.Threading.Thread.Sleep(500);
            isEnabled = true;
            return new ExhibitsPage(_driver);
        }

        public void SelectUnprocessedExhibitFromListByEclExhibitNumber(string exhibitNumber)
        {
            ClickOnElementFromListFoundByElementText(_driver, _exhibitNumbersList, exhibitNumber);
            WaitForElementDisplayed(_driver, _processExhibitLink);
        }

        public void SelectProcessedExhibitFromListByEclExhibitNumber(string exhibitNumber)
        {
            ClickOnElementFromListFoundByElementText(_driver, _exhibitNumbersList, exhibitNumber);
            WaitForElementDisplayed(_driver, _editExhibitLink);
        }

        public void SelectProcessExhibitOption(string exhibitNumber)
        {
            SelectUnprocessedExhibitFromListByEclExhibitNumber(exhibitNumber);
            RetryToClick(_processExhibitLink);

            _driver.SwitchTo().Frame(0);
            WaitForElementDisplayed(_driver, _processExhibitDialogButton);
        }

        public void IsDeviceInfoDisplayed()
        {
            try
            {
                Assert.True(_deviceinfo.Displayed);
            }
            catch (Exception e)
            {
                
                throw new Exception("The device info for the exhibit isn't visible. Exception "+e);
            }
            
        }


     

        public void IsDeviceInfoValid(Table expectedTable)
        {
           

            int totalLabel = _driver.FindElements(By.XPath("//*[@id='MainContent_ctlExhibitDetails_MetadataDiv']/table/tbody/tr")).Count;
            Console.WriteLine(totalLabel);
            try
            {

                Assert.True(expectedTable.RowCount == totalLabel);
              
            }
            catch (Exception)
            {
                
                throw new Exception("The expected number of labels were "+expectedTable.RowCount+" but the acutal numbers of labels present on the device info are "+totalLabel);
            }

            IWebElement[] label = new IWebElement[totalLabel];
            IWebElement[] value = new IWebElement[totalLabel];
            
            string[] lableString=new string[totalLabel];
            string[] valueString=new string[totalLabel];
            int i=0;
            foreach (TableRow row  in expectedTable.Rows)
            {
                lableString[i] = row["Field"].ToString();
                valueString[i] = row["Value"].ToString();
                i++;
            }

            for (int count = 1; count <= totalLabel; count++)
            {
                try
                {
                    label[count - 1] = _driver.FindElement(By.XPath("//*[@id='MainContent_ctlExhibitDetails_MetadataDiv']/table/tbody/tr[" + count + "]/td[1]"));
                    value[count - 1] = _driver.FindElement(By.XPath("//*[@id='MainContent_ctlExhibitDetails_MetadataDiv']/table/tbody/tr[" + count + "]/td[2]"));
                }
                catch (Exception)
                {
                    
                    throw new Exception("Unable to find the expected lable or value in Device Info with index "+count);
                }
               
                try
                {
                    Assert.True(label[count-1].Text==lableString[count-1]);
                    Assert.True(value[count-1].Text==valueString[count-1]);
                }
                catch (Exception)
                {
                    
                    throw new Exception("The actual lable "+label[count-1].Text+" or value "+value[count-1].Text+
                        " didnot match with the expected label "+lableString[count-1]+" or value "+valueString[count-1]+" in the Device Info");
                }
   
            }
            
        }

        public ExhibitsPage ClickProcessExhibitButton()
        {
            var action = new Actions(_driver);
            action.MoveToElement(_processExhibitDialogButton).Build().Perform();
            action.Click(_processExhibitDialogButton).Build().Perform();
            // _processExhibitDialogButton.Click();
            _driver.SwitchTo().DefaultContent();
            return new ExhibitsPage(_driver);
        }
        //TODO OV review this method
        public ExhibitsPage ProcessExhibit(string exhibitNumber)
        {
            SelectProcessExhibitOption(exhibitNumber);
            return ClickProcessExhibitButton();
        }

        //TODO OV review the max time to wait for an exhibit to be marked as Queued.
        //added Stopwatch to retry to look for Queued status. As the status is not refreshed immediately after selecting processing an exhibit.
        public bool IsExhibitMarkedAsQueued(string exhibitNumber)
        {
            var errorMessage = string.Empty;
            const int maxSecondsToWait = 5;
            var sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed < TimeSpan.FromSeconds(maxSecondsToWait))
            {
                try
                {
                    var element = FindElementFromListByElementText(_driver, _exhibitNumbersList, exhibitNumber);
                    var title = element.FindElement(By.XPath(_exhibitStatusIconRelatedToExhibitNumberElementXpath)).GetAttribute("title");
                    if (!title.Equals("Queued")) continue;
                    sw.Stop();
                    return true;
                }
                catch (StaleElementReferenceException e)
                {
                    errorMessage = e.ToString();
                }
            }
            sw.Stop();
            if (!errorMessage.Equals(string.Empty))
            {
                throw new StaleElementReferenceException(errorMessage);
            }
            return false;
        }

        public int GetNumberOfDisplayedExhibits()
        {
            return _exhibitsList.Count;
        }

        public bool HasNumberOfExhibitsIncreasedBy1(int initialNumberOfExhistingExhibits)
        {
            var result = false;
            var maxSecondsToWait = Convert.ToInt32(ConfigurationManager.AppSettings.Get("dbRetrieveTimeout"));
            var s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(maxSecondsToWait))
            {
                var currentNumberOfExhibits = GetNumberOfDisplayedExhibits();
                if (currentNumberOfExhibits == initialNumberOfExhistingExhibits + 1)
                {
                    result = true;
                    break;
                }
            }
            s.Stop();
            return result;
        }

        public void SelectWorkflow(string workflowOption)
        {
            WaitForElementDisplayed(_driver, _workflowOptionListToggle);
            _workflowOptionListToggle.Click();
            WaitForElementDisplayed(_driver, _workflowOptionList);
            _workflowOptionList.FindElement(By.XPath("//*[text()='" + workflowOption + "']")).Click();

            //case of a new Custom workflow
            if (workflowOption.Equals(WorkflowDataModel.DefaultWorkflowOptionValues.Custom.ToString()))
            {
                WaitForElementDisplayed(_driver, _saveCustomWorkflowLink);
            }

            //case of a custom saved workflow
            //If a workflowOption supplied is none of the default options, then we assume it is a custom workflow.
            var isCustomSavedWorkflow = Enum.GetValues(typeof(WorkflowDataModel.DefaultWorkflowOptionValues)).Cast<WorkflowDataModel.DefaultWorkflowOptionValues>().Any(value => value.ToString().Equals(workflowOption));
            if (!isCustomSavedWorkflow)
            {
                WaitForElementDisplayed(_driver, _deleteWorkflowLink);
            }
        }

        public bool IsWorkflowPresentInList(string expectedWorkflowOption)
        {
            _workflowOptionListToggle.Click();
            WaitForElementDisplayed(_driver, _workflowOptionList);
            try
            {
                _workflowOptionList.FindElement(By.XPath("//*[text()='" + expectedWorkflowOption + "']"));
                _workflowOptionListToggle.Click();
                return true;
            }
            catch (Exception e)
            {
                _workflowOptionListToggle.Click();
                throw new Exception("Workflow option '" + expectedWorkflowOption + "' is not present in Workflow options list. Exception is: " + e.ToString());
            }
        }

        /**
         * Based on the current behavior where by default all media processing options are selected.
         * */
        public ExhibitsPage AddCustomWorkflow(WorkflowDataModel workflowDataModel)
        {
            SelectWorkflow(workflowDataModel.WorkflowOption);
            SetWorkflowMediaProcessingOptions(workflowDataModel);
            _saveCustomWorkflowLink.Click();
            WaitForElementDisplayed(_driver, _customWorkflowNameInputField);

            _customWorkflowNameInputField.SendKeys(workflowDataModel.CustomWorkflowName);

            _saveCustomWorkflowButton.Click();

            WaitForElementDisplayed(_driver, _deleteWorkflowLink);

            _driver.Navigate().Refresh();

            _driver.SwitchTo().DefaultContent();
            return new ExhibitsPage(_driver);
        }

        public void SetWorkflowMediaProcessingOptions(WorkflowDataModel workflowDataModel)
        {
            //TODO OV this options are only valid for certain workflow config.
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowDeletedFilesCheckbox, workflowDataModel.DeletedFiles);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowLppCheckbox, workflowDataModel.Lpp);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowMultimediaCheckbox, workflowDataModel.Multimedia);
            // SelectOrDeselectkWorkflowMediaProcessingOption(_workflowVideoClassificationCheckbox, workflowDataModel.VideoClassification);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowImagesCheckbox, workflowDataModel.Images);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowImageRatingCheckbox, workflowDataModel.ImageRating);
            //SelectOrDeselectkWorkflowMediaProcessingOption(_workflowImageClassificationCheckbox, workflowDataModel.ImageClassification);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowContentCheckbox, workflowDataModel.Content);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowEmailCheckbox, workflowDataModel.Email);
            SelectOrDeselectkWorkflowMediaProcessingOption(_workflowBrowserCheckbox, workflowDataModel.Browser);
            //SelectOrDeselectkWorkflowMediaProcessingOption(_workflowUacCheckbox, workflowDataModel.Uac);
        }

        public void SelectOrDeselectkWorkflowMediaProcessingOption(IWebElement option, bool whetherToSelect)
        {
            if (whetherToSelect)
            {
                CheckCheckboxIfUnchecked(_driver, option);
            }
            else
            {
                UncheckCheckboxIfChecked(_driver, option);
            }
        }

        public bool IsWorkflowMediaProcessingOptionSelected(WorkflowDataModel.WorkflowMediaProcessingOptions option)
        {
            try
            {
                IWebElement element;
                _workflowProcessingOptionToElementMap.TryGetValue(option, out element);
                return element.Selected;
            }
            catch (Exception)
            {
                throw new Exception("Workflow media processing option specified '" + option.ToString() + "' might not exist.");

            }
        }

        private void InitialiseWorkflowProcessingOptionToElementMap()
        {
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.DeletedFiles, _workflowDeletedFilesCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Lpp, _workflowLppCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Multimedia, _workflowMultimediaCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.VideoClassification, _workflowVideoClassificationCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Images, _workflowImagesCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.ImageRating, _workflowImageRatingCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.ImageClassification, _workflowImageClassificationCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Content, _workflowContentCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Email, _workflowEmailCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Browser, _workflowBrowserCheckbox);
            _workflowProcessingOptionToElementMap.Add(WorkflowDataModel.WorkflowMediaProcessingOptions.Uac, _workflowUacCheckbox);
        }

        private void AddExhibitDetailsAndSave(ExhibitDataModel exhibitDataModel)
        {
            TypeInto(_eclExhibitNumber, exhibitDataModel.EclExhibitNumber);
            TypeInto(_policeExhibitNumber, exhibitDataModel.PoliceExhibitNumber);
            TypeInto(_description, exhibitDataModel.Description);

            var action = new Actions(_driver);
            action.MoveToElement(_saveButton).Build().Perform();
            action.Click(_saveButton).Build().Perform();
            //_saveButton.Click();
        }

        public void AddMediaDetailsAndSave(MediaDataModel mediaDataModel)
        {
            _driver.SwitchTo().Frame(0);
            var action= new Actions(_driver);
            action.MoveToElement(_saveButton).Build().Perform();
            action.Click(_saveButton).Build().Perform();
            _driver.SwitchTo().DefaultContent();
            //_saveButton.Click();
        }

        public MediaPage SelectFirstMediaOnCaseExhibitsPage()
        {
            _firstMediaLabel.Click();
            return new MediaPage(_driver);
        }

        public string GetFirstMediaLabel()
        {
            return _firstMediaLabel.Text;
        }
    }
}
