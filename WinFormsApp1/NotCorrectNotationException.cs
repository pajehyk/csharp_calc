using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class NotCorrectNotationException : Exception
    {
        public NotCorrectNotationException()
        {

        }

        public NotCorrectNotationException(String message)
            : base(message)
        {

        } 
    }
}
