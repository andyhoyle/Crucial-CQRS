using System;
using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Messaging
{
    public interface ICommandBus
    {
        Task Send<T>(T command) where T : Command;
    }
}
