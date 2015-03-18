using Crucial.Framework.DesignPatterns.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Domain.Mementos
{
    [Serializable]
    public class QuestionMemento : BaseMemento
    {
        public string QuestionText { get; set; }

        public QuestionMemento(int id, string question, int version)
        {
            Id = id;
            QuestionText = question;
            Version = version;
        }
    }
}
