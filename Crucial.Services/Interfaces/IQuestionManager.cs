using System;
using System.Collections.Generic;
namespace Crucial.Services.Managers.Interfaces
{
    public interface IQuestionManager
    {
        IEnumerable<Crucial.Services.ServiceEntities.Question> GetQuestions();

        ServiceEntities.Question GetQuestion(int questionId);
    }
}
