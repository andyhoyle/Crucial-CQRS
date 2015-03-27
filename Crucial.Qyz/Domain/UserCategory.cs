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
using Crucial.Framework.Logging;

namespace Crucial.Qyz.Domain
{
    public class UserCategory : AggregateRoot,
       IHandle<UserCategoryCreatedEvent>,
       IHandle<UserCategoryNameChangedEvent>,
       IHandle<UserCategoryDeletedEvent>,
       IOriginator
    {
        #region Public Parameterless Constructor

        public UserCategory()
        {

        }

        #endregion

        #region Public properties

        public string Name { get; set; }
        public DateTime CreatedDate { get; private set; }

        #endregion

        #region Internal command implementations

        internal UserCategory(int id, string name)
        {
            ApplyChange(new UserCategoryCreatedEvent(id, name, DateTime.UtcNow));
        }

        internal void ChangeName(string name)
        {
            ApplyChange(new UserCategoryNameChangedEvent(Id, name, Version, DateTime.UtcNow));
        }

        internal void Delete()
        {
            ApplyChange(new UserCategoryDeletedEvent(Id, Version, DateTime.UtcNow));
        }

        #endregion

        #region IOriginator implementation

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

        #endregion

        #region IHandle implementation

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

        public void Handle(UserCategoryDeletedEvent e)
        {
            Id = e.AggregateId;
            Version = e.Version;
        }

        #endregion
    }
}
