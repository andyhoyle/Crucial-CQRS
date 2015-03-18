using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands.Question
{
    public class QuestionTextChangeCommand : Command
    {
        public string QuestionText { get; private set; }

        public QuestionTextChangeCommand(int id, string question, int version)
            : base(id, version)
        {
            QuestionText = question;
        }
    }
}
