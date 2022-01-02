using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataServiceLayer;
using ExceptionHandling.CustomExceptions;
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
        private readonly IQuizAttemptDsl _IQuizAttemptDsl;
        private readonly IAccountHelper _accountHelper;

        public QuizAttemptController(IQuizAttemptDsl quizAttemptDsl, IAccountHelper accountHelper)
        {
            _IQuizAttemptDsl = quizAttemptDsl;
            _accountHelper = accountHelper;
        }

        [HttpPost("{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<QuizAttempt>> PostQuizAttempt(string userId, QuizAttempt quizAttempt)
        {
            await _IQuizAttemptDsl.PutQuizAttempt(userId, quizAttempt);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizAttempt>>> GetQuizAttempts()
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _IQuizAttemptDsl.GetQuizAttempts(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttempt(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttempt(id);
        }

        [HttpGet("GetQuizAttemptWithQuiz/{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttemptWithQuiz(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttemptWithQuiz(id);
        }
        [HttpGet("GetQuizAttemptWithQuizLight/{id}")]
        public async Task<ActionResult<QuizAttemptVM>> GetQuizAttemptWithQuizLight(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttemptWithQuizLight(id);
        }

        [HttpPut]
        public async Task<ActionResult<QuizAttempt>> PutQuizAttempt(QuizAttempt quizAttempt)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (!ModelState.IsValid || string.IsNullOrEmpty(userId))
                throw new CustomExceptionBase("Invalid quiz attempt");
            quizAttempt.UserId = userId;
            await _IQuizAttemptDsl.PostQuizAttempt(quizAttempt);
            return CreatedAtAction("GetQuizAttempt", new {id = quizAttempt.Id}, quizAttempt);
        }

        [HttpPut("UpdateQuizAttemptGrade")]
        public async Task<ActionResult<QuizAttempt>> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId) || userId != quizAttempt.Quiz.UserId) return BadRequest();
            await _IQuizAttemptDsl.UpdateQuizAttemptGrade(quizAttempt);
            return NoContent();
        }

        [HttpGet("GetUserQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetUserQuizAttemptsForQuiz(int quizId)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                throw new CustomExceptionBase("Invalid user id");
            return await _IQuizAttemptDsl.GetUserQuizAttemptsForQuiz(quizId, userId);
        }

        [HttpGet("GetAllQuizAttemptsForQuiz/{quizId:int}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            return await _IQuizAttemptDsl.GetAllQuizAttemptsForQuiz(quizId);
        }

        [HttpGet("GetUngradedAttemptsForQuiz/{quizId:int}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetUngradedAttemptsForQuiz(int quizId)
        {
            return await _IQuizAttemptDsl.GetUngradedAttemptsForQuiz(quizId);
        }
    }
}