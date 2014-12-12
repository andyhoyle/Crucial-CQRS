using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.DesignPatterns.CQRS.Domain;

namespace Crucial.DesignPatterns.CQRS.Storage
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}
