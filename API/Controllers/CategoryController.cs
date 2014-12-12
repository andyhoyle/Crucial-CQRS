using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace Api.Controllers
{
    public class CategoryController : ApiController
    {
        // GET: api/User
        public IEnumerable<API.Models.Category> Get()
        {
            return new API.Models.Category[] { new Category { Name = "value1" }, new Category { Name = "value2" } };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
