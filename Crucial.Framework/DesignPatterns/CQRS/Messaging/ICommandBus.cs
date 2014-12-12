using System;
using Crucial.DesignPatterns.CQRS.Commands;

namespace Crucial.DesignPatterns.CQRS.Messaging
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}
