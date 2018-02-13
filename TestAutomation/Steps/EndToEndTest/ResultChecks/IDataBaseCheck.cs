namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public interface IDataBaseCheck
    {
        void AssertNumberOfRows(int exhibitId, int expectedValue);
    }
}
