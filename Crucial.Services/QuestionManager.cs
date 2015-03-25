using Crucial.Providers.Questions;
using Crucial.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;
using Crucial.Framework.Logging;

namespace Crucial.Services.Managers
{
    public class QuestionManager : 
        Crucial.Services.Managers.Interfaces.IQuestionManager,
        Crucial.Services.Managers.Interfaces.ICategoryManager
    {
        private readonly ICategoryRepositoryAsync _categoryRepo;
        private readonly IQuestionRepositoryAsync _questionRepo;
        private readonly ILogger _logger;

        private readonly CategoryToCategoryMapper _categoryMapper;
        private readonly QuestionToQuestionMapper _questionMapper;

        public QuestionManager(ICategoryRepositoryAsync categoryRepo,
            IQuestionRepositoryAsync questionRepo, ILogger logger)
        {
            _categoryRepo = categoryRepo;
            _questionRepo = questionRepo;
            _logger = logger;
            _categoryMapper = new CategoryToCategoryMapper();
            _questionMapper = new QuestionToQuestionMapper();
        }

        #region ICategoryManager implementation

        public async Task<IEnumerable<ServiceEntities.Category>> GetUserCategories()
        {
            _logger.Trace("Get User Categories Start");

            var categories = await _categoryRepo.FindByAsync(x => x.Id > -1).ConfigureAwait(false);
            var categoriesList = categories.ToList();
            var cat = categoriesList.Select(_categoryMapper.ToServiceEntity);

            _logger.Trace("Get User Categories End");

            return cat;
        }

        public async Task<ServiceEntities.Category> GetUserCategory(int categoryId)
        {
            _logger.Trace(String.Format("Get User Category: {0} Start", categoryId));

            var providerEntity = await _categoryRepo.FindByAsync(x => x.Id == categoryId).ConfigureAwait(false);
            var cat = _categoryMapper.ToServiceEntity(providerEntity.FirstOrDefault());

            _logger.Trace(String.Format("Get User Category: {0} End", categoryId));

            return cat;
        }

        #endregion

        #region IQuestionManager implementation

        public async Task<IEnumerable<ServiceEntities.Question>> GetQuestions()
        {
            _logger.Trace("Get Questions Start");

            var questions = await _questionRepo.FindByAsync(x => x.Id > -1).ConfigureAwait(false);
            var questionsList = questions.ToList();
            var ques = questionsList.Select(_questionMapper.ToServiceEntity);

            _logger.Trace("Get Questions End");

            return ques;
        }

        public async Task<ServiceEntities.Question> GetQuestion(int questionId)
        {
            _logger.Trace(String.Format("Get Question: {0} Start", questionId));

            var providerEntity = await _questionRepo.FindByAsync(x => x.Id == questionId).ConfigureAwait(false);
            var q = _questionMapper.ToServiceEntity(providerEntity.FirstOrDefault());
            
            _logger.Trace(String.Format("Get Question: {0} End", questionId));

            return q;
        }

        #endregion
    }
}
