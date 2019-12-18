using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
