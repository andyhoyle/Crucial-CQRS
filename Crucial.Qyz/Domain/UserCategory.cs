using Crucial.Framework.DesignPatterns.CQRS.Domain;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using Crucial.Qyz.Domain.Mementos;
using Crucial.Qyz.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Domain
{
    public class UserCategory : AggregateRoot,
       IHandle<UserCategoryCreatedEvent>,
       IOriginator
    {
        public string Name { get; set; }

        public UserCategory()
        {

        }

        public UserCategory(int id, string name)
        {
            ApplyChange(new UserCategoryCreatedEvent(id, name));
        }

        public void ChangeName(string name)
        {
            ApplyChange(new UserCategoryNameChangedEvent(Id, name, Version));
        }

        public BaseMemento GetMemento()
        {
            return new UserCategoryMemento(Id, Name, Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            Name = ((UserCategoryMemento)memento).Name;
            Version = memento.Version;
            Id = memento.Id;
        }
        
        public void Handle(UserCategoryCreatedEvent e)
        {
            Name = e.Name;
            Version = e.Version;
            Id = e.AggregateId;
        }

        public void Handle(UserCategoryNameChangedEvent e)
        {
            Name = e.Name;
            Version = e.Version;
            Id = e.AggregateId;
        }
    }
}
