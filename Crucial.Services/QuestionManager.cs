using Crucial.Providers.Questions;
using Crucial.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Services.Managers
{
    public class QuestionManager : 
        Crucial.Services.Managers.Interfaces.IQuestionManager,
        Crucial.Services.Managers.Interfaces.ICategoryManager
    {
        private ICategoryRepository _categoryRepo;
        private IQuestionRepository _questionRepo;

        private CategoryToCategoryMapper _categoryMapper;
        private QuestionToQuestionMapper _questionMapper;

        public QuestionManager(ICategoryRepository categoryRepo,
            IQuestionRepository questionRepo)
        {
            _categoryRepo = categoryRepo;
            _questionRepo = questionRepo;
            _categoryMapper = new CategoryToCategoryMapper();
            _questionMapper = new QuestionToQuestionMapper();
        }

        #region ICategoryManager implementation

        public IEnumerable<ServiceEntities.Category> GetUserCategories()
        {
            var categories = _categoryRepo.FindBy(x => x.Id > -1).ToList();
            return categories.Select(s => _categoryMapper.ToServiceEntity(s)).ToList();
        }

        public ServiceEntities.Category GetUserCategory(int categoryId)
        {
            var providerEntity = _categoryRepo.FindBy(x => x.Id == categoryId).FirstOrDefault();
            return _categoryMapper.ToServiceEntity(providerEntity);
        }

        #endregion

        #region IQuestionManager implementation

        public IEnumerable<ServiceEntities.Question> GetQuestions()
        {
            var questions = _questionRepo.FindBy(x => x.Id > -1).ToList();
            return questions.Select(q => _questionMapper.ToServiceEntity(q)).ToList();
        }

        public ServiceEntities.Question GetQuestion(int questionId)
        {
            var providerEntity = _questionRepo.FindBy(x => x.Id == questionId).FirstOrDefault();
            return _questionMapper.ToServiceEntity(providerEntity);
        }

        #endregion
    }
}
