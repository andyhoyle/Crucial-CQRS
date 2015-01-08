using Crucial.Providers.Questions;
using Crucial.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Services.Managers
{
    public class QuestionManager : Crucial.Services.Managers.Interfaces.IQuestionManager
    {
        private ICategoryRepository _categoryRepo;

        private CategoryToCategoryMapper _categoryMapper;

        public QuestionManager(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _categoryMapper = new CategoryToCategoryMapper();
        }

        public ServiceEntities.Category CreateCategory(string name)
        {
            var category = _categoryMapper.ToProviderEntity(new ServiceEntities.Category { Name = name});
            return _categoryMapper.ToServiceEntity(_categoryRepo.Create(category));
        }
    }
}
