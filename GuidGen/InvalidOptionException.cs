using System;

namespace GuidGen
{
    internal class InvalidOptionException : Exception
    {
        public string Option { get; private set; }

        public InvalidOptionException(string option, string message)
            : base(message)
        {
            Option = option;
        }
    }
}
