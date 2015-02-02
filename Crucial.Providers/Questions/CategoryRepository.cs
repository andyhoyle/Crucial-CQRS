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
    public class QuestionContextProvider : Framework.Data.EntityFramework.ContextProvider<QuestionsDbContext>, IQuestionContextProvider
    {
    }

    public interface IQuestionContextProvider : Crucial.Framework.Data.EntityFramework.IDatabaseContextProvider
    {

    }

    public interface ICategoryRepository : IQueryableRepository<Entities.Category>,
                                            IUpdateRepository<Entities.Category>,
                                            ICreateRepository<Entities.Category, Entities.Category>,
                                            Framework.IoC.IAutoRegister
    {

    }

    public class CategoryRepository : BaseRepository<Entities.Category, Entities.Category>, ICategoryRepository
    {
        public CategoryRepository(IQuestionContextProvider context)
            : base(context.DbContext)
        {

        }
    }
}
