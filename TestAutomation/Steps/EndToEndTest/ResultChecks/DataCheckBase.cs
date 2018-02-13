namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    abstract public class DataCheckBase
    {
        protected string ConnectionString;

        protected DataCheckBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
