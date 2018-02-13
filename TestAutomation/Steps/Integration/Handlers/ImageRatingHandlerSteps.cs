using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using EVE.Data;
using EVE.Site.BLL;
using EVE.Functions;
using EVE.Functions.Extensions;
using EVE.ProcessingAgent.Contract.WorkItem;
using EVE.ProcessingAgent.Handler;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Steps.Integration.Handlers.Infrastructure;

namespace TestAutomation.Steps.Integration.Handlers
{
    [Binding, Scope(Feature="ImageRatingHandler")]
    public class ImageRatingHandlerSteps : ImageHandlerStepsBase
    {   
        //before scenario in base class
        
        //Image resources are currently embedded, this can and probably should transition to a test db.
        [Given(@"I have an image file of non interest")]
        public void HaveAnImageFileOfNonInterest()
        {
            const string resourceLocation = "TestAutomation.Resources.ImageScalingTestImage.jpg";
            SetUpImageForRating(resourceLocation);
        }

        [Given(@"I have an image file of interest")]
        public void HaveAnImageFileOfInterest()
        {
            const string resourceLocation = "TestAutomation.Resources.ImageRatingPositiveSample1.jpg";
            SetUpImageForRating(resourceLocation);
        }

        private void SetUpImageForRating(string resourceLocation)
        {
            SetupTestData(resourceLocation);

            //need to insert into imagemetadata now so Image Rating 
            var image = new Bitmap(ImageMemoryStream);
            var imageAsByteArray = ImageMemoryStream.ToByteArray();
            new ImageMetadataTable(ExhibitConnectionString).InsertImageMetaData(ExhibitId, FileId, image.Width, image.Height, imageAsByteArray, imageAsByteArray);
        }

        [When(@"I rate the file")]
        public void WhenIProcessTheFile()
        {

            ImageMemoryStream.Seek(0, SeekOrigin.Begin);
            const string classifierConfigFile = "svm_config.xml";

            //Arrange
            var handler = new ImageRatingHandler
            {
                Parameters = new WorkItemBase.WorkItemParameters
                {
                    RuntimeData = new Dictionary<string, string>
                    {
                        {"ExhibitId", ExhibitId.ToString(CultureInfo.InvariantCulture)},
                        {"FileId", FileId.ToString(CultureInfo.InvariantCulture)},
                        {"FileCategoryId", CategoryId.ToString(CultureInfo.InvariantCulture)},
                        {"ClassifierConfigFile", Path.GetFullPath(classifierConfigFile)}
                    }
                },
                WorkItemDataStream = ImageMemoryStream
            };

            //Act
            using (new Performance("ImageRating"))
            {   
                HandlerOutcome = handler.Execute();
            }
        }

        [Then(@"An image rating is generated")]
        public void ThenTheImageRatingIsGenerated()
        {
            //load the result
            var fileWarning = FileWarning.GetFileWarning_File(ExhibitId, FileId); 
            
            //Assert
            Assert.AreEqual(HandlerOutcome, WorkItemBase.ExecutionOutcome.Succeeded);
            Assert.AreEqual(fileWarning, null);
        }

        [Then(@"Image is not identified as of interest")]
        public void ThenImageIsNotIdentifiedAsOfInterest()
        {
            var metadata = ImageMetadata.GetImageMetadata(ExhibitId, FileId, true, true);
            Assert.AreNotEqual(metadata, null);
            Assert.LessOrEqual(metadata.PornProbability, 0.0); 
        }

        [Then(@"Image is identified as of interest")]
        public void ThenImageIsIdentifiedAsOfInterest()
        {
            var metadata = ImageMetadata.GetImageMetadata(ExhibitId, FileId, true, true);
            Assert.AreNotEqual(metadata, null);
            //show that there is a non zero probability of being of interest.
            Assert.GreaterOrEqual(metadata.PornProbability, 20.0);
        }
    }
}
