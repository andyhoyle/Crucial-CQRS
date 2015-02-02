using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Framework.DesignPatterns.CQRS.Utils;

namespace Crucial.Framework.DesignPatterns.CQRS.Domain
{
    public class AggregateRoot : IEventProvider
    {
        private readonly List<Event> _changes;

        public int Id { get; set; }
        public int Version { get; set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<Event>();
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
            Version = history.Last().Version;
            EventVersion = Version;
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic d = this;
            
            d.Handle(Converter.ChangeTo(@event,@event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }      
    }
}