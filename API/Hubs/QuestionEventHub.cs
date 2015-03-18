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

        internal void QuestionCreateEventNotify(Models.Question question)
        {
            Clients.All.questionCreated(question);
        }

        internal void QuestionTextChangedEventNotify(Models.Question question)
        {
            Clients.All.questionTextChangedCreated(question);
        }

        internal void QuestionDeletedEventNotify(int id)
        {
            Clients.All.questionDeleted(id);
        }
    }

    public class BroadcastQuestionCreatedEventHandler : 
        IEventHandler<QuestionCreatedEvent>,
        IEventHandler<QuestionTextChangedEvent>,
        IEventHandler<QuestionDeletedEvent>
    {
        public void Handle(QuestionCreatedEvent handle)
        {
            QuestionEventBroadcaster.Instance.QuestionCreateEventNotify(new Models.Question { Id = handle.AggregateId, QuestionText = handle.Question, Version = handle.Version, CreatedDate = handle.Timestamp });
        }

        public void Handle(QuestionTextChangedEvent handle)
        {
            QuestionEventBroadcaster.Instance.QuestionTextChangedEventNotify(new Models.Question { Id = handle.AggregateId, QuestionText = handle.Question, Version = handle.Version, ModifiedDate = handle.Timestamp });
        }

        public void Handle(QuestionDeletedEvent handle)
        {
            QuestionEventBroadcaster.Instance.QuestionDeletedEventNotify(handle.AggregateId);
        }
    }
}