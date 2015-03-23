using Crucial.Framework.DesignPatterns.Repository.Async;
using Crucial.Providers.Questions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Providers.Questions
{
    public interface IQuestionRepositoryAsync : 
        Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<Entities.Question>,
        IUpdateRepositoryAsync<Entities.Question>,
        ICreateRepositoryAsync<Entities.Question, Entities.Question>,
        IDeleteRepositoryAsync<Entities.Question>,
        Framework.IoC.IAutoRegister
    {
    }

    public class QuestionRepositoryAsync : Crucial.Framework.Data.EntityFramework.Async.BaseRepository<IQuestionsDbContext, Entities.Question, Entities.Question>, IQuestionRepositoryAsync
    {
    }
}
