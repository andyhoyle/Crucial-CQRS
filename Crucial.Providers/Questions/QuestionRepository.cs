using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.DesignPatterns.Repository;
using Crucial.Providers.Questions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Providers.Questions
{
    public interface IQuestionRepository : IQueryableRepository<Entities.Question>,
                                            IUpdateRepository<Entities.Question>,
                                            ICreateRepository<Entities.Question, Entities.Question>,
                                            IDeleteRepository<Entities.Question>,
                                            Framework.IoC.IAutoRegister
    {

    }

    public class QuestionRepository : BaseRepository<IQuestionsDbContext, Entities.Question, Entities.Question>, IQuestionRepository
    {
    }
}
