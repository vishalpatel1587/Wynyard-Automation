using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Cells.Charts;
using EVE.Functions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using System.Configuration;
using System.Text;
using TechTalk.SpecFlow.Tracing;
using TestAutomation.Drivers.PageDriver.Login;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps
{
    [Binding]
    public class DeiGuiTest
    {
        private static string _pathToTestFailureScreenshotsLocalFolder =
            Path.Combine(Directory.GetCurrentDirectory(), @"..\..\TestFailureScreenshots");

        private static string _pathToHtmlWithFailureScreenshots =
         Path.Combine(Directory.GetCurrentDirectory(), @"..\..\htmlReports\FailureScreenshots\");

        private static string _pathToFailureScreenshotsInJenkinsWorkspace =
            @"../artifact/Artifacts/_Testing/AutomationTesting/TestArtifacts/TestFailureScreenshots/";

        public static IWebDriver Driver { get; private set; }

        private static readonly string DeiLoginUrl = ConfigurationManager.AppSettings.Get("loginUrl");

        [BeforeFeature("@guiTest")]
        public static void NavigateToLoginPage()
        {


            var options = new ChromeOptions();

            options.AddArgument("--start-maximized");
            //options.BinaryLocation = @"D:\ChromeBin\chrome.exe";
            Driver = new ChromeDriver(@"C:\ChromeDriver", options);
            //Driver.Manage().Window.Maximize();
            /*if (typeof(TWebDriver) == typeof(ChromeDriver))
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");

                Driver = new ChromeDriver(driverPath, options);
                new ChromeDriver()
            }*/
            Driver.Navigate().GoToUrl(DeiLoginUrl);
            Console.WriteLine(_pathToHtmlWithFailureScreenshots);
        }

        [BeforeFeature("@loginAsAdmin")]
        public static void LoginAsAdmin()
        {
            var loginDataModel = new LoginDataModel().BuildModel(LoginDataModel.DataInstance.ValidDetails);
            try
            {
                var loginPage = new LoginPage(Driver);
                loginPage.LogInWithValidDetails(loginDataModel);
            }
            catch (Exception)
            {
                Driver.Quit();
                throw;
            }
        }

        [AfterFeature("@guiTest")]
        public static void CloseBrowser()
        {
            Driver.Quit();
        }
        [Scope(Tag = "guiTest")]
        [AfterScenario]
        public static void TakeScreenshotOnScenarioFailure()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var error = ScenarioContext.Current.TestError;
                Console.WriteLine("An error ocurred:" + error.Message);
                Console.WriteLine("It was of type:" + error.GetType().Name);
                TakeScreenshot(Driver);
            }
        }

        private static void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                var fileNameBase = string.Format("error_{0}_{1}_{2}",
                                                    FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
                                                    ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier(),
                                                    DateTime.Now.ToString("HHmmss_ddMMyyyy"));

                var artifactDirectory = _pathToTestFailureScreenshotsLocalFolder;
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);

                var takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    var screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + ".png");

                    if (screenshotFilePath.Length >= 260)
                    {
                        fileNameBase = string.Format("error_{0}_{1}",
                                                        FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
                                                        DateTime.Now.ToString("HHmmss_ddMMyyyy"));
                        screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + ".png");
                    }

                    screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);

                    AppendFailureScreenshotToHtmlPage(_pathToHtmlWithFailureScreenshots, fileNameBase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while taking screenshot: {0}", e);
            }
        }

        private static void AppendFailureScreenshotToHtmlPage(string pathToHtmlWithFailureScreenshots, string fileNameBase)
        {
            var htmlWithScreenshotsPath = Path.Combine(pathToHtmlWithFailureScreenshots + "FailureScreenshots.html");

            if (!Directory.Exists(pathToHtmlWithFailureScreenshots))
            {
                Directory.CreateDirectory(pathToHtmlWithFailureScreenshots);
            }
            if (!File.Exists(htmlWithScreenshotsPath))
            {
                using (var fs = new FileStream(htmlWithScreenshotsPath, FileMode.Create))
                {
                    using (var w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine("<html>");
                        w.WriteLine("<body>");
                        w.WriteLine("<h1>Failure Screenshots</h1>");
                        w.WriteLine("<div>");
                    }
                }
            }
            //Uncomment for running locally and use localScreenshotFilePath instead of jenkinsScreenshotFilePath
            // var localScreenshotFilePath = Path.Combine(_pathToTestFailureScreenshotsLocalFolder, fileNameBase + ".png");
            //running on Jenkins
            var jenkinsScreenshotFilePath =
                      Path.Combine(
                          _pathToFailureScreenshotsInJenkinsWorkspace,
                          fileNameBase + ".png");
            using (var fs = new FileStream(htmlWithScreenshotsPath, FileMode.Append))
            {
                using (var w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<a href=\"" + jenkinsScreenshotFilePath + "\">");
                    w.WriteLine("<img src=\"" + jenkinsScreenshotFilePath + "\" width=\"250\" height=\"150\" border=\"3\"/>");
                    w.WriteLine("</a>");
                    w.WriteLine("<p>" + fileNameBase + "</p>");
                    w.WriteLine(Environment.NewLine);
                }
            }
        }
    }
}
