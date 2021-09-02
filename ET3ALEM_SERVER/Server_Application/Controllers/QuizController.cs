using System.Collections.Generic;
using System.Security.Claims;
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
    public class QuizController : ControllerBase
    {
        private readonly IQuizDsl _iQuizDsl;

        public QuizController(IQuizDsl quizDsl)
        {
            _iQuizDsl = quizDsl;
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _iQuizDsl.GetQuiz(id);

            if (quiz == null) return NotFound();

            return quiz;
        }

        [HttpGet("GetQuizTitleFromCode/{code}")]
        public async Task<ActionResult<string>> GetQuizTitleFromCode(string code)
        {
            var title = await _iQuizDsl.GetQuizTitleFromCode(code);

            if (string.IsNullOrEmpty(title)) return NotFound();

            var returnedTitle = new
            {
                title
            };

            return Ok(returnedTitle);
        }

        //GET: api/Quiz
        [HttpGet("GetBasicQuizByCode/{code}")]
        public async Task<ActionResult<Quiz>> GetBasicQuizByCode(string code)
        {
            return await _iQuizDsl.GetBasicQuizByCode(code);
        }

        [HttpGet("GetFullQuizByCode/{code}")]
        public async Task<ActionResult<Quiz>> GetFullQuizByCode(string code)
        {
            return await _iQuizDsl.GetFullQuizByCode(code);
        }

        [HttpGet]
        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            var quizList = await _iQuizDsl.GetQuizzes(userId);
            return quizList;
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid || string.IsNullOrEmpty(userId)) return BadRequest(quiz);
            quiz.UserId = userId;
            await _iQuizDsl.InsertQuiz(quiz);
            return CreatedAtAction("GetQuiz", new {id = quiz.Id}, quiz);
        }

        [HttpDelete]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _iQuizDsl.DeleteQuiz(id);
            if (quiz == null) return NotFound();
            return quiz;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
                return BadRequest();

            await _iQuizDsl.PutQuiz(id, quiz);

            return NoContent();
        }
    }
}