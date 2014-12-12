using System;

namespace Crucial.DesignPatterns.CQRS.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException(string message) : base(message) { }
    }
}
