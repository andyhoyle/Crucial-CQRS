using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands.Question
{
    public class QuestionDeleteCommand : Command
    {
        public QuestionDeleteCommand(int id, int version)
            : base(id, version)
        {

        }
    }
}
