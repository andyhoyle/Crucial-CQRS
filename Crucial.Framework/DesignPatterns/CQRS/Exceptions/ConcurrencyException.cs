using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.DesignPatterns.CQRS.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message) { }
    }
}
