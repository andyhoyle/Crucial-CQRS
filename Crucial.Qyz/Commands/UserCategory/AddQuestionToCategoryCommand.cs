using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands.UserCategory
{
    public class AddQuestionToCategoryCommand : Command
    {
        public int QuestionId { get; private set; }

        public AddQuestionToCategoryCommand(int questionId, int id, int version)
            : base(id, version)
        {
            QuestionId = questionId;
        }
    }
}
