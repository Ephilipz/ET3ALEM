using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCollectionController : ControllerBase
    {
        private readonly IQuestionCollectionDsl _IQuestionCollectionDsl;

        public QuestionCollectionController(IQuestionCollectionDsl IQuestionCollectionDsl)
        {
            _IQuestionCollectionDsl = IQuestionCollectionDsl;
        }

        // GET api/<QuestionCollectionController>/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<QuestionCollection>>> GetQuestionCollections(string userId)
        {
            return await _IQuestionCollectionDsl.GetQuestionCollections(userId);
        }

        // POST api/<QuestionCollectionController>
        [HttpPost]
        public async Task<ActionResult<QuestionCollection>> Post(QuestionCollection questionCollection)
        {
            return await _IQuestionCollectionDsl.InsertQuestionCollection(questionCollection);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionCollection>> DeleteQuestionCollection(int id)
        {
            return await _IQuestionCollectionDsl.DeleteQuestionCollection(id);
        }
    }
}
