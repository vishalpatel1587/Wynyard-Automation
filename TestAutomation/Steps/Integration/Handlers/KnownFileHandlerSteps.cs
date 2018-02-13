using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using EVE.Common;
using EVE.Functions;
using EVE.ProcessingAgent.Contract.WorkItem;
using EVE.ProcessingAgent.Handler;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Model.DataModel;
using EVE.Site.BLL;
using TestAutomation.Steps.Integration.Handlers.Infrastructure;

namespace TestAutomation.Steps.Integration.Handlers
{
    [Binding, Scope(Feature = "KnownFileHandler")]
    public class KnownFileHandlerSteps : HandlersStepsBase
    {
        private string _fileContent;
        private const string HashAlgorithm = "SHA-1";
        
        private int _fileId;
        private int _knownFileCategoryId;
        private int _knownFileId;
        private int _partitionMetaDataId;

        //Constant data that is irrelevant for the purposes of this test
        private const string FileName = "Integration Test Unknown File";
        private const string FileExtension = "jpeg";

        
        private string _fileSignatureConnectionString;
        
        [BeforeScenario]
        protected override void Init()
        {
            //handles determining Exhibit ID or creating anew.
            base.Init();

            _fileSignatureConnectionString = Config.GetConnectionString(DataStore.FileSignature);

            //is the exhibit suitable to continue testing?
            if (!ExhibitSetup && ExhibitId == 0)
                throw new Exception("Exhibit Not suitably setup");
            
        }

        [Given(@"I have a file")]
        public void HaveAFile()
        {
            //this is an image from a test exhibit. Should not exist in known file table without the test explicity adding it (should also remove on clean up)
            _fileContent = @"0x496D61676520466F726D61743A20094A5045472028455849462C204950544329090D0A45717569706D656E74204D616E7566616374757265723A200953414D53554E47090D0A43616D657261204D6F64656C3A200947542D4D38383030090D0A536F6674776172653A200941646F62652050686F746F73686F70204353332057696E646F7773090D0A5069637475726520446174653A2009323030393A30323A31302031313A35353A3436090D0A4578706F737572652054696D653A2009343030302F3439203D2038312E36333237090D0A46204E756D6265723A2009352F3133203D20302E333834363135090D0A49534F20537065656420526174696E673A2009313030090D0A457869662056657273696F6E3A200930090D0A536875747465722053706565643A2009302E313537343431090D0A41706572747572653A2009302E333632373131090D0A4272696768746E6573733A2009302E323433313839090D0A496D6167652057696474683A200933303230090D0A496D616765204865696768743A200932323139090D0A4F7269656E746174696F6E3A200931090D0A5943624372506F736974696F6E696E673A200931090D0A585265736F6C7574696F6E3A20093732303030302F3130303030090D0A595265736F6C7574696F6E3A20093732303030302F3130303030090D0A5265736F6C7574696F6E556E69743A200932090D0A4D616B653A200953414D53554E47090D0A4D6F64656C3A200947542D4D38383030090D0A4E61746976654469676573743A20093235362C3235372C3235382C3235392C3236322C3237342C3237372C3238342C3533302C3533312C3238322C3238332C3239362C3330312C3331382C3331392C3532392C3533322C3330362C3237302C3237312C3237322C3330352C3331352C33333433323B3438424437433037314233374139314544353446313441393146393742374331090D0A5861703A43726561746F72546F6F6C3A200941646F62652050686F746F73686F70204353332057696E646F7773090D0A5861703A437265617465446174653A2009323030392D30322D30315431303A35313A30382B30313A3030090D0A5861703A4D6F64696679446174653A2009323030392D30322D31305431313A35353A34362B30313A3030090D0A5861703A4D65746164617461446174653A2009323030392D30322D31305431313A35353A34362B30313A3030090D0A4578696656657273696F6E3A200930323230090D0A466C61736870697856657273696F6E3A200930313030090D0A436F6C6F7253706163653A200931090D0A506978656C5844696D656E73696F6E3A200933303230090D0A506978656C5944696D656E73696F6E3A200932323139090D0A4578706F7375726554696D653A20093234352F3230303030090D0A464E756D6265723A200932362F3130090D0A53687574746572537065656456616C75653A20093431363235372F3635353336090D0A417065727475726556616C75653A20093138303638342F3635353336090D0A4272696768746E65737356616C75653A20093236393438362F3635353336090D0A4E61746976654469676573743A200933363836342C34303936302C34303936312C33373132312C33373132322C34303936322C34303936332C33373531302C34303936342C33363836372C33363836382C33333433342C33333433372C33343835302C33343835322C33343835352C33343835362C33373337372C33373337382C33373337392C33373338302C33373338312C33373338322C33373338332C33373338342C33373338352C33373338362C33373339362C34313438332C34313438342C34313438362C34313438372C34313438382C34313439322C34313439332C34313439352C34313732382C34313732392C34313733302C34313938352C34313938362C34313938372C34313938382C34313938392C34313939302C34313939312C34313939322C34313939332C34313939342C34313939352C34313939362C34323031362C302C322C342C352C362C372C382C392C31302C31312C31322C31332C31342C31352C31362C31372C31382C32302C32322C32332C32342C32352C32362C32372C32382C33303B4438344635373443453637384541443936303832434642343430434445313935090D0A466F726D61743A2009696D6167652F6A706567090D0A50686F746F73686F703A436F6C6F724D6F64653A200933090D0A50686F746F73686F703A49434350726F66696C653A2009735247422049454336313936362D322E31090D0A5861704D4D3A496E7374616E636549443A2009757569643A4239433643363343363146374444313142393136463333423135353638333335090D0A436F6D706F6E656E7473436F6E66696775726174696F6E3A200920203B2031203B2032203B2033203B20302020090D0A49534F5370656564526174696E67733A200920203B203130302020090D0A";
            _partitionMetaDataId = new PartitionMetadataTable(ExhibitConnectionString).InsertPartitionMetadata(ExhibitId, MediaId);
            _fileId = new FileMetadataTable(ExhibitConnectionString).InsertFileMetaData(ExhibitId, FileName, FileExtension, _fileContent, _partitionMetaDataId);
            
            FileContent.InsertFileContent(ExhibitId, _fileId, _fileContent, ".txt");
        }

