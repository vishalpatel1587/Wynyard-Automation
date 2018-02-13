using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EVE.Common;
using EVE.Functions;
using EVE.ProcessingAgent.Contract.WorkItem;
using EVE.ProcessingAgent.Handler;
using EVE.Site.BLL;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Steps.Integration.Handlers.Infrastructure;

namespace TestAutomation.Steps.Integration.Handlers
{
    [Binding, Scope(Feature = "ImageScalingHandler")]
    public class ImageScalingHandlerSteps : ImageHandlerStepsBase
    {
        //Image resources are currently embedded, this can and probably should transition to a test db.
        [Given(@"I have an image file")]
        public void HaveAnImageFile()
        {
            const string resourceLocation = "TestAutomation.Resources.ImageScalingTestImage.jpg";
            SetupTestData(resourceLocation);
        }

        [Given(@"I have a non image file")]
        public void HaveANonImageFile()
        {
            const string resourceLocation = "TestAutomation.Resources.ImageScalingFailureTestData.txt";
            SetupTestData(resourceLocation);
        }

        [When(@"I scale the file")]
        public void WhenIProcessTheFile()
        {
            //Arrange

            var handler = new ImageScalingHandler
            {
                Parameters = new WorkItemBase.WorkItemParameters
                {
                    RuntimeData = new Dictionary<string, string>
                    {
                        {"ExhibitId", ExhibitId.ToString(CultureInfo.InvariantCulture)},
                        {"FileId", FileId.ToString(CultureInfo.InvariantCulture)},
                        {"FileCategoryId", CategoryId.ToString(CultureInfo.InvariantCulture)},
                    }
                },
                WorkItemDataStream = ImageMemoryStream
            };

            //Act
            using (new Performance("ImageScaling"))
            {
                HandlerOutcome = handler.Execute();    
            }
            
        }

        [Then(@"A thumbnail image is generated")]
        public void ThenTheThumbnailIsGenerated()
        {
            //load the result
            var metadata = ImageMetadata.GetImageMetadata(ExhibitId, FileId, true, true);
            var fileWarning = FileWarning.GetFileWarning_File(ExhibitId, FileId); //, FileWarningType.ImageFormatNotRecognised

            //Assert
            Assert.AreEqual(HandlerOutcome, WorkItemBase.ExecutionOutcome.Succeeded);

            Assert.AreNotEqual(metadata, null);
            Assert.AreNotEqual(metadata.Thumbnail, null);
            Assert.AreEqual(fileWarning, null);
        }

        [Then(@"No thumbnail image is generated")]
        public void ThenNoThumbnailIsGenerated()
        {
            //load the result
            var metadata = ImageMetadata.GetImageMetadata(ExhibitId, FileId, true, true);
            var fileWarning = FileWarning.GetFileWarning_File(ExhibitId, FileId);

            //Assert
            Assert.AreEqual(HandlerOutcome, WorkItemBase.ExecutionOutcome.Succeeded);
            Assert.AreNotEqual(metadata, null);
            Assert.AreEqual(metadata.Thumbnail, null);
            Assert.AreNotEqual(fileWarning, null);
            Assert.AreEqual(fileWarning.First().FileWarningType, FileWarningType.ImageFormatNotRecognised.ToString());
        }
        
        [AfterScenario]
        private void CleanUpDb()
        {
            ////Clean up what we inserted so this adds nothing to the db.
            new FileWarningTable(ExhibitConnectionString).DeleteFileWarning(ExhibitId, FileId);

            base.TearDownTestData();
        }
    }
}
