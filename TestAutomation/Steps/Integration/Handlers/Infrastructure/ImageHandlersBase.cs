using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using EVE.Common;
using EVE.Functions.Extensions;
using EVE.Site.BLL;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.Integration.Handlers.Infrastructure
{
    public abstract class ImageHandlerStepsBase : HandlersStepsBase
    {
        protected string FileSignatureConnectionString;
        
        protected Stream ImageMemoryStream;
        
        protected int FileId;
        protected int CategoryId;
        protected int PartitionMetaDataId;

        [BeforeScenario]
        protected override void Init()
        {
            base.Init();
            
            FileSignatureConnectionString = Config.GetConnectionString(DataStore.FileSignature);
        }

        protected void SetupTestData(string resourceLocation)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceLocation);

            if (stream == null)
            {
                throw new Exception(string.Format("Unable to load resource \"{0}\"", resourceLocation));
            }

            ImageMemoryStream = new MemoryStream();
            stream.CopyTo(ImageMemoryStream);
            var fileContent = ImageMemoryStream.AsString();

            var fileExtension = Path.GetExtension(resourceLocation) ?? string.Empty;
            var fileName = Path.GetFileNameWithoutExtension(resourceLocation);
            var path = Path.GetFullPath(resourceLocation);

            var partitionMetaDataTable = new PartitionMetadataTable(ExhibitConnectionString);
            PartitionMetaDataId = partitionMetaDataTable.InsertPartitionMetadata(ExhibitId, MediaId);

            FileId = new FileMetadataTable(ExhibitConnectionString).InsertFileMetaData(ExhibitId, fileName, fileExtension.TrimStart('.'), fileContent, PartitionMetaDataId, path);
            FileContent.InsertFileContent(ExhibitId, FileId, fileContent, fileExtension);

            CategoryId = new FileCategoryTable(FileSignatureConnectionString).GetCategoryIdByFileCategoryCode("IMAGE");
        }

        protected void TearDownTestData()
        {
            var errors = new List<string>();
            
            ////Clean up what we inserted so this adds nothing to the db - this order is important.
            if(new ImageMetadataTable(ExhibitConnectionString).DeleteImageMetaData(ExhibitId, FileId) != 1)
                errors.Add(string.Format("ImageMetadata Table Clean up failure  [ExhibitId = {0}, FileId = {1}]", ExhibitId, FileId));
            if (new FileMetadataTable(ExhibitConnectionString).DeleteFileMetaData(ExhibitId, FileId) != 1)
                errors.Add(string.Format("FileMetadata Table Clean up failure [ExhibitId = {0}, FileId = {1}]", ExhibitId, FileId));
            if (new PartitionMetadataTable(ExhibitConnectionString).DeletePartionMetaData(PartitionMetaDataId) != 1)
                errors.Add(string.Format("PartitionMetaData Table Clean up failure  [ExhibitId = {0}, PartitionMetaDataId = {1}]", ExhibitId, PartitionMetaDataId));
            if(new FileContentTable(ExhibitConnectionString).DeleteFileContent(ExhibitId, FileId) != 1)
                errors.Add(string.Format("FileContent Table Clean up failure  [ExhibitId = {0}, FileId = {1}]", ExhibitId, FileId));

            //display errors
            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(errors.Count, 0, "ImageHandler clean up errors.");
        }

    }
}