        [Given(@"It has an existing hash")]
        public void WithAnExistingHash()
        {
            //need to have the option since the handler can also work with files without hash
            var fileHashTable = new FileHashTable(ExhibitConnectionString);
            fileHashTable.InsertFileHash(ExhibitId, _fileId, HashAlgorithm, new MemoryStream(System.Text.Encoding.ASCII.GetBytes(_fileContent)));
        }

        [Given(@"This file is known")]
        public void FileIsKnown()
        {
            //Rather that rely on an entry in knownFileCategoryTable, insert one that we can use for testing
            const string categoryName = "Integration Testing";
            var kfcTable = new KnownFileCategoryTable(_fileSignatureConnectionString);
            _knownFileCategoryId = kfcTable.GetKnownFileCategoryId(categoryName);

            if (_knownFileCategoryId == 0)
            {
                _knownFileCategoryId = kfcTable.InsertKnownFileCategory(
                    new KnownFileCategoryModel
                    {
                        HashSetId = 0,
                        CategoryName = categoryName,
                        Vendor = "Wynyard Group - DEI",
                        Package = "Integration Test",
                        Version = "1",
                        IsAuthenticated = true,
                        IsNotable = false,
                        Initials = "NUnit",
                        NumberOfFiles = 0,
                        CategoryDescription = "Integration testing - Known File Hander",
                        DateLoaded = DateTime.Now
                    }
                    );
            }

            //generate the hash and insert a known file entry for our test file.
            var hashValue = Hashing.GetHash(new MemoryStream(Encoding.ASCII.GetBytes(_fileContent)), HashAlgorithm);
            _knownFileId = new KnownFileTable(_fileSignatureConnectionString).InsertKnownFile(FileName, hashValue, _knownFileCategoryId);
        }
        

