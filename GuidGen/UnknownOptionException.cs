using System;

namespace GuidGen
{
    internal class UnknownOptionException : Exception
    {
        public string Option { get; private set; }

        public UnknownOptionException(string option)
            : this(option, string.Format("Unknown Option: {0}", option))
        {
        }

        public UnknownOptionException(string option, string message)
            : base(message)
        {
            Option = option;
        }
    }
}
