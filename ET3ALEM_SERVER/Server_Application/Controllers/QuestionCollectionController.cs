using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataServiceLayer;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCollectionController : ControllerBase
    {
        private readonly IQuestionCollectionDsl _iQuestionCollectionDsl;
        private readonly IAccountHelper _accountHelper;

        public QuestionCollectionController(IQuestionCollectionDsl questionCollectionDsl, IAccountHelper accountHelper)
        {
            _iQuestionCollectionDsl = questionCollectionDsl;
            _accountHelper = accountHelper;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionCollection>>> GetQuestionCollections()
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _iQuestionCollectionDsl.GetQuestionCollections(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionCollection>> GetQuestionCollection(int id)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _iQuestionCollectionDsl.GetQuestionCollection(id, userId);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionCollection>> Post(QuestionCollection questionCollection)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            questionCollection.UserId = userId;
            return await _iQuestionCollectionDsl.InsertQuestionCollection(questionCollection);
        }

        [HttpDelete]
        public async Task<ActionResult<QuestionCollection>> DeleteQuestionCollection(int id)
        {
            return await _iQuestionCollectionDsl.DeleteQuestionCollection(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuestionCollection questionCollection)
        {
            if (id != questionCollection.Id)
                return BadRequest();
            await _iQuestionCollectionDsl.PutQuestionCollection(id, questionCollection);
            return NoContent();
        }

        [HttpGet("NameExists/{name}")]
        public async Task<ActionResult<bool>> NameExists(string name)
        {
            return await _iQuestionCollectionDsl.NameExists(name);
        }
    }
}