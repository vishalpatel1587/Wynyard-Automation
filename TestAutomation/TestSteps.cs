using System;
using TechTalk.SpecFlow;

namespace TestAutomation
{
    [Binding]
    public class TestSteps
    {
        [Given(@"I login")]
        public void GivenILogin()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
