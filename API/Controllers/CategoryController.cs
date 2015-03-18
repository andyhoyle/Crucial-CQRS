using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Web.Http.Cors;
using Crucial.Services.Managers.Interfaces;
using Crucial.Qyz.Commands.UserCategory;
using API.Mappers;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:8000,http://localhost:6307", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryManager _categoryManager;
        private CategoryToCategoryMapper _categoryMapper;
        private ICommandBus _commandBus;

        public CategoriesController(ICategoryManager categoryManager, ICommandBus commandBus)
        {
            _categoryManager = categoryManager;
            _categoryMapper = new CategoryToCategoryMapper();
            _commandBus = commandBus;
        }

        // GET: api/User
        public IEnumerable<API.Models.Category> Get()
        {
            var categories = _categoryManager.GetUserCategories();
            return categories.Select(c => _categoryMapper.ToAnyEntity(c)).ToList();
        }

        // GET: api/User/5
        public Category Get(int id)
        {
            var category = _categoryManager.GetUserCategory(id);
            return _categoryMapper.ToAnyEntity(category);
        }

        // POST: api/User
        public void Post([FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryCreateCommand(value.Name));
        }

        // PUT: api/<controller>/5
        public void Put(int id, [FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryNameChangeCommand(value.Id, value.Name, value.Version));
        }

        // DELETE: api/User/5
        [HttpPost]
        public void Delete(int id, [FromBody]API.Models.Category value)
        {
            _commandBus.Send(new UserCategoryDeleteCommand(id, value.Version));
        }
    }
}
