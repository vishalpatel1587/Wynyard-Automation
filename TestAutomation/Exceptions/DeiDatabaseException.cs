using System;

namespace TestAutomation.Exceptions
{
    class DeiDatabaseException : Exception
    {
        public DeiDatabaseException()
        {
        }

        public DeiDatabaseException(string message)
            : base(message)
        {
        }
    }
}
