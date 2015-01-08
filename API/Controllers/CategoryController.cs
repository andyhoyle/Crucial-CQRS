using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Web.Http.Cors;
using Crucial.Services.Managers.Interfaces;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:8000", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly IQuestionManager _questionManager;
        private static List<Category> _categories;

        public CategoriesController(IQuestionManager questionManager)
        {
            _questionManager = questionManager;
            _categories = new List<Category>();
            _categories.Add(new Category { Id = 1, Name = "General Knowledge" });
            _categories.Add(new Category { Id = 2, Name = "Music" });
        }

        // GET: api/User
        public IEnumerable<API.Models.Category> Get()
        {
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
            int maxId = _categories.Max(i => i.Id);
            value.Id = maxId + 1;
            _categories.Add(value);
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
