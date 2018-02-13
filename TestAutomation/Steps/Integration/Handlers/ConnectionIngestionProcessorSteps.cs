
using Common.Logging;
using EVE.Framework.Tests.Stubs.Common;
using EVE.ProcessingAgent.Contract.WorkItem;
using EVE.ProcessingAgent.Handler.Lib.ConnectionIngestion;
using EVE.Workflow;
using TechTalk.SpecFlow;

namespace TestAutomation.Steps.Integration.Handlers
{
    [Binding, Scope(Feature = "ConnectionIngestionProcessor")]
    public class ConnectionIngestionProcessorSteps
    {
        protected WorkItemBase.ExecutionOutcome HandlerOutcome;
        protected ILog Log;

        [BeforeScenario]
        protected void Init()
        {
            Log = new LoggerStub();
        }

        [Given(@"I have a UFED item")]
        public void GivenIHaveAUfedItem()
        {
        }

        [Given(@"And there is a single UFED Logical xml file")]
        public void AndASingleUFEDLogicalXmlFile()
        {
        }
        
        [Given(@"And there is a single UFED Physical xml file")]
        public void AndASingleUFEDPhysicalXmlFile()
        {
        }


        [When(@"I process the item")]
        public void WhenIProcessTheItem()
        {
            //Arrange
            var handler = new ConnectionIngestionProcessor(Log)
            {
               
            };

            //Act
            //HandlerOutcome = handler.Execute(, 0, "UFED", 1, 1);

        }

        [Then(@"The necessary work items are created")]
        public void ThenTheFileIsDetectedAsUnknown()
        {
        }

        [AfterScenario]
        private void CleanUpDb()
        {
        }
    }
}
