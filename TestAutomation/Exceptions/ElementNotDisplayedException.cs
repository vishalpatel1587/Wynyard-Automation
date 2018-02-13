using System;

namespace TestAutomation.Exceptions
{
    class ElementNotDisplayedException : Exception
    {
        public ElementNotDisplayedException()
        {
        }

        public ElementNotDisplayedException(string message)
            : base(message)
        {
        }
    }
}
