using System;

namespace Language
{
    internal class UndefinedLetterException : Exception
    {
        public UndefinedLetterException(char i_InvalidChar)
            : base(String.Format("{0} is an invalid letter", i_InvalidChar))
        {
        }
    }
}