        [When(@"I process the file")]
        public void WhenIProcessTheFile()
        {
            //Arrange
            var handler = new KnownFileHandler
            {
                Parameters = new WorkItemBase.WorkItemParameters
                {
                    RuntimeData = new Dictionary<string, string>
                    {
                        {"ExhibitId", ExhibitId.ToString(CultureInfo.InvariantCulture)},
                        {"FileId", _fileId.ToString(CultureInfo.InvariantCulture)},
                    }
                },
                WorkItemDataStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(_fileContent))
            };

            //Act
            base.HandlerOutcome = handler.Execute();
        }

        [Then(@"The file is not detected as known.")]
        public void ThenTheFileIsDetectedAsUnknown()
        {
            //load the result
            var metadata = FileMetadata.GetFileMetadata(ExhibitId, _fileId);

            //Assert
            Assert.AreEqual(base.HandlerOutcome, WorkItemBase.ExecutionOutcome.Succeeded);
            Assert.AreEqual(metadata.KnownFileId, 0);

        }

        [Then(@"The file is detected as known.")]
        public void ThenTheFileIsDetectedAsKnown()
        {
            //load the result
            var metadata = FileMetadata.GetFileMetadata(ExhibitId, _fileId);

            //Assert
            //Oddity here if the file is known and not notable it fails outcome..???
            Assert.AreEqual(base.HandlerOutcome, WorkItemBase.ExecutionOutcome.Failed);
            Assert.AreNotEqual(metadata.KnownFileId, 0);
        }

        [AfterScenario]
        private void CleanUpDb()
        {
            var errors = new List<string>();
            //Clean up what we inserted so this adds nothing to the db.
            if( new FileContentTable(ExhibitConnectionString).DeleteFileContent(ExhibitId, _fileId) != 1)
                errors.Add(string.Format("FileContent Table Clean up failure [ExhibitId = {0}, FileId = {1}]", ExhibitId, _fileId));
            
            if(new FileHashTable(ExhibitConnectionString).DeleteFileHash(ExhibitId, _fileId) != 1)
                errors.Add(string.Format("FileHash Table Clean up failure [ExhibitId = {0}, FileId = {1}]", ExhibitId, _fileId));

            if(new FileMetadataTable(ExhibitConnectionString).DeleteFileMetaData(ExhibitId, _fileId) != 1)
                errors.Add(string.Format("FileMetadata Table Clean up failure [ExhibitId = {0}, FileId = {1}]", ExhibitId, _fileId));
            
            //only attempt to clean up if we have a value.
            if (_knownFileId > 0)
            {
                if(new KnownFileTable(_fileSignatureConnectionString).DeleteKnownFile(_knownFileId) != 1)
                    errors.Add(string.Format("KnownFile Table Clean up failure [ExhibitId = {0}, FileId = {1}]", ExhibitId, _fileId));
            }

            if (_partitionMetaDataId > 0)
            {
                if(new PartitionMetadataTable(ExhibitConnectionString).DeletePartionMetaData(_partitionMetaDataId) != 1)
                    errors.Add(string.Format("PartitionMetaData Table Clean up failure  [ExhibitId = {0}, FileId = {1}]", ExhibitId, _fileId));
            }

            //display errors
            foreach (var error in errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual(errors.Count, 0, "KnownFileHander clean up errors.");

            //NOTE: This process is quite expensive (ie > 30 seconds) so we could permanently add one in our test database to facilitate this.
            //DISABLE This for the moment since this is a test and the cost to clean up is too high. Added check for existing entry to avoid adding multiple redundant data.
            //if (_knownFileCategoryId > 0)
            //{
            //    var result = new KnownFileCategoryTable(_fileSignatureConnectionString).DeleteKnownFileCategory(_knownFileCategoryId);
            //    if(result != 1)
            //        throw new Exception("Scenario clean up failed to remove Known file Category");
            //}
        }
    }
}
