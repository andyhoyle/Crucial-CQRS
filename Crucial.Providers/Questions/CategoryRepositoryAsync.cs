using Crucial.Framework.DesignPatterns.Repository.Async;
using Crucial.Providers.Questions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.Logging;

namespace Crucial.Providers.Questions
{
    public interface ICategoryRepositoryAsync : 
        Crucial.Framework.DesignPatterns.Repository.Async.IReadOnlyRepository<Entities.Category>,
        IUpdateRepositoryAsync<Entities.Category>,
        ICreateRepositoryAsync<Entities.Category, Entities.Category>,
        IDeleteRepositoryAsync<Entities.Category>,
        Framework.IoC.IAutoRegister
    {
    }

    public class CategoryRepositoryAsync : Crucial.Framework.Data.EntityFramework.Async.BaseRepository<IQuestionsDbContext, Entities.Category, Entities.Category>, ICategoryRepositoryAsync
    {
        public CategoryRepositoryAsync(IContextProvider<IQuestionsDbContext> cp, ILogger logger) : base(cp, logger) {}
    }
}
