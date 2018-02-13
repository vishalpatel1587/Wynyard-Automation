using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using EVE.Common;
using EVE.Functions.Extensions;
using EVE.ProcessingAgent.Contract.WorkItem;
using EVE.ProcessingAgent.Handler;
using EVE.ProcessingAgent.Handler.Lib.XmlTransformation;
using EVE.Site.BLL;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TestAutomation.Steps.Integration.Handlers.Lib.XmlTransformation
{
    [Binding]
    public class XmlTransformationSteps
    {
        
        private ExtractType _currentExtractType;
        private string _currentExtractTypeName = null;
        private string _currentSectionName = null;
        private int _currentTransformId = 0;
        private Type _currentCommunicationType = null;
        private Object _currentTypedObject = null;

        private static Dictionary<string, List<Tuple<string, string, string>>> _xsltDictionary = new Dictionary<string, List<Tuple<string, string, string>>>();
        private static Dictionary<string, string> _xmlDictionary = new Dictionary<string, string>(); 
        

        [Given(@"an extract type (.*) for a certain Ufed file")]
        public void GivenAnExtractTypeForACertainUfedFile(string extractTypeDescription)
        { 
            _currentExtractType = (ExtractType)Enum.Parse(typeof(ExtractType), extractTypeDescription);
            _currentExtractTypeName = Enum.GetName(typeof(ExtractType), _currentExtractType);
        }

        [Given(@"a section (.*) is identified")]
        public void GivenASectionContactsIsIdentified(string sectionName)
        {
            _currentSectionName = sectionName;
        }

        [Given(@"that section relates to a specific transformation")]
        public void GivenThatSectionRelatesToASpecificTransformation()
        {
            var connectionSectionData = ConnectionSection.GetConnectionSection_NameAndType(_currentSectionName, (int) _currentExtractType);
            if(connectionSectionData == null)
                throw new InvalidDataException(String.Format("Connection Section Data doesn't have any entry for Name='{0}' and Type='{1}'", _currentSectionName, _currentExtractType));

            var sectionData = connectionSectionData.FirstOrDefault();
            
            if (sectionData == null) throw new InvalidDataException(String.Format("Section Data doesn't have any entry for Name='{0}' and Type='{1}'", _currentSectionName, _currentExtractType));

            var patternData = ConnectionPattern.GetConnectionPattern_Section(sectionData.ConnectionSectionId).FirstOrDefault();
            if (patternData == null) throw new InvalidDataException(String.Format("Pattern Data doesn't have any entry for SectionId='{2}'. Name='{0}'. Type='{1}'", _currentSectionName, _currentExtractType, sectionData.ConnectionSectionId));

            if (!patternData.TransformId.HasValue) throw new InvalidDataException(String.Format("Pattern Data doens't have a TransformId for PatternId='{3}'. SectionId='{2}'. Name='{0}'. Type='{1}'", _currentSectionName, _currentExtractType, sectionData.ConnectionSectionId, patternData.ConnectionPatternId));

            _currentCommunicationType = Type.GetType(sectionData.CommunicationType);
            _currentTransformId = patternData.TransformId.Value;
        }

        [Given(@"a valid Xml document is provided")]
        public void GivenAValidXmlDocumentIsProvided()
        {
            var basePath = @"Resources\Communication\";
            if (_xmlDictionary.ContainsKey(_currentExtractTypeName))
                return;

            var xmlFilePath = basePath + _currentExtractTypeName + ".xml";
            using (var xmlFile = File.Open(xmlFilePath, FileMode.Open))
            {
                using (var reader = new StreamReader(xmlFile))
                {
                    _xmlDictionary.Add(_currentExtractTypeName, reader.ReadToEnd());
                }
            }
        }

        [Given(@"a valid Xlst document is provided")]
        public void GivenAValidXlstDocumentIsProvided()
        {
            if (_xsltDictionary.ContainsKey(_currentExtractTypeName) 
                && _xsltDictionary[_currentExtractTypeName].Find(tuple => (tuple.Item1 == _currentSectionName && tuple.Item2 == _currentCommunicationType.ToString())) != null)
                return;

            var transformData = ConnectionTransform.GetConnectionTransform(_currentTransformId);
            List<Tuple<string, string, string>> list = null;

            if (!_xsltDictionary.ContainsKey(_currentExtractTypeName))
            {
                list = new List<Tuple<string, string, string>>();
                _xsltDictionary.Add(_currentExtractTypeName, list);
            }
            else
            {
                list = _xsltDictionary[_currentExtractTypeName];
            }

            list.Add(new Tuple<string, string, string>(_currentSectionName, _currentCommunicationType.ToString(), transformData.Transform));
        }

        [When(@"the XmlTransformation Handler is called")]
        public void WhenTheXmlTransformationHandlerIsCalled()
        {
            var handler = new Transformation();
            var xslt = _xsltDictionary[_currentExtractTypeName].Find(tuple => tuple.Item1 == _currentSectionName && tuple.Item2 == _currentCommunicationType.ToString()).Item3;
            var serializedObject = handler.Transform(xslt, _xmlDictionary[_currentExtractTypeName], _currentCommunicationType);

            using (var objectOutput = new MemoryStream(serializedObject))
            {
                var binaryFormatter = new BinaryFormatter();
                var obj = binaryFormatter.Deserialize(objectOutput);
                _currentTypedObject = Convert.ChangeType(obj, _currentCommunicationType);
            }
        }

        [Then(@"a binary file with a serialized object is saved in a given path")]
        public void ThenABinaryFileWithASerializedObjectIsSavedInAGivenPath()
        {
            switch (_currentCommunicationType.ToString())
            {
                case "EVE.Serialization.Communication.Contacts":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.Contacts>(_currentTypedObject, "object type is wrong");
                    break;
                case "EVE.Serialization.Communication.PhoneCalls":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.PhoneCalls>(_currentTypedObject, "object type is wrong");
                    break;
                case "EVE.Serialization.Communication.SmsMessages":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.SmsMessages>(_currentTypedObject, "object type is wrong");
                    break;
                case "EVE.Serialization.Communication.MmsMessages":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.MmsMessages>(_currentTypedObject, "object type is wrong");
                    break;
                case "EVE.Serialization.Communication.MediaFiles":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.MediaFiles>(_currentTypedObject, "object type is wrong");
                    break;
                case "EVE.Serialization.Communication.CalendarEvents":
                    Assert.IsInstanceOf<EVE.Serialization.Communication.CalendarEvents>(_currentTypedObject, "object type is wrong");
                    break;
                default:
                    Assert.Fail(String.Format("{0} not supported", _currentCommunicationType.ToString()));
                    break;
            }
        }
    }
}
