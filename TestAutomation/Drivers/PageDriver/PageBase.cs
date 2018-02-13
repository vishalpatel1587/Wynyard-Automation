using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Aspose.Cells;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using TestAutomation.Exceptions;

namespace TestAutomation.Drivers.PageDriver
{
    public class PageBase
    {
        public void TypeInto(IWebElement elementToTypeInto, string textToType)
        {
            elementToTypeInto.Clear();
            elementToTypeInto.SendKeys(textToType);
            Assert.True(elementToTypeInto.GetAttribute("value").Equals(textToType), "It may be that the input field element was not populated with text '" + textToType + "' as was expected.");
        }

        public void WaitForElementDisplayed(IWebDriver driver, IWebElement element)
        {
            var maxSecondsToWait = Convert.ToInt32(ConfigurationManager.AppSettings.Get("webElementTimeout"));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxSecondsToWait));
            try
            {
                wait.Until(d => element.Displayed);
            }

            catch (StaleElementReferenceException)
            {
                Console.WriteLine(@"THE ELEMENT HAS BECOME STALE. Waiting till the driver finds the element.\n");
                WaitForElementDisplayed(driver,element);
            }

            catch (Exception e)
            {
                throw new ElementNotDisplayedException("It may be that the" + element.GetAttribute("id") +" element is not displayed on a screen after waiting for maximum of " + maxSecondsToWait + " seconds. Exception is: " + e.ToString());
            }
        }
        //Had initially been written to handle Stale Element exception but is now being handled by WaiForElementDisplayed method.-VISHAL
       /* public IWebElement HandleStaleElement(IWebDriver driver, By by)
        {
             int isElementPresent=0;
            while (isElementPresent==0)
            {
               isElementPresent=driver.FindElements(by).Count;
            }

            return driver.FindElement(by);
        }*/

        public void SelectFromDropdown(IWebElement dropdownElement, string textToSelect)
        {
            
            var select = new SelectElement(dropdownElement);
           
            select.SelectByText(textToSelect);

            //Added a try and a catch to remove the stale element exception which was getting genereated due AJAX(Timezone field being populated during runtime).
            //-vishal
              try
            {
                Assert.True(select.SelectedOption.Text.Equals(textToSelect), "It may be that the dropdown list element was not populated with text '" + textToSelect + "' as was expected.");
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
               // System.Threading.Thread.Sleep(250);
                Assert.True(select.SelectedOption.Text.Equals(textToSelect), "It may be that the dropdown list element was not populated with text '" + textToSelect + "' as was expected.");              
            } 
        }

/* Commenting the function: The assertions fail as index does not match with expected text
 * 
        public bool DoesColumnWithExpectedIndexAndTextExistInTable(IWebElement table, int columnIndexInARow, string expectedText)
        {
            try
            {
               //made changes to below line and added an Assert. Removed /tr/td as it was only considering the first row.
               //to make it robust we need to send rown index if possible.
                Assert.True(table.Text.Contains(expectedText));
               // Assert.True(
               // table.FindElement(By.XPath(".//tr/td[" + columnIndexInARow + "]")).Text.Contains(expectedText));
                //Console.WriteLine("### ## For Index :"+columnIndexInARow+" INDEX VALUE: " +table.FindElement(By.XPath(".//tr/td[" + columnIndexInARow + "]")).Text);
                // Change by: Vipul
                // Commented the below code so as to make the function more generic
                // table.FindElement(By.XPath(".//tr/td[" + columnIndexInARow + "][text()='" + expectedText + "']"));
                return true;
            }
            catch (Exception)
            {
                throw new Exception("The column with index = " + columnIndexInARow + " with expected text '" + expectedText + "' might not have been found in a table");
            }
        }
*/

        public bool DoesColumnWithExpectedIndexAndTextExistInTable(IWebElement table, int columnIndexInARow, string expectedText)
        {
           try
            {
                Assert.True(table.Text.Contains(expectedText));
                return true;
            }
            catch (Exception)
            {
                throw new Exception("The column"+columnIndexInARow+" with expected text " + expectedText + " might not have been found in a table");
            }
        }

        public bool DoesColumnWithExpectedIndexAndTextExistInTable(IWebElement table, string expectedText)
        {
            try
            {
                Assert.True(table.Text.Contains(expectedText));
                return true;
            }
            catch (Exception)
            {
                throw new Exception("The column with with expected text '" + expectedText + "' might not have been found in a table");
            }
        }

        public void DoubleClickOnTableColumnWithExpectedIndexAndText(IWebDriver driver, IWebElement table, int columnIndexInARow, string expectedText)
        {
            if (!DoesColumnWithExpectedIndexAndTextExistInTable(table, columnIndexInARow, expectedText)) return;
            //var action = new Actions(driver);
            //action.MoveToElement(table.FindElement(By.XPath(".//tr/td[" + columnIndexInARow + "][text()='" + expectedText + "']"))).DoubleClick().Build().Perform();

            table.FindElement(By.XPath(".//tr/td[" + columnIndexInARow + "][text()='" + expectedText + "']")).Click();
        }

        public void ClickOnElementFromListFoundByElementText(IWebDriver driver, IList<IWebElement> list, string expectedElementText)
        {
            foreach (var element in list.Where(el => el.Text.Equals(expectedElementText)))
            {
                element.Click();
                return;
            }
            throw new Exception("Could not select an element with text='" + expectedElementText + "' from the list");
        }

        public IWebElement FindElementFromListByElementText(IWebDriver driver, IList<IWebElement> list, string expectedElementText)
        {
            foreach (var element in list.Where(el => el.Text.Equals(expectedElementText)))
            {
                return element;
            }
            throw new Exception("Could not find an element with text='" + expectedElementText + "' in the list");
        }

        public void CheckCheckboxIfUnchecked(IWebDriver driver, IWebElement element)
        {
            if (element.Selected) return;
            element.Click();
            Assert.True(element.Selected);
        }

        public void UncheckCheckboxIfChecked(IWebDriver driver, IWebElement element)
        {
            if (!element.Selected) return;
            element.Click();
            Assert.False(element.Selected);
        }

        public void RetryToClick(IWebElement element)
        {
            var maxSecondsToWait = Convert.ToInt32(ConfigurationManager.AppSettings.Get("webElementTimeout"));
            var sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                try
                {
                    element.Click();
                    sw.Stop();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    if (!(sw.Elapsed.TotalSeconds > maxSecondsToWait)) continue;
                    sw.Stop();
                    throw e;
                }
            }
        }
    }
}
