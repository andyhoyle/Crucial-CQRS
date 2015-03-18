using Crucial.Framework.Entities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Services.Mappers
{
    public class QuestionToQuestionMapper : EntityMapper<Crucial.Providers.Questions.Entities.Question, Crucial.Services.ServiceEntities.Question>
    {
        public override Providers.Questions.Entities.Question ToProviderEntity(ServiceEntities.Question source)
        {
            var target = base.ToProviderEntity(source);
            target.UserId = 100;
            return target;
        }

        public override ServiceEntities.Question ToServiceEntity(Providers.Questions.Entities.Question source)
        {
            var target = base.ToServiceEntity(source);
            target.QuestionText = source.Text;
            return target;
        }
    }
}
