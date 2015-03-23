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
    [HubName("categoryEventHub")]
    public class CategoryEventHub : Hub
    {
        private readonly CategoryEventBroadcaster _categoryEventBroadcaster;

        public CategoryEventHub() : this(CategoryEventBroadcaster.Instance) { }

        public CategoryEventHub(CategoryEventBroadcaster broadcaster)
        {
            _categoryEventBroadcaster = broadcaster;
        }
    }

    public class CategoryEventBroadcaster
    {
        private static readonly Lazy<CategoryEventBroadcaster> _instance = new Lazy<CategoryEventBroadcaster>(() => new CategoryEventBroadcaster(GlobalHost.ConnectionManager.GetHubContext<CategoryEventHub>().Clients), true);

        public static CategoryEventBroadcaster Instance
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

        private CategoryEventBroadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        internal async Task UserCategoryCreateEventNotify(Models.Category category)
        {
            await Clients.All.userCategoryCreated(category);
        }

        internal async Task UserCategoryNameChangedEventNotify(Models.Category category)
        {
            await Clients.All.userCategoryNameChanged(category);
        }

        internal async Task UserCategoryDeletedEventNotify(int id)
        {
            await Clients.All.userCategoryDeleted(id);
        }
    }

    public class BroadcastUserCategoryCreatedEventHandler : 
        IEventHandler<UserCategoryCreatedEvent>, 
        IEventHandler<UserCategoryNameChangedEvent>,
        IEventHandler<UserCategoryDeletedEvent>
    {
        public async Task Handle(UserCategoryCreatedEvent handle)
        {
            await CategoryEventBroadcaster.Instance.UserCategoryCreateEventNotify(new Models.Category { Id = handle.AggregateId, Name = handle.Name, Version = handle.Version, CreatedDate = handle.Timestamp });
        }

        public async Task Handle(UserCategoryNameChangedEvent handle)
        {
            await CategoryEventBroadcaster.Instance.UserCategoryNameChangedEventNotify(new Models.Category { Id = handle.AggregateId, Name = handle.Name, Version = handle.Version, ModifiedDate = handle.Timestamp });
        }

        public async Task Handle(UserCategoryDeletedEvent handle)
        {
            await CategoryEventBroadcaster.Instance.UserCategoryDeletedEventNotify(handle.AggregateId);
        }
    }
}