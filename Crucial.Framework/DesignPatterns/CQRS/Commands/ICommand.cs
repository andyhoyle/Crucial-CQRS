using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.DesignPatterns.CQRS.Commands
{
    public interface ICommand
    {
        int Id { get; }
    }
}
