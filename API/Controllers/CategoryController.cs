using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Web.Http.Cors;
using Crucial.Services.Managers.Interfaces;
using Crucial.Qyz.Commands;
using API.Mappers;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:8000,http://localhost:6307", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly IQuestionManager _questionManager;
        private CategoryToCategoryMapper _categoryMapper;
        private ICommandBus _commandBus;

        public CategoriesController(IQuestionManager questionManager, ICommandBus commandBus)
        {
            _questionManager = questionManager;
            _categoryMapper = new CategoryToCategoryMapper();
            _commandBus = commandBus;
        }

        // GET: api/User
        public IEnumerable<API.Models.Category> Get()
        {
            var categories = _questionManager.GetUserCategories();
            return categories.Select(c => _categoryMapper.ToThirdPartyEntity(c)).ToList();
        }

        // GET: api/User/5
        public Category Get(int id)
        {
            var category = _questionManager.GetUserCategory(id);
            return _categoryMapper.ToThirdPartyEntity(category);
        }

        // POST: api/User
        public void Post([FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryCreateCommand(value.Name));
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryNameChangeCommand(value.Id, value.Name, value.Version));
        }

        // DELETE: api/User/5
        public void Delete(int id, [FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryDeleteCommand(id, value.Version));
        }
    }
}
