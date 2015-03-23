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
using System.Threading.Tasks;

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
        public async Task<IEnumerable<API.Models.Category>> Get()
        {
            var categories = await _categoryManager.GetUserCategories().ConfigureAwait(false);
            return categories.Select(c => _categoryMapper.ToAnyEntity(c)).ToList();
        }

        // GET: api/User/5
        public async Task<Category> Get(int id)
        {
            var category = await _categoryManager.GetUserCategory(id).ConfigureAwait(false);
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
