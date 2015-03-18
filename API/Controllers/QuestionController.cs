using API.Mappers;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Qyz.Commands;
using Crucial.Qyz.Commands.Question;
using Crucial.Services.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "http://localhost:8000,http://localhost:6307", headers: "*", methods: "*")]
    public class QuestionsController : ApiController
    {
        ICommandBus _commandBus;
        IQuestionManager _questionManager;
        QuestionToQuestionMapper _questionMapper;

        public QuestionsController(ICommandBus commandBus,
            IQuestionManager questionManager)
        {
            _commandBus = commandBus;
            _questionManager = questionManager;
            _questionMapper = new QuestionToQuestionMapper();
        }

        // GET api/<controller>
        public IEnumerable<API.Models.Question> Get()
        {
            var questions = _questionManager.GetQuestions();
            return questions.Select(c => _questionMapper.ToAnyEntity(c)).ToList();
        }

        // GET api/<controller>/5
        public API.Models.Question Get(int id)
        {
            var question = _questionManager.GetQuestion(id);
            return _questionMapper.ToAnyEntity(question);
        }

        // POST api/<controller>
        public void Post([FromBody]API.Models.Question value)
        {
            _commandBus.Send(new QuestionCreateCommand(value.QuestionText));
        }

        // PUT: api/<controller>/5
        public void Put(int id, [FromBody]API.Models.Question value)
        {
            _commandBus.Send(new QuestionTextChangeCommand(value.Id, value.QuestionText, value.Version));
        }

        // DELETE: api/<controller>/5
        [HttpPost]
        public void Delete(int id, [FromBody]API.Models.Category value)
        {
            _commandBus.Send(new QuestionDeleteCommand(id, value.Version));
        }
    }
}