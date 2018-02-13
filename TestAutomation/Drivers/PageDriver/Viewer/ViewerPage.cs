using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EVE.Functions.Extensions;
using Microsoft.Web.Services3.Referral;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Exceptions;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Viewer
{
    public class ViewerPage : PageBase
    {

        private IWebDriver _driver;
        private static string _parentHandle;
        private static string _childHandle;
        private static string _childHandle2;


        [FindsBy(How = How.Id, Using = "FileDetailsControl_FileTable")] 
        private IWebElement _viewerHeaderTable;

        [FindsBy(How = How.ClassName, Using = "viewer_table_results")] 
        private IWebElement _viewerTableResult;

        [FindsBy(How = How.Id, Using = "FileDetailsControl_TableCell3")] 
        private IWebElement _exhibitNumber;

        [FindsBy(How = How.Id, Using = "FileDetailsControl_FileTable")]
        private IWebElement _viewerTable;     
        
        public ViewerPage(IWebDriver driver, string childHandle, string parentHandle)
        {

            _driver = driver;
            _childHandle = childHandle;
            _parentHandle = parentHandle;

            PageFactory.InitElements(_driver, this);
            _driver.SwitchTo().Window(_childHandle);
            try
            {
                WaitForElementDisplayed(_driver, _viewerHeaderTable);

            }
            catch (Exception e)
            {
                throw new ElementNotDisplayedException(
                    "Search page is expected to be shown with the Viewer Header Div (id='" +
                    _viewerHeaderTable.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }
        }

        public bool FindTextinViewer(string expectedText)
        {
            try
            {
                Assert.True(_viewerTableResult.Text.Contains(expectedText));
                return true;
            }
            catch (Exception e)
            {

                throw new Exception("The viewer that has been opened does not contain the " + expectedText +
                                    " Exception raised would be:" + e);
            }

        }

        public bool FindTextinViewerHeader(string expectedText)
        {
            try
            {
                Assert.True(_viewerHeaderTable.Text.Contains(expectedText));
                return true;
            }
            catch (Exception e)
            {
                CloseViewer();
                SwitchToParent();
                throw new Exception("The viewer that has been opened does not contain the " + expectedText +
                                    " in the header part. Exception raised would be:" + e);
                
            }

        }

        public void IsViewerOpen()
        {
            try
            {
                Assert.True(_exhibitNumber.Displayed);
            }
            catch (Exception exception)
            {

                throw new Exception(@"The viewer has not been opened. Exception raised:" + exception);
            }

        }

        public void SwitchToParent()
        {

            _driver.SwitchTo().Window(_parentHandle);
        }

        public void SwitchToChild()
        {
            _driver.SwitchTo().Window(_childHandle);
        }

        public void SwitchToChild2()
        {
            _driver.SwitchTo().Window(_childHandle2);
        }

        public void CloseViewer()
        {
            _driver.SwitchTo().Window(_childHandle).Close();
           
        }

        
        public bool ValidateImageVisible()
        {
           try
           {
               _driver.SwitchTo().Frame(0);
                IWebElement element = _driver.FindElement(By.TagName("img"));
               
                //Validates where the image has been downloaded in the viewer and is actually visible to the user.
                HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(element.GetAttribute("src"));
                string lsResponse = string.Empty;
                using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
                {
                    if (HttpStatusCode.OK == lxResponse.StatusCode)
                    {
                        Console.WriteLine(@"The image has been downloaded {0}", element.GetAttribute("src"));
                    }
                }
               
                return true;
            }
            catch (Exception exception)
            {
                CloseViewer();
                throw new Exception("The image is not visible in the viewer.  "+ exception);
            }
         
        }

        public void OpenMMSAttachment(string attachmentName)
        {
            try
            {
                WaitForElementDisplayed(_driver, _viewerTable);
                _driver.FindElement(By.LinkText(attachmentName)).Click();
                 foreach (string windowhandle in _driver.WindowHandles)
                 {
                     if (windowhandle != _childHandle && windowhandle!=_parentHandle)
                          _childHandle2 = windowhandle;
                     else
                     {
                         continue;
                     }

                 }
       
                SwitchToChild2();
                ValidateImageVisible();
                _driver.Close();

            }
            catch (Exception exception)
            {
                CloseViewer();
                throw new Exception("Unable to find Attachment link in the viewer. Exception:"+exception);
            }
            
        }

        public bool ValidateVideoPlayable()
        {
            try
            {
                
                 //Assert.True(_driver.FindElement(By.TagName("video")).Displayed);
                Assert.True(_driver.FindElement(By.Id("VideoPlayerControl_MediaPlayerControl_Toolbar_PlayButton")).Displayed);
                 return true;

            }
            catch (Exception e)
            {
                CloseViewer();
                SwitchToParent();
                throw new Exception("Video is not playable. Exception:"+e);
                
            }

        }


    }
}
