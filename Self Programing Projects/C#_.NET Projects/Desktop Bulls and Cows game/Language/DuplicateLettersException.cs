using System;
using System.Runtime.Serialization;

namespace Language
{
    internal class DuplicateLettersException : Exception
    {
        public DuplicateLettersException()
            : base(String.Format("Input can't including duplicate letters"))
        {
        }
    }
}