using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Services.Mappers
{
    public class CategoryToCategoryMapper : EntityMapper<Crucial.Providers.Questions.Entities.Category, Crucial.Services.ServiceEntities.Category>
    {
        public override Providers.Questions.Entities.Category ToProviderEntity(ServiceEntities.Category source)
        {
            var target = base.ToProviderEntity(source);
            target.UserId = 100;
            return target;
        }
    }
}
