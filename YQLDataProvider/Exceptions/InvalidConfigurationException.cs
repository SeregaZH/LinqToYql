using System;

namespace YQLDataProvider.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string message)
            : base(message)
        {

        }
    }
}
