using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Commands
{
    public interface ICommand
    {
        int Id { get; }
    }
}
