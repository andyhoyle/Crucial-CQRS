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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Crucial.Qyz.Commands.UserCategory;

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
        public async Task<IEnumerable<API.Models.Question>> Get()
        {
            var questions = await _questionManager.GetQuestions().ConfigureAwait(false);
            return questions.Select(c => _questionMapper.ToAnyEntity(c)).ToList();
        }

        // GET api/<controller>/5
        public async Task<API.Models.Question> Get(int id)
        {
            var question = await _questionManager.GetQuestion(id).ConfigureAwait(false);
            return _questionMapper.ToAnyEntity(question);
        }

        // POST api/<controller>
        public async Task Post([FromBody]API.Models.Question value)
        {
            await _commandBus.Send(new QuestionCreateCommand(value.QuestionText));
        }

        // PUT: api/<controller>/5
        public async Task Put(int id, [FromBody]API.Models.Question value)
        {
            await _commandBus.Send(new QuestionTextChangeCommand(value.Id, value.QuestionText, value.Version));
        }

        // DELETE: api/<controller>/5
        [HttpPost]
        public async Task Delete(int id, [FromBody]API.Models.Question value)
        {
            await _commandBus.Send(new QuestionDeleteCommand(id, value.Version));
        }

        //[HttpPost]
        //public async Task Category(int id, int actionId, [FromBody]API.Models.Question question)
        //{
        //    await _commandBus.Send(new AddQuestionToCategoryCommand(id, actionId, question.Version));
        //}
        //}
    }
}