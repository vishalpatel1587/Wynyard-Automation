namespace TestAutomation.Drivers.DBDriver.Infrastructure
{
    public class Table
    {
        protected string ConnectionString;

        public Table(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
