using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionDsl _QuestionDsl;

        public QuestionsController(IQuestionDsl QuestionDsl)
        {
            _QuestionDsl = QuestionDsl;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<List<Question>> GetQuestions()
        {
            return await _QuestionDsl.GetQuestions();
        }
    }
}