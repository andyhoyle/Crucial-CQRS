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

        internal void UserCategoryCreateEventNotify(Models.Category category)
        {
            Clients.All.userCategoryCreated(category);
        }

        internal void UserCategoryNameChangedEventNotify(Models.Category category)
        {
            Clients.All.userCategoryNameChanged(category);
        }

        internal void UserCategoryDeletedEventNotify(int id)
        {
            Clients.All.userCategoryDeleted(id);
        }
    }

    public class BroadcastUserCategoryCreatedEventHandler : 
        IEventHandler<UserCategoryCreatedEvent>, 
        IEventHandler<UserCategoryNameChangedEvent>,
        IEventHandler<UserCategoryDeletedEvent>
    {
        public void Handle(UserCategoryCreatedEvent handle)
        {
            CategoryEventBroadcaster.Instance.UserCategoryCreateEventNotify(new Models.Category { Id = handle.AggregateId, Name = handle.Name, Version = handle.Version, CreatedDate = handle.Timestamp });
        }

        public void Handle(UserCategoryNameChangedEvent handle)
        {
            CategoryEventBroadcaster.Instance.UserCategoryNameChangedEventNotify(new Models.Category { Id = handle.AggregateId, Name = handle.Name, Version = handle.Version, ModifiedDate = handle.Timestamp });
        }

        public void Handle(UserCategoryDeletedEvent handle)
        {
            CategoryEventBroadcaster.Instance.UserCategoryDeletedEventNotify(handle.AggregateId);
        }
    }
}