using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Crucial.Framework.DesignPatterns.CQRS.Events;
using Crucial.Qyz.Events;
using System.Collections.Concurrent;
using System.Threading;
using System.Web.Hosting;
using System.Threading.Tasks;

namespace API.Controllers
{
    [HubName("questionEventHub")]
    public class QuestionEventHub : Hub
    {
        private readonly QuestionEventBroadcaster _questionEventBroadcaster;

        public QuestionEventHub() : this(QuestionEventBroadcaster.Instance) { }

        public QuestionEventHub(QuestionEventBroadcaster broadcaster)
        {
            _questionEventBroadcaster = broadcaster;
        }
    }

    public class QuestionEventBroadcaster
    {
        private static readonly Lazy<QuestionEventBroadcaster> _instance = new Lazy<QuestionEventBroadcaster>(() => new QuestionEventBroadcaster(GlobalHost.ConnectionManager.GetHubContext<QuestionEventHub>().Clients), true);

        public static QuestionEventBroadcaster Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private QuestionEventBroadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        internal async Task QuestionCreateEventNotify(Models.Question question)
        {
            await Clients.All.questionCreated(question).ConfigureAwait(false);
        }

        internal async Task QuestionTextChangedEventNotify(Models.Question question)
        {
            await Clients.All.questionTextChangedCreated(question).ConfigureAwait(false);
        }

        internal async Task QuestionDeletedEventNotify(int id)
        {
            await Clients.All.questionDeleted(id).ConfigureAwait(false);
        }
    }

    public class BroadcastQuestionCreatedEventHandler : 
        IEventHandler<QuestionCreatedEvent>,
        IEventHandler<QuestionTextChangedEvent>,
        IEventHandler<QuestionDeletedEvent>
    {
        public async Task Handle(QuestionCreatedEvent handle)
        {
            await QuestionEventBroadcaster.Instance.QuestionCreateEventNotify(new Models.Question { Id = handle.AggregateId, QuestionText = handle.Question, Version = handle.Version, CreatedDate = handle.Timestamp }).ConfigureAwait(false);
        }

        public async Task Handle(QuestionTextChangedEvent handle)
        {
            await QuestionEventBroadcaster.Instance.QuestionTextChangedEventNotify(new Models.Question { Id = handle.AggregateId, QuestionText = handle.Question, Version = handle.Version, ModifiedDate = handle.Timestamp }).ConfigureAwait(false);
        }

        public async Task Handle(QuestionDeletedEvent handle)
        {
            await QuestionEventBroadcaster.Instance.QuestionDeletedEventNotify(handle.AggregateId).ConfigureAwait(false);
        }
    }
}