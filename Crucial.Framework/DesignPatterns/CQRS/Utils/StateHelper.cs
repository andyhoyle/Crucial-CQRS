using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Framework.DesignPatterns.CQRS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public class StateHelper : Crucial.Framework.DesignPatterns.CQRS.Utils.IStateHelper
    {
        IEventStorage _eventStore;
        IEventBus _eventBus;

        public StateHelper(IEventStorage eventStore, IEventBus eventBus)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
        }

        public async Task RestoreState()
        {
            var events = await _eventStore.GetAllEvents().ConfigureAwait(false);
            _eventBus.Replay(events);
        }
    }
}
