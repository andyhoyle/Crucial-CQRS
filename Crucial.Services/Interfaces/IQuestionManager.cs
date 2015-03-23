using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Crucial.Services.Managers.Interfaces
{
    public interface IQuestionManager
    {
        Task<IEnumerable<Crucial.Services.ServiceEntities.Question>> GetQuestions();

        Task<ServiceEntities.Question> GetQuestion(int questionId);
    }
}
