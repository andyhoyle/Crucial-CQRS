using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands.Question
{
    //public class SingleTextAnswerQuestionCreateCommand : Command
    //{
    //    public string Question { get; private set; }
    //    public string Answer { get; private set; }

    //    public SingleTextAnswerQuestionCreateCommand(string question, string answer) : base(0, -1)
    //    {
    //        Question = question;
    //        Answer = answer;
    //    }
    //}

    public class QuestionCreateCommand : Command
    {
        public string QuestionText { get; private set; }

        public QuestionCreateCommand(string question) : base(0, -1)
        {
            QuestionText = question;
        }
    }
}
