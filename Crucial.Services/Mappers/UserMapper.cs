using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Services.Mappers
{
    public class UserToUserMapper : EntityMapper<Crucial.Providers.Identity.Entities.AspNetUser, Crucial.Services.ServiceEntities.User>
    {
        public override ServiceEntities.User ToServiceEntity(Providers.Identity.Entities.AspNetUser source)
        {
            var target = base.ToServiceEntity(source);
            target.Id = source.Id;
            target.Password = source.PasswordHash;
            return target;
        }

        public override Providers.Identity.Entities.AspNetUser ToProviderEntity(ServiceEntities.User source)
        {
            var target = base.ToProviderEntity(source);
            target.Id = source.Id;
            return target;
        }
    }
}
