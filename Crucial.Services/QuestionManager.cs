using Crucial.Providers.Questions;
using Crucial.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crucial.Framework.DesignPatterns.Repository.Async.Extensions;

namespace Crucial.Services.Managers
{
    public class QuestionManager : 
        Crucial.Services.Managers.Interfaces.IQuestionManager,
        Crucial.Services.Managers.Interfaces.ICategoryManager
    {
        private ICategoryRepositoryAsync _categoryRepo;
        private IQuestionRepositoryAsync _questionRepo;

        private CategoryToCategoryMapper _categoryMapper;
        private QuestionToQuestionMapper _questionMapper;

        public QuestionManager(ICategoryRepositoryAsync categoryRepo,
            IQuestionRepositoryAsync questionRepo)
        {
            _categoryRepo = categoryRepo;
            _questionRepo = questionRepo;
            _categoryMapper = new CategoryToCategoryMapper();
            _questionMapper = new QuestionToQuestionMapper();
        }

        #region ICategoryManager implementation

        public async Task<IEnumerable<ServiceEntities.Category>> GetUserCategories()
        {
            var categories = await _categoryRepo.FindByAsync(x => x.Id > -1).ConfigureAwait(false);
            var categoriesList = categories.ToList();
            return categoriesList.Select(_categoryMapper.ToServiceEntity);
        }

        public async Task<ServiceEntities.Category> GetUserCategory(int categoryId)
        {
            var providerEntity = await _categoryRepo.FindByAsync(x => x.Id == categoryId).ConfigureAwait(false);
            return _categoryMapper.ToServiceEntity(providerEntity.FirstOrDefault());
        }

        #endregion

        #region IQuestionManager implementation

        public async Task<IEnumerable<ServiceEntities.Question>> GetQuestions()
        {
            var questions = await _questionRepo.FindByAsync(x => x.Id > -1).ConfigureAwait(false);
            var questionsList = questions.ToList();
            return questionsList.Select(_questionMapper.ToServiceEntity);
        }

        public async Task<ServiceEntities.Question> GetQuestion(int questionId)
        {
            var providerEntity = await _questionRepo.FindByAsync(x => x.Id == questionId).ConfigureAwait(false);
            return _questionMapper.ToServiceEntity(providerEntity.FirstOrDefault());
        }

        #endregion
    }
}
