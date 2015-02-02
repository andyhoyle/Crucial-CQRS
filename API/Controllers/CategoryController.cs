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
//using Crucial.Qyz.Commands;
//using Crucial.Qyz.Configuration;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:8000,http://localhost:6307", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly IQuestionManager _questionManager;
        private static List<Category> _categories;
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
            _categories = categories.Select(c => _categoryMapper.ToThirdPartyEntity(c)).ToList();
            return _categories;
        }

        // GET: api/User/5
        public Category Get(int id)
        {
            return _categories.Where(i => i.Id == id).FirstOrDefault();
        }

        // POST: api/User
        public void Post([FromBody]API.Models.Category value)
        {
            int maxId = 1;
            
            if (_categories != null && _categories.Count > 0)
            {
                maxId = _categories.Max(i => i.Id);
            }
            
            _commandBus.Send(new UserCategoryCreateCommand(maxId + 1, value.Name));
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]API.Models.Category value)
        {
            _categories.Where(i => i.Id == id).First().Name = value.Name;
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            _categories.RemoveAll(i => i.Id == id);
        }
    }
}
