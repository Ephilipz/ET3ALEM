using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.CustomExceptions
{
    public class CustomExceptionBase : Exception
    {
        public CustomExceptionBase()
        {
        }

        public CustomExceptionBase(string message) : base(message)
        {
        }

        public CustomExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
