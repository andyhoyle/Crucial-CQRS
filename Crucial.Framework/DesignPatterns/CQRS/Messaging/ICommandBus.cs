using System;
using Crucial.Framework.DesignPatterns.CQRS.Commands;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}
