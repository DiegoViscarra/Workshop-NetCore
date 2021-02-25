using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopLibrary.Exceptions
{
    public class WrongOperationException : Exception 
    {
        public WrongOperationException(string message) : base(message)
        {

        }
    }
}
