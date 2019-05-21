using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.Helpers
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
