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
    public class QuizAttemptController : ControllerBase
    {
        private readonly IQuizAttemptDsl _iQuizAttemptDsl;

        public QuizAttemptController(IQuizAttemptDsl quizAttemptDsl)
        {
            _iQuizAttemptDsl = quizAttemptDsl;
        }

        // POST: api/QuizAttempt
        [HttpPost("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<QuizAttempt>> PostQuizAttempt(string userId, QuizAttempt quizAttempt)
        {
            await _iQuizAttemptDsl.PostQuizAttempt(userId, quizAttempt);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizAttempt>>> GetQuizAttempts()
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _iQuizAttemptDsl.GetQuizAttempts(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttempt(int id)
        {
            return await _iQuizAttemptDsl.GetQuizAttempt(id);
        }

        [HttpGet("GetQuizAttemptWithQuiz/{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttemptWithQuiz(int id)
        {
            return await _iQuizAttemptDsl.GetQuizAttemptWithQuiz(id);
        }

        [HttpPut]
        public async Task<ActionResult<QuizAttempt>> PutQuizAttempt(QuizAttempt quizAttempt)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (!ModelState.IsValid || string.IsNullOrEmpty(userId))
                return BadRequest(quizAttempt);
            quizAttempt.UserId = userId;
            await _iQuizAttemptDsl.InsertQuizAttempt(quizAttempt);
            return CreatedAtAction("GetQuizAttempt", new {id = quizAttempt.Id}, quizAttempt);
        }

        [HttpPut("UpdateQuizAttemptGrade")]
        public async Task<ActionResult<QuizAttempt>> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId) || userId != quizAttempt.Quiz.UserId) return BadRequest();
            await _iQuizAttemptDsl.UpdateQuizAttemptGrade(quizAttempt);
            return NoContent();
        }

        [HttpGet("GetUserQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetUserQuizAttemptsForQuiz(int quizId)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(quizId);
            return await _iQuizAttemptDsl.GetUserQuizAttemptsForQuiz(quizId, userId);
        }

        [HttpGet("GetAllQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            return await _iQuizAttemptDsl.GetAllQuizAttemptsForQuiz(quizId);
        }
    }
}