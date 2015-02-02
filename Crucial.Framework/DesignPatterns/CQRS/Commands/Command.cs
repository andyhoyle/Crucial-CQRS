using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public int Id { get; private set; }
        public int Version { get; private set; }
        public Command(int id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
